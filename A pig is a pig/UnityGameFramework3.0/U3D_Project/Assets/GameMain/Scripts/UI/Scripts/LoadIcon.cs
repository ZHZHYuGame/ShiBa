using GameFramework.Resource;
using StarForce;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AtylisType { Chan }
public static class LoadIcon
{
    public static void Load(this Image imag, AtylisType type, string name)
    {
        var callback = new LoadAssetCallbacks((string assetName, object asset, float duration, object userData) =>
        {
            if (asset == null) return;
            if (asset as Sprite)
            {
                imag.sprite = (Sprite)asset;
            }
        });
        var path = AssetUtility.GetAtylisType(type, name);
        GameEntry.Resource.LoadAsset(path.ToString(), typeof(Sprite), callback);
    }
}