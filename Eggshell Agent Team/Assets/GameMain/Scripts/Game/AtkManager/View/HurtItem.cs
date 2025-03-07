using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HurtItem : MonoBehaviour
{
  


    public void Init(string str)
    {

        GetComponent<Text>().text = str;
        Invoke("SaveObjectPool", 1f);
    }
    // Start is called before the first frame update
    void Start()
    {
    

    }

    void SaveObjectPool()
    {
        ObjectPool.Enqueue(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(enabled)
        {
            transform.GetComponent<RectTransform>().anchoredPosition += Vector2.up * Time.deltaTime;
        }
        
    }
}
