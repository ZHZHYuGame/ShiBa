//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using UnityEngine;
using UnityGameFramework.Runtime;

namespace StarForce
{
    public class LoginForm : UGuiForm
    {

        private ProcedureLogin m_ProcedureLogin = null;


        public void Login()
		{
            Log.Debug("ProcedureLogin is invalid when open LoginForm.");
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnOpen(object userData)
#else
        protected internal override void OnOpen(object userData)
#endif
        {
            base.OnOpen(userData);

            m_ProcedureLogin = (ProcedureLogin)userData;
            if (m_ProcedureLogin == null)
            {
                Log.Warning("ProcedureMenu is invalid when open MenuForm.");
                return;
            }

        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnClose(bool isShutdown, object userData)
#else
        protected internal override void OnClose(bool isShutdown, object userData)
#endif
        {
            m_ProcedureLogin = null;

            base.OnClose(isShutdown, userData);
        }
    }
}
