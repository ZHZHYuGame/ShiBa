using ConfigTools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SighCellView : MonoBehaviour
{
    public Image icon;
    public GameObject bg,bu,qian,mask;

    internal void SetData(object value)
    {
        bg.gameObject.SetActive(false);
        bu.gameObject.SetActive(false);
        qian.gameObject.SetActive(false);
        mask.gameObject.SetActive(false);
        if (value != null)
        {
            bg.gameObject.SetActive(true);
            SighData bcon = (SighData)value;
            icon.sprite = Resources.Load<Sprite>("Item/Item_" + bcon.signIn.reward_item);
            if (SighModel.Instance.day > bcon.day)
            {
                if (!bcon.ist)
                {
                    bu.SetActive(true);
                }
                else
                {
                    mask.SetActive(true);
                }
            }
            else if (SighModel.Instance.day < bcon.day)
            {
                bu.SetActive(false);
                
            }
            else
            {
                if (bcon.ist)
                {
                    mask.SetActive(true);
                }
                else
                {
                    qian.SetActive(true);
                }
                
            }
        }
        else
        {

            bg.gameObject.SetActive(false);
            icon.sprite = null;
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
