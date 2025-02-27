using GameFramework.Resource;
using StarForce;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ConfigMgr
{
    #region
    //static List<string> folderNames = new List<string>
    //    {
    //        "100", "1000", "2000", "3000", "4000", "5000", "6000", "8000", "9000","12000", "22000", "23000",
    //        "24000", "26000", "27000", "28000", "30000", "44000", "50000", "51000", "52000", "53000", "60000",
    //        "61000", "62000", "63000", "65000","70000","90000"
    //    };

    //static SpriteAtlas itemAtlas;

    //public static void Init()
    //{
    //    GetItemSpriteAtlas();
    //}

    //public static string FindTargetFolder(int targetId)
    //{
    //    string targetFolder = null;

    //    foreach (string folderName in folderNames)
    //    {
    //        if (int.TryParse(folderName, out int folderInt) && folderInt <= targetId)
    //        {
    //            targetFolder = folderName;
    //        }
    //    }

    //    return targetFolder;
    //}
    //public static void GetItemSpriteAtlas()
    //{
    //    string path = AssetUtility.GetSpriteAtlasAsset();
    //    // ���غ�Ļص�����
    //    var loadCallback = new LoadAssetCallbacks((assetName, asset, duration, userData) =>
    //    {
    //        itemAtlas = asset as SpriteAtlas;
    //    });
    //    GameEntry.Resource.LoadAsset(path, typeof(SpriteAtlas), loadCallback);
    //}

    //public static void SetItemSpriteByAtlas(Image img, string id)
    //{

    //    img.sprite = itemAtlas.GetSprite("Item_" + id);
    //}
    //    public static void SetSpriteById(Image img, string icon_id)
    //{
    //    // ��Դ��·��
    //    string path = FindTargetFolder(int.Parse(icon_id)) + "/Item_" + icon_id;
    //    var bytePath = AssetUtility.GetItemSpriteAsset(path);
    //    // ���غ�Ļص�����
    //    var loadCallback = new LoadAssetCallbacks((assetName, asset, duration, userData) =>
    //    {
    //        img.sprite = asset as Sprite;
    //    });
    //    GameEntry.Resource.LoadAsset(bytePath, typeof(Sprite), loadCallback);

    //}

    //public static void GetProfById(Image img, sbyte prof)
    //{
    //    // ��Դ��·��
    //    var bytePath = AssetUtility.GetProfSpriteAsset(prof.ToString());
    //    // ���غ�Ļص�����
    //    var loadCallback = new LoadAssetCallbacks((assetName, asset, duration, userData) =>
    //    {
    //        img.sprite = asset as Sprite;
    //    });
    //    GameEntry.Resource.LoadAsset(bytePath, typeof(Sprite), loadCallback);
    //}
    //public static void GetVipByLevel(Image img, string level)
    //{
    //    // ��Դ��·��
    //    var bytePath = AssetUtility.GetVipSpriteAsset(level);
    //    // ���غ�Ļص�����
    //    var loadCallback = new LoadAssetCallbacks((assetName, asset, duration, userData) =>
    //    {
    //        img.sprite = asset as Sprite;
    //    });
    //    GameEntry.Resource.LoadAsset(bytePath, typeof(Sprite), loadCallback);
    //}

    //public static void GetSkillById(Image icon, int skill_icon)
    //{
    //    // ��Դ��·��
    //    var bytePath = AssetUtility.GetSkillSpriteAsset(skill_icon.ToString());
    //    // ���غ�Ļص�����
    //    var loadCallback = new LoadAssetCallbacks((assetName, asset, duration, userData) =>
    //    {
    //        icon.sprite = asset as Sprite;
    //    });
    //    GameEntry.Resource.LoadAsset(bytePath, typeof(Sprite), loadCallback);

    //}
    #endregion

    public static void GetSpriteByName(Image img,IconType type,string name)
    {
        var bytePath = AssetUtility.GetSpritesAsset(type ,name);
        var loadCallback = new LoadAssetCallbacks((assetName, asset, duration, userData) =>
        {
            img.sprite = asset as Sprite;
        });
        GameEntry.Resource.LoadAsset(bytePath, typeof(Sprite), loadCallback);

    }

    public static void GetMaterialByName(Material material ,string name)
    {
        var bytePath = AssetUtility.GetMaterialAsset(name);
        var loadCallback = new LoadAssetCallbacks((assetName, asset, duration, userData) =>
        {
            material = asset as Material;
        });
        GameEntry.Resource.LoadAsset(bytePath, typeof(Material), loadCallback);
    }
}
