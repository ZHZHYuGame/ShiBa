using GameFramework.UI;
using StarForce;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

public class SelectRolePanel : UGuiForm
{
    public Transform content;
    public RoleItem role;
    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        role.gameObject.SetActive(false);
        UpdataRoleList();
    }
    int num=0;
    private void UpdataRoleList()
    {
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
        foreach (var item in Test.ins.roleList)
        {
            RoleItem go = Instantiate(role,content);
            go.gameObject.SetActive(true);
            go.Init(item);
            num++;
        }
        for (int i = 0; i < 3-num; i++)
        {
            RoleItem go = Instantiate(role, content);
            go.Init(null);
        }
    }

    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
    }


}
