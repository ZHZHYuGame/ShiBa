using System;
using System.Collections.Generic;
using ZeroFormatter;
using GameFramework.Resource;
using StarForce;
using UnityEngine;

namespace ConfigTools
{
    public partial class DataMgr : Singleton<DataMgr>
    {
        private readonly Dictionary<string, Action<byte[]>> loadDataDic = new Dictionary<string, Action<byte[]>>();

        private bool isLoaded;

        public override void Init()
        {
            if (isLoaded) return;

            ZeroFormatterRegister.Register();
            LoadConfig();
            
            isLoaded = true;
        }

        private void LoadConfig()
        {
            foreach (var data in loadDataDic)
            {
                // 资源的路径
                var bytePath = AssetUtility.GetByteAsset(data.Key);

                // 加载后的回调函数
                var loadCallback = new LoadAssetCallbacks((assetName, asset, duration, userData) =>
                {
                    if (asset is TextAsset byteText)
                    {
                        data.Value.Invoke(byteText.bytes);
                    }
                });
        
                GameEntry.Resource.LoadAsset(bytePath, typeof(TextAsset), loadCallback);
            }
        }
    }
}
