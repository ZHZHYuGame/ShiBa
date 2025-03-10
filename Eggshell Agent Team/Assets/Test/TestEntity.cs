using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEntity : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("CreateCube", 0.1f);
    }
    private void CreateCube()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Vector2 pos = Random.insideUnitCircle * 30;
            obj.transform.position = new Vector3(pos.x, pos.y, 0);
            EntityBase entity = new EntityBase(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
