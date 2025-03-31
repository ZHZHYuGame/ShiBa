 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario : MonoBehaviour
{
    SpriteRenderer sprite;//2dͼƬ
    Rigidbody2D rigidbody2D;//2d��ײ
    BoxCollider2D  boxCollider2D;
    bool Iscanjump = false;
    bool isDeath = false;
    Animator ani;
    float x;
    
    // Start is called before the first frame update
    void Start()
    {
        sprite=GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    //private void FixedUpdate()
    //{
    //    x = Input.GetAxis("Horizontal");
    //    if (x > 0)
    //    {
    //        sprite.flipX = false;
    //        transform.Translate(Vector3.right * 5 * Time.deltaTime);
    //    }
    //    else if (x < 0)
    //    {
    //        sprite.flipX = true;
    //        transform.Translate(Vector3.left * 5 * Time.deltaTime);
    //    }
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        rigidbody2D.AddForce(Vector3.up * 6, ForceMode2D.Impulse);
    //    }
    //}
    // Update is called once per frame
    void Update()
    {
        if (!isDeath)
        {
            x = Input.GetAxis("Horizontal");
            if (x > 0)
            {
                sprite.flipX = false;
                transform.Translate(Vector3.right * 5 * Time.deltaTime);
            }
            else if (x < 0)
            {
                sprite.flipX = true;
                transform.Translate(Vector3.left * 5 * Time.deltaTime);
            }
            if (x != 0)
            {
                ani.SetBool("Move", true);
            }
            else
            {
                ani.SetBool("Move", false);
            }
            if (Input.GetKeyDown("space"))
            {
                if (Iscanjump)
                {
                    rigidbody2D.AddForce(Vector3.up * 8, ForceMode2D.Impulse);
                    Music.Ins.PlayDeath(AudioType.Jump);
                }
            }
            if(Input.GetKeyDown(KeyCode.J))
            {

                GameObject fire = Instantiate(Resources.Load<GameObject>("fire"));
                int n = 0;
                if (sprite.flipX==false)
                {
                    fire.transform.position = transform.position + Vector3.right * 1f;
                    n = 5;
                }
                else 
                {
                    fire.transform.position = transform.position + Vector3.left * 1f;
                    n = -5;
                }
                fire.AddComponent<MarioFire>().Init(n);
            }
        }
        
    }
    //�����ڵ�����ײ���Ӽ�⣬ͨ����������ײ����transform��tag��ǩΪground
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "ground")
        {
            Iscanjump = true;
            ani.SetBool("Jump", false);
        }
        if (collision.transform.tag == "enemy")
        {
            rigidbody2D.AddForce(Vector3.up *7, ForceMode2D.Impulse);
            ani.SetTrigger("Death");
            boxCollider2D.isTrigger = true;
            Music.Ins.PlayDeath(AudioType.death);
            isDeath = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "ground")
        {
            Iscanjump = false;
            ani.SetBool("Jump", true);
        }
    }
}
public enum AudioType
{
    bg,
    Jump,
    death,
    eatCoin,
    eatBig,
    small
}
