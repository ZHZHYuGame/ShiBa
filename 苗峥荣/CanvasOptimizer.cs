using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasOptimizer : MonoBehaviour
{
    public Canvas[] canvasesToMerge;

    private void Start()
    {
        if (canvasesToMerge.Length > 0)
        {
            Canvas mainCanvas = canvasesToMerge[0];
            for (int i = 1; i < canvasesToMerge.Length; i++)
            {
                Transform[] children = new Transform[canvasesToMerge[i].transform.childCount];
                for (int j = 0; j < children.Length; j++)
                {
                    children[j] = canvasesToMerge[i].transform.GetChild(j);
                    children[j].SetParent(mainCanvas.transform, true);
                }
                Destroy(canvasesToMerge[i].gameObject);
            }
        }
    }
}
