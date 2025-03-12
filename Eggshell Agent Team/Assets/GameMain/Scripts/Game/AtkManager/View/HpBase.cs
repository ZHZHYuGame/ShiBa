using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBase : MonoBehaviour
{
    GameObject m_player;
    Role m_role;
    internal void Init(GameObject player, Role role)
    {
        m_player = player;
        m_role = role;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(m_player.transform.position+Vector3.down*0.6f);

    }
}
