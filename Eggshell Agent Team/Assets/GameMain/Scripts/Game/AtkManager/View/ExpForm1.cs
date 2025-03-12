using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpForm1 : MonoBehaviour
{
    public static ExpForm1 Instance;
    public Scrollbar scrollbar;
    public Text levelText;
    Exp exp;
    int allExp;
    int level = 0;
    int nowLevelMaxExp;


    internal void SetData(Exp exp)
    {
        this.exp = exp;
    }

    private void Awake()
    {
        Instance = this;
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
