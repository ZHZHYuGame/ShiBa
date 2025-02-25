using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoleItem : MonoBehaviour
{
    public Role date;
    [SerializeField] GameObject bg, creatbg;

    internal void Init(Role item)
    {
        if(item!=null)
        {
            date = item;
        }
        else
        {
            bg.gameObject.SetActive(false);
            creatbg.gameObject.SetActive(true);
        }
    }
}
