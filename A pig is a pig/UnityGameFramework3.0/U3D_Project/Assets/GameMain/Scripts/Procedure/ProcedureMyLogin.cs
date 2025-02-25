//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using ConfigTools;
using GameFramework.Event;
using System;
using UnityEngine;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace StarForce
{
    public class ProcedureMyLogin : ProcedureBase
    {
        private bool m_StartGame = false;
        private LoginForm m_LoginForm = null;

        bool isChangeScene=false;
        public override bool UseNativeDialog
        {
            get
            {
                return false;
            }
        }


        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            GameEntry.UI.OpenUIForm(UIFormId.LoginView, this);
            GameEntry.Event.Subscribe(ChangeSceneEventArgs.EventId, ChangeSceneSuccess);
            Debug.Log("我的场景");
        }

        private void ChangeSceneSuccess(object sender, GameEventArgs e)
        {
            isChangeScene = true;
        }

        

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            if(isChangeScene)
            {
                procedureOwner.SetData<VarInt32>("NextSceneId", Test.ins.sceneId);
                ChangeState<ProcedureChangeScene>(procedureOwner);
                isChangeScene = false;
            }
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
        }

    }
}
