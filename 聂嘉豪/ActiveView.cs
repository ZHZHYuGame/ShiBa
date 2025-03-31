using ConfigTools;
using GameFramework;
using GameFramework.Resource;
using StarForce;
using UnityEngine;
using UnityEngine.UI;

public class BagCellView : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Text numText;
    
    bool ishave;

    KnapsackItem knapsackInfo1;

    Button btn;

    gift1 gift1 ;
    gift2 gift2;
    Zconsume Zconsume;
    Bconsume Bconsume;
    Equipments equipments;

    private void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(() =>
        {
            GameObject go = Instantiate(Resources.Load<GameObject>("BagItemCollider"), transform.parent.parent.parent.parent);
            go.transform.Find("item1").Find("Icon").GetComponent<Image>().sprite = icon.sprite;
            go.transform.Find("close").GetComponent<Button>().onClick.AddListener(() =>
            {
                Destroy(go);
            });
          
            if (equipments!=null)
            {
                go.transform.Find("item1").Find("Btn_Use").GetComponentInChildren<Text>().text = "穿戴";
                go.transform.Find("item1").Find("Btn_Use").GetComponent<Button>().onClick.AddListener(() =>
                {
                    //CSDiscardItem cSDiscardItem = ReferencePool.Acquire<CSDiscardItem>();
                    //cSDiscardItem.index = knapsackInfo1.index;
                    //cSDiscardItem.item_id_in_client = knapsackInfo1.item_id;
                    //cSDiscardItem.item_num_in_client = knapsackInfo1.num;
                    //cSDiscardItem.discard_num = knapsackInfo1.num;
                    ////cSDiscardItem.discard_medthod
                    //Test.m_Channel.Send(cSDiscardItem);

                    CSUseItem cSUseItem = ReferencePool.Acquire<CSUseItem>();
                    cSUseItem.equip_index = (short)equipments.subtype;
                    cSUseItem.index = knapsackInfo1.Index;
                    cSUseItem.num = 1;
                    Test.m_Channel.Send(cSUseItem);
                    Debug.Log("穿戴装备");

                });
            }
            else
            {
                if (Bconsume!=null)
                {
                    go.transform.Find("item1").Find("Btn_Use").GetComponent<Button>().onClick.AddListener(() =>
                    {
                        //Destroy(go);
                        //icon.sprite = Resources.Load<Sprite>("bg_道具");
                        //numText.text = "";
                        CSUseItem cSUseItem = ReferencePool.Acquire<CSUseItem>();
                        //cSUseItem.equip_index = (short)Bconsume.sub_type;
                        cSUseItem.index = knapsackInfo1.Index;
                        cSUseItem.num = 1;
                        Test.m_Channel.Send(cSUseItem);
                    });
                }
                else if(Zconsume!=null)
                {
                    go.transform.Find("item1").Find("Btn_Use").GetComponent<Button>().onClick.AddListener(() =>
                    {
                        //Destroy(go);
                        //icon.sprite = Resources.Load<Sprite>("bg_道具");
                        //numText.text = "";
                        CSUseItem cSUseItem = ReferencePool.Acquire<CSUseItem>();
                        //cSUseItem.equip_index = (short)Zconsume.sub;
                        cSUseItem.index = knapsackInfo1.Index;
                        cSUseItem.num = 1;
                        Test.m_Channel.Send(cSUseItem);
                    });
                }
               
            }
            go.transform.Find("item1").Find("Btn_Throw").GetComponent<Button>().onClick.AddListener(() =>
            {
                CSDiscardItem cSDiscardItem = ReferencePool.Acquire<CSDiscardItem>();
                cSDiscardItem.index = knapsackInfo1.Index;
                cSDiscardItem.item_id_in_client = knapsackInfo1.ItemId;
                cSDiscardItem.item_num_in_client = knapsackInfo1.Num;
                cSDiscardItem.discard_num = knapsackInfo1.Num;
                //cSDiscardItem.discard_medthod
                Test.m_Channel.Send(cSDiscardItem);

            });
        });
    }
    internal void SetData(KnapsackItem knapsackInfo, BagForm bagForm)
    {
      //  Debug.Log(knapsackInfo.has_param + ":" + knapsackInfo.index + ":" + knapsackInfo.invalid_time + ":" + knapsackInfo.is_bind + ":" + knapsackInfo.item_id + ":" + knapsackInfo.num);

        if (knapsackInfo != null)
        {
            ishave = true;
        }
        else
        {
            ishave = false;
        }
        icon.gameObject.SetActive(ishave);
        numText.gameObject.SetActive(ishave);
        if (!ishave) return; ;
        knapsackInfo1 = knapsackInfo;
        numText.text = knapsackInfo.Num.ToString();
         gift1 = bagForm.bagmodle.FindGift1(knapsackInfo.ItemId);
         gift2 = bagForm.bagmodle.FindGift2(knapsackInfo.ItemId);
         Zconsume = bagForm.bagmodle.FindZconsume(knapsackInfo.ItemId);
         Bconsume = bagForm.bagmodle.FindBconsume(knapsackInfo.ItemId);
        equipments = bagForm.bagmodle.FindEquipments(knapsackInfo.ItemId);
        
        if (gift1!=null)
        {
            Debug.Log(gift1.name);

            Debug.Log("gift1");
        }
        else if (gift2 != null)
        {
            Debug.Log(gift2.name);

            Debug.Log("gift2");

        }
        else if (Zconsume != null)
        {
            Debug.Log(Zconsume.name);
            Debug.Log("Zconsume");
            var bytePath = AssetUtility.GetSpriteAsset("Item/Item_" + Zconsume.icon_id);

            var loadCallBack = new LoadAssetCallbacks((assetName, asset, duration, userData) =>
            {
                icon.sprite = asset as Sprite;
            });
            GameEntry.Resource.LoadAsset(bytePath, typeof(Sprite), loadCallBack);

            Debug.Log(bytePath+"===="+ loadCallBack);
        }
        else if(Bconsume != null)
        {
            Debug.Log(Bconsume.name);
            Debug.Log("Bconsume");
            var bytePath = AssetUtility.GetSpriteAsset("Item/Item_" + Bconsume.icon_id);

            var loadCallBack = new LoadAssetCallbacks((assetName, asset, duration, userData) =>
            {
                icon.sprite = asset as Sprite;
            });
            GameEntry.Resource.LoadAsset(bytePath, typeof(Sprite), loadCallBack);
            Debug.Log(222222222);


        }
        else if (equipments != null)
        {
            Debug.Log(equipments.name);
            Debug.Log("equipments.name");
            
            var bytePath = AssetUtility.GetSpriteAsset("Item/Item_" + equipments.iconid);

            var loadCallBack = new LoadAssetCallbacks((assetName, asset, duration, userData) =>
            {
                icon.sprite = asset as Sprite;
            });
            GameEntry.Resource.LoadAsset(bytePath, typeof(Sprite), loadCallBack);
            Debug.Log(333333);

        }
    }

   
}

