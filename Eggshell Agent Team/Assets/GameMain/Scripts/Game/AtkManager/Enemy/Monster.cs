
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    Role data;//怪物属性
    RefreshWaves refreshWaves;//波次属性

    public void Init(Role data, RefreshWaves refreshWaves)
    { 
        this.data= data;
        this.refreshWaves = refreshWaves;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            data.Blood -= 10;
           
            transform.GetComponent<SpriteRenderer>().color = Color.red;
           GameObject hurt=  ObjectPool.GetObject(GameObject.Find("Canvas").transform.GetChild(0).GetChild(0).gameObject);
            hurt.transform.position=Camera.main.WorldToScreenPoint(transform.position);
            hurt.GetComponent<HurtItem>().Init("-10");//伤害动态更新
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (data.Blood > 0)
        {
            transform.GetComponent<SpriteRenderer>().color = Color.white;
        }
        else
        {
            transform.GetComponent<SpriteRenderer>().color = Color.white;
            // Destroy(this.gameObject);
            ObjectPool.Enqueue(this.gameObject);
            //经验球掉落 根据怪物波次进行经验球的掉落
            GameObject expPrefab = ObjectPool.GetObject(GameObject.Find("ExpPool").transform.GetChild(0).gameObject);
            expPrefab.transform.position = transform.position;
            List<Exp> exps = ConfigMgr.GetTable<List<Exp>>("ExpData");
            switch (refreshWaves.ExpType)
            {
                case ExpType.lowerExp:
                    expPrefab.GetComponent<SpriteRenderer>().sprite = AssetDatabase.LoadAssetAtPath<Sprite>(exps[0].Exp_path);
                    //属性设置
                    expPrefab.GetComponent<ExpPrefab>().Init(exps[0]);
                    break;
                case ExpType.midelExp:
                    expPrefab.GetComponent<SpriteRenderer>().sprite = AssetDatabase.LoadAssetAtPath<Sprite>(exps[1].Exp_path);
                    expPrefab.GetComponent<ExpPrefab>().Init(exps[0]);
                    break;
                case ExpType.higherExp:
                    expPrefab.GetComponent<SpriteRenderer>().sprite = AssetDatabase.LoadAssetAtPath<Sprite>(exps[2].Exp_path);
                    expPrefab.GetComponent<ExpPrefab>().Init(exps[0]);
                    break;
            }

        }

    }
}
