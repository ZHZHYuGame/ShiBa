using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Hurtwhele : MonoBehaviour
{
    ActiveSkill weapon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.collider.tag=="Enemy")
        {
            Monster moster = other.collider.GetComponent<Monster>();
            int atk = (int)(weapon.Level * weapon.Coefficient * weapon.Skill_hurt);
            //血量-(武器伤害-防御)
            int realAtk = (int)(atk - moster.data.Def);
            moster.data.Blood -= realAtk;
            GameObject hurt = ObjectPool.GetObject(GameObject.Find("Canvas").transform.GetChild(0).GetChild(0).gameObject);
            hurt.transform.position = Camera.main.WorldToScreenPoint(transform.position);
            hurt.GetComponent<HurtItem>().Init(realAtk);//伤害动态更新

        }
    }

    public void Init(ActiveSkill waepon)
    {
       this.weapon=waepon;
        if (weapon.Level >= 5)
        {
            //满级图片
            string assetName = Path.GetFileNameWithoutExtension(weapon.Slill_AfterIcon);
            this.GetComponent<SpriteRenderer>().sprite = ResourcesLoader.LoadResources<Sprite>(Application.streamingAssetsPath + "/weapon", assetName, "weapon");
        }
    }
}
