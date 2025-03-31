using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public float CameraSensitivity = 1;//相机灵敏度
    public float CameraSpeed = 1;//相机移动速度
    GameObject player;//角色物体
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("HeroKnight");//找到角色物体组件
    }

    // Update is called once per frame
    void Update()
    {
        //获取相机与角色的位置向量，两者越远向量越大，相机移动的越快
        Vector3 v = player.transform.position - transform.position;
        v.z = 0;//冻结z不让相机穿过画面,导致物体看不见
        if (v.magnitude > 0.5 * CameraSensitivity)
        {

            transform.Translate(v * Time.deltaTime * CameraSpeed * 1.2f);
        }
    }
}