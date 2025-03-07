using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家移动
/// </summary>
public class PlayerRole : MonoBehaviour
{
    public ETC etc;
    public float moveSpeed = 0.5f;
    private Vector3 lastMoveDirection = Vector3.forward;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] etcs = GameObject.FindGameObjectsWithTag("Etc");
        etc = etcs[0].GetComponent<ETC>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = etc.GetDis("h");
        float v = etc.GetDis("v");
        

        Vector3 pos = new Vector3(h* moveSpeed, v* moveSpeed, 0);
        Vector3 moveDrection = Camera.main.transform.TransformDirection(pos);
        moveDrection.z = 0;
        moveDrection.Normalize();//向量归一化
        if (pos != Vector3.zero)
        {
            transform.position += pos * Time.deltaTime * 5;
            float angle = Mathf.Atan2(moveDrection.y,moveDrection.x)*Mathf.Rad2Deg;//计算子弹发射角度
            
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //武器等级升级
            transform.GetComponent<WeaponOrbitController>().currentLevel++;

        }
    }
}
