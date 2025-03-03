using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//子弹脚本
public class Projectile : MonoBehaviour
{
    [Range(0, 100)]
    [SerializeField]
    private int hurt;//伤害

    [Range(0, 100)]
    [SerializeField]
    private float speed;//速度

    private GameObject enemy;//敌人对象

    [Range(0, 10)]
    [SerializeField]
    private float angle_differ;//允许的差异角度

    [Range(0, 2)]
    [SerializeField]
    private float angle_fix;//每次修正的角度

    [Range(0, 100)]
    [SerializeField]
    private float dis_time;//消失时间
    [Range(0, 50)]
    [SerializeField]
    private float distance =50;//消失半径
    float time = 0;

    private void Start()
    {
        
    }
    private void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
        //检测到敌人时则执行追踪
        if (enemy != null)
        {
            //对需要追踪物体和自身间的角度求解运算
            Vector2 row = (enemy.transform.position - transform.position).normalized;
            //获取两物体间夹角
            float angle1 = Vector3.SignedAngle(Vector3.up, row, Vector3.forward);
            //将夹角坐标和世界坐标的取值和范围对齐
            angle1 = (angle1 + 270) % 360;
            //获取弹幕自身世界坐标
            float angle2 = transform.eulerAngles.z % 360;
            //获取两个角度间的差异值并标准化
            float angle3 = ((angle1 - angle2) + 360) % 360;
 
 
            //对物体的角度做修正，使得物体x轴指向需要追踪的目标
            //朝向需要追踪对象的方向调整角度，按照设定的值进行调整
            if (angle3 < 180 - angle_differ)
            {
                Quaternion reAngle = Quaternion.Euler(0, 0, transform.eulerAngles.z - angle_fix);
                transform.rotation = reAngle;
            }
            else if (angle3 > 180 + angle_differ)
            {
                Quaternion reAngle = Quaternion.Euler(0, 0, transform.eulerAngles.z + angle_fix);
                transform.rotation = reAngle;
            }
 
        }
        OnArea();
    }
    private void OnArea()
    {

        //相对上面的方式能够减少性能开销，而且能指定选择的对象
        GameObject[] games = GameObject.FindGameObjectsWithTag("Enemy");
        //设置当前最近需要追踪对象的距离，如果结束后这个值没有变说明范围内没有敌人
        float distance = this.distance;
        foreach (GameObject game in games)
        {
            //可以在此基础上发展追踪优先级，的比如同距离优先锁定BOSS，或者不能被追踪的敌人对象
            if (game.GetComponent<AI_Move>() != null)
            {
                //找到场景中指定为敌人的对象，进行距离求值运算
                float dis = Vector2.Distance(new Vector2(game.transform.position.x, game.transform.position.y),
                    new Vector2(gameObject.transform.position.x, gameObject.transform.position.y));
                if (distance > dis)
                {
                    //将最小距离的GameObject对象赋值给需要追踪的对象enemy，会不断的循环更替，最终结束循环的时候
                    //筛选出来的enemy就是距离这个弹幕最近的敌人
                    distance = dis;
                    enemy = game;
                }
            }
        }
        time += Time.deltaTime;
        if (time >= 4)
        {
            //如果没有找到或者飞行过程中脱离了最大追踪距离，放入对象池中
            ObjectPool.Enqueue(gameObject);
            time = 0;
            
        }
    }
    //碰撞消失
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            ObjectPool.Enqueue(gameObject);
        }
    }
}
