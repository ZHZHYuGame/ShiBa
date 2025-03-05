using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> where T:class,new()
{
    private static T Instance;
    public static T Ins
    {
        get
        {
            if (Instance==null)
            {
                Instance = new T();
            }
            return Instance;
        }
    }
}
