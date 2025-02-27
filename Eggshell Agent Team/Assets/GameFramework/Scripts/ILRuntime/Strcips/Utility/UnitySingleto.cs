using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitySingleto<T> : MonoBehaviour where T:Component
{

    private static T instance;
    public static T Instanct
    {
        get
        {
            if(instance==null)
            {
               
                instance =GameObject.FindObjectOfType(typeof(T)) as T;  
                if(instance==null)
                {
                    GameObject obj = new GameObject();
                    instance=obj.AddComponent<T>(); 
                    obj.hideFlags = HideFlags.DontSave;
                    obj.name= typeof(T).Name;
                }
            }
            return instance;
        }
    }
   public virtual void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (instance == null)
        {
            instance = this as T;
        }
        else
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
