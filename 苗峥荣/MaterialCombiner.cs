using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialCombiner : MonoBehaviour
{
    public Image[] imagesToCombine;

    private void Start()
    {
        if (imagesToCombine.Length > 0)
        {
            Material sharedMaterial = imagesToCombine[0].material;
            for (int i = 1; i < imagesToCombine.Length; i++)
            {
                imagesToCombine[i].material = sharedMaterial;
            }
        }
    }
}
