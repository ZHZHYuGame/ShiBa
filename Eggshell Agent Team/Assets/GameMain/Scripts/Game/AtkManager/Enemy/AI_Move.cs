using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Move : MonoBehaviour
{
    GameObject[] players;
    public float moveSpeed = 3f;//移动速度
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");//玩家列表
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (players[0] != null)
        {
            Vector3 direction = players[0].transform.position - transform.position;
            //transform.position += direction * moveSpeed * Time.deltaTime/10;
            rb.MovePosition(transform.position + direction * moveSpeed * Time.deltaTime / 10);
        }
    }
}
