using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    
     Role currenterData;


    public void Init(Role data)
    { 
        currenterData= data;
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
            currenterData.Blood -= 10;
           
            transform.GetComponent<SpriteRenderer>().color = Color.red;
           GameObject hurt=  ObjectPool.GetObject(GameObject.Find("Canvas").transform.GetChild(0).GetChild(0).gameObject);
            hurt.transform.position=Camera.main.WorldToScreenPoint(transform.position);
            hurt.GetComponent<HurtItem>().Init("-10");
            //if (currenterData.Blood <= 0)
            //{
            //    transform.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("die2");
            //}
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (currenterData.Blood > 0)
        {
            transform.GetComponent<SpriteRenderer>().color = Color.white;
        }
        else
        {
            transform.GetComponent<SpriteRenderer>().color = Color.white;
            // Destroy(this.gameObject);
            ObjectPool.Enqueue(this.gameObject);
            //经验球掉落

        }

    }
}
