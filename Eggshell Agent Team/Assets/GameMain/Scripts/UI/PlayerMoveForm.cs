//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------
using StarForce;
using UnityEngine;
using UnityGameFramework.Runtime;


public class PlayerMoveForm : UGuiForm
{
    [SerializeField] ETC etc;
    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
        SceneComponent sceneComponent = UnityGameFramework.Runtime.GameEntry.GetComponent<SceneComponent>();
        //卸载场景
        sceneComponent.UnloadScene("Assets/GameMain/Scenes/Menu.unity");
        //调用加载场景方法
        sceneComponent.LoadScene("Assets/GameMain/Scenes/Main.unity");

    }

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        //etc = GameObject.Find()
    }

    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);
    }

    protected override void OnClose(bool isShutdown, object userData)
    {
        base.OnClose(isShutdown, userData);
    }
    
}
