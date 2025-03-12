using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainPanel : BasePanel
{
    private static string name = "MainPanel";
    private static string path = "Panel/MainPanel";
    private static LayerType layerType = LayerType.Normal;
    private List<Button>btns= new List<Button>();
    public static readonly UIType uIType = new UIType(path, name, layerType);

    public MainPanel() : base(uIType)
    {

    }
    List<Toggle> toggles = new List<Toggle>();
    GameObject togglegame;
    public override void OnStart()
    {
        base.OnStart();

        togglegame=UIMethod.Ins.GetOrAddSingleComponentInChild<ToggleGroup>(ActiveObj, "togglegame").gameObject;
        for (int i = 0; i < togglegame.transform.childCount; i++)
        {
            toggles.Add(UIMethod.Ins.AddOrGetComponent<Toggle>(togglegame.transform.GetChild(i).gameObject));
        }
        UIMethod.Ins.GetOrAddSingleComponentInChild<Button>(ActiveObj, "Load").onClick.AddListener(Load);
        for (int i = 0; i < toggles.Count; i++)
        {
            int index = i;
            toggles[index].transform.GetChild(2).gameObject.SetActive(false);
            toggles[index].onValueChanged.AddListener((X) =>
            {
                if (X == true)
                {
                    toggles[index].transform.GetChild(1).transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
                    toggles[index].transform.GetChild(2).gameObject.SetActive(true);

                }
                else
                {
                    toggles[index].transform.GetChild(1).transform.localScale = new Vector3(1f, 1f, 1f);
                    toggles[index].transform.GetChild(2).gameObject.SetActive(false);
                }
            });
        }
        toggles[2].isOn = true;
        toggles[2].transform.GetChild(1).transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        toggles[2].transform.GetChild(2).gameObject.SetActive(true);
        GameObject content = UIMethod.Ins.GetOrAddSingleComponentInChild<RectTransform>(ActiveObj, "Content").gameObject;
        // Debug.Log(gameObject.name);
        for (int i = 0; i < GameMgr.GetInstance().dataAnalysis.mapDic.Count; i++)
        {

            //Button levelBtn = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<Button>("Assets/GameMain/GameResources/Prefabs/ScenePrefab.prefab"), content.transform);
            Button levelBtn = UIMethod.Ins.InstantiatePrefab<Button>("ScenePrefab", content.transform);
            levelBtn.transform.Find("Text").GetComponent<Text>().text = GameMgr.GetInstance().dataAnalysis.mapDic[i].Map_Name;
            levelBtn.GetComponent<Image>().sprite = UIManager.Ins._resourcesManager.LoadResource<Sprite>(Application.streamingAssetsPath + "/ui", GameMgr.GetInstance().dataAnalysis.mapDic[i].Map_Icon, "ui");
            //levelBtn.GetComponent<Image>().sprite = AssetDatabase.LoadAssetAtPath<Sprite>(GameMgr.GetInstance().dataAnalysis.mapDic[i].Map_Icon);
            Debug.Log(GameMgr.GetInstance().dataAnalysis.mapDic[i].Map_Icon);
            btns.Add(levelBtn);
        }
        for (int i = 0; i < btns.Count; i++)
        {
            int levelIndex = i;
            btns[i].onClick.AddListener(() =>
            {
                //跳转场景
                Game game = new Game();
                PlayerPrefs.SetInt("levelIndex", levelIndex);
                GameMgr.GetInstance().UIManager_Root.Pop(false);
                GameMgr.GetInstance().SceneControl_Root.LoadScene(game.SceneName, game);

            });
        }

    }

    private void Load()
    {
        Game game = new Game();
        GameMgr.GetInstance().SceneControl_Root.LoadScene(game.SceneName, game);
        
    }

    public override void OnEndable()
    {
        base.OnEndable();
    }

    public override void OnDistroy()
    {
        base.OnDistroy();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }
}
