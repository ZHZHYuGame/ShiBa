using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorCheck : MonoBehaviour
{
    public GameObject A;
    public GameObject B;
    public float Ang;
    public float AtanAng;
    void Start()
    {

    }

    void Update()
    {
        Vector3 pos = B.transform.position - A.transform.position;
        Ang = Vector3.Angle(A.transform.forward, pos);
        float dis = Vector3.Distance(B.transform.position, A.transform.position);
    }
}
