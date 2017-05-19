using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SingleTon<T> where T:new()
{
    static T s_instance;
    public static T Instance
    {
        get
        {
            if (null == s_instance)
                s_instance = new T();
            return s_instance;
        }
    }
}

