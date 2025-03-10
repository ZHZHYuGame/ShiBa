using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioce : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioClip clip = Resources.Load<AudioClip>("music_about");
            AudioMgr.Instance.PlayMusic(clip,0.5f);
           
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            AudioClip clip = Resources.Load<AudioClip>("music_about");
            AudioMgr.Instance.PlaySFX(clip);
           
        }
    }
}
