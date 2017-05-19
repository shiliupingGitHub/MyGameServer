using System.Collections;
using System.Net;
using System.Net.Sockets;
using System;
using System.Collections.Generic;
using System.IO;
public class EasyClient
{

    public enum PEER_STATUS
    {
        NONE,
        CONNECTING,
        CONNECTED,
    }
    struct SendArg
    {
        public uint opCode;
        public byte[] stream;
    }
    TcpClient mClient;
    PEER_STATUS mStaus = PEER_STATUS.NONE;
    public Action<bool> mOnConnect;
    public Action<uint, byte[]> mOnRecieve;
    Queue<SendArg> mSendBuff = new Queue<SendArg>();
    IAsyncResult mSendAsy = null;
    IAsyncResult mRecieveAsy = null;
    MemoryStream mRecieveStream = null;
    public void Conect(string host, int port)
    {
        if (mStaus != PEER_STATUS.NONE)
            return;
        Close();
        mClient = new TcpClient(famliy);
        mStaus = PEER_STATUS.CONNECTING;
        mClient.BeginConnect(host, port, onBeginConnect, null);
    }

    public void SendMsg(uint opCode, byte[] stream)
    {
        SendArg arg = new SendArg();
        arg.opCode = opCode;
        arg.stream = stream;
        mSendBuff.Enqueue(arg);
    }

    public void SetUp()
    {
        System.Timers.Timer t = new System.Timers.Timer(1);
        t.Elapsed += OnTimeHander;
        t.AutoReset = true;
        t.Enabled = true;
    }
    void OnTimeHander(object source, System.Timers.ElapsedEventArgs e)
    {
        if (mStaus != PEER_STATUS.CONNECTED)
            return;
        PostSend();
        PostRecieve();

    }
    void PostSend()
    {
        if (null != mClient && mClient.Connected && mClient.GetStream().CanWrite)
        {
            if (null == mSendAsy)
            {
                if (mSendBuff.Count > 0)
                {
                    SendArg arg = mSendBuff.Dequeue();
                    using (MemoryStream ms = new MemoryStream())
                    {
                        byte[] opBs = BitConverter.GetBytes(arg.opCode);
                        ms.Write(opBs, 0, opBs.Length);
                        byte[] lenBs = BitConverter.GetBytes((uint)arg.stream.Length);
                        ms.Write(lenBs, 0, lenBs.Length);
                        ms.Write(arg.stream, 0, arg.stream.Length);
                        PostSend(ms.ToArray());
                    }

                }

            }
        }
    }
    void PostRecieve()
    {
        if (mStaus != PEER_STATUS.CONNECTED)
            return;
        if (null != mClient && mClient.Connected && mClient.GetStream().CanRead)
        {
            if (null == mRecieveAsy)
            {
                byte[] buff = new byte[1024];
                mRecieveAsy = mClient.GetStream().BeginRead(buff, 0, buff.Length, onEndRecieve, buff);
            }

        }
        RecievePacket();
    }
    void RecievePacket()
    {
        if (mRecieveStream.Length >= 8)
        {
            byte[] opBs = new byte[4];
            byte[] opLen = new byte[4];
            mRecieveStream.Read(opBs, 0, 4);
            mRecieveStream.Read(opLen, 3, 4);
            uint len = BitConverter.ToUInt32(opLen, 0);
            int left = 0;
            if (mRecieveStream.Length >= len + 8)
            {
                byte[] packetBs = new byte[len];
                mRecieveStream.Read(packetBs, 8, (int)len);
                left = (int)mRecieveStream.Length - (int)(len + 8);
                uint opCode = BitConverter.ToUInt32(opBs, 0);
                if (null != mOnRecieve)
                    mOnRecieve(opCode, packetBs);
            }
            else
                return;
            if (left > 0)
            {
                byte[] leftBs = new byte[left];
                mRecieveStream.Close();
                mRecieveStream = new MemoryStream();
                mRecieveStream.Write(leftBs, 0, leftBs.Length);
                RecievePacket();
            }

        }
    }
    void onEndRecieve(IAsyncResult ar)
    {
        int readLen = mClient.GetStream().EndRead(ar);
        byte[] buff = (byte[])ar.AsyncState;
        mRecieveStream.Write(buff, 0, readLen);
        mRecieveAsy = null;
    }
    void PostSend(byte[] buff)
    {
        if (null != mClient && mClient.Connected)
        {
            mSendAsy = mClient.GetStream().BeginWrite(buff, 0, buff.Length, onEndSend, null);
        }
    }
    void onEndSend(IAsyncResult ar)
    {
        mClient.GetStream().EndWrite(ar);
        mSendAsy = null;
    }
    void Close()
    {
        if (null != mClient)
            mClient.Close();
        mClient = null;
        mStaus = PEER_STATUS.NONE;
        mSendBuff.Clear();
        mSendAsy = null;
        mRecieveAsy = null;
        mRecieveStream.Close();
    }
    void onBeginConnect(IAsyncResult ar)
    {
        if (null != mClient)
        {
            mClient.EndConnect(ar);
            if (null != mOnConnect)
            {
                mOnConnect(mClient.Connected);
            }
            if (!mClient.Connected)
            {
                Close();
            }
            else
            {
                mRecieveStream = new MemoryStream();
                mStaus = PEER_STATUS.CONNECTED;
            }
        }
    }
    AddressFamily famliy
    {
        get
        {
            IPHostEntry entry = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var e in entry.AddressList)
            {
                if (e.AddressFamily == AddressFamily.InterNetwork)
                    return e.AddressFamily;
                else if (e.AddressFamily == AddressFamily.InterNetworkV6)
                    return e.AddressFamily;
            }
            return AddressFamily.InterNetwork;
        }
    }
}

