using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTest : MonoBehaviour
{
    public Image icon;
    public Button btn;
    private AssetBundle ab;
    private AssetBundle ab1;
    private AssetBundle ab2;
    private void Awake()
    {
        //ab = AssetBundle.LoadFromFile(GetS + "prefab.sb");
        //ab1 = AssetBundle.LoadFromFile(GetS + "material.sb");
        //ab2 = AssetBundle.LoadFromFile(GetS + "image.sb");
        ABMgr.Init("PC");
    }
    // Start is called before the first frame update
    void Start()
    {

        //icon.GetComponent<Image>().sprite= Instantiate(ab2.LoadAsset<Sprite>("image1"));
        //Instantiate(ab.LoadAsset<GameObject>("1"));
        //Instantiate(ab.LoadAsset<GameObject>("Cube"));
        //Instantiate(ab.LoadAsset<GameObject>("1"));
        //Instantiate(ab.LoadAsset<GameObject>("Cube"));
        icon.GetComponent<Image>().sprite = Instantiate(ABMgr.Load<Sprite>("image.sb", "image1"));
        Instantiate(ABMgr.Load<GameObject>("prefab.sb","1"));
        Instantiate(ABMgr.Load<GameObject>("prefab.sb", "Cube"));




    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// »ñÈ¡SÂ·¾¶
    /// </summary>
    public string GetS
    {
        get
        {
            return Application.streamingAssetsPath + "/PC/";
        }
    }

}
