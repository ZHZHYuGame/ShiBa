using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenEnemy : MonoBehaviour
{
    public List<GameObject> lisEnemy = new List<GameObject>();
    private float time;
    private Pool pool;//获取敌人对象池
    // Start is called before the first frame update
    void Start()
    {
        pool = GetComponent<Pool>();
    }

    // Update is called once per frame
    void Update()
    {

        //按键生成敌人
        if (Input.GetKeyDown(KeyCode.K))
        {
            //对象池取出对象
            GameObject enemyGo = pool.Pop();
            //设置为激活状态
            enemyGo.SetActive(true);
            //加入存活集合来进行管理
            lisEnemy.Add(enemyGo);

        }

    }
}