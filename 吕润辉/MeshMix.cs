using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MeshMix : MonoBehaviour
{
    public SkinnedMeshRenderer player;
    public AnimationClip clip;
    public Animator animator;
    bool isplay;
    
    List<Vector3> verList = new List<Vector3>();
    public Button btn;
    int num=0;
    public Texture2D anim;
    float max=1.5f;
    float min=-1f;
    int verNum;

    Mesh shadowmesh;
    GameObject shadow;

    void Start()
    {
        btn.onClick.AddListener(() =>
        {
            OnBtn();
        });
    }

    void Update()
    {
        if (isplay)
        {
            num++;
            if (num >= clip.length * 30)
            {
                isplay = false;
            }
            //Mesh mesh = new Mesh();
            //player.BakeMesh(mesh);
            //GameObject shadow = new GameObject("Shadow" + num);
            //shadow.transform.position = player.transform.position;
            //shadow.AddComponent<MeshFilter>().mesh = mesh;
            //shadow.AddComponent<MeshRenderer>().material = player.material;
            //foreach (var item in shadow.GetComponent<MeshRenderer>().materials)
            //{
            //    item.SetFloat("_Float", 0.15f);
            //}
            //Destroy(shadow, 0.2f);
            for (int i = 0; i < verNum; i++)
            {
                shadowmesh.vertices[i] = verList[num * verNum + i];
            }
            shadow.GetComponent<MeshFilter>().mesh=shadowmesh;
        }
    }
    public void OnBtn()
    {

        //AnimatorOverrideController aoc = new AnimatorOverrideController(animator.runtimeAnimatorController);
        //aoc["Default"] = clip;
        //animator.runtimeAnimatorController = aoc;
        //animator.SetTrigger("Jump");
        //num = 0;
        //isplay = true;
        //for (int i = 0; i < clip.length*30; i++)
        //{
        //    Mesh mesh=new Mesh();
        //    float t = i * 1f / 30f;
        //    clip.SampleAnimation(animator.gameObject,t);
        //    player.BakeMesh(mesh);
        //    GameObject shadow = new GameObject("Shadow" + num);
        //    shadow.transform.position = player.transform.position;
        //    shadow.AddComponent<MeshFilter>().mesh = mesh;
        //    shadow.AddComponent<MeshRenderer>().material = player.material;
        //    foreach (var item in shadow.GetComponent<MeshRenderer>().materials)
        //    {
        //        item.SetFloat("_Float", 0.15f);
        //    }
        //    Destroy(shadow, 0.2f);
        //}
        verNum = player.sharedMesh.vertexCount;
        float allNum = verNum * clip.length * 30;
        int size = Mathf.NextPowerOfTwo(Mathf.CeilToInt(Mathf.Sqrt(allNum)));
        Texture2D texture = new Texture2D(size, size);

        min = float.MaxValue;
        max = float.MinValue;
        for (int i = 0; i < clip.length * 30; i++)
        {
            Mesh mesh = new Mesh();
            float t = i * 1f / 30f;
            clip.SampleAnimation(animator.gameObject, t);
            player.BakeMesh(mesh);
            for (int j = 0; j < mesh.vertices.Length; j++)
            {
                Vector3 pos = mesh.vertices[j] + player.transform.position;
                max = Mathf.Max(max, pos.x, pos.y, pos.z);
                min = Mathf.Max(min, pos.x, pos.y, pos.z);
            }
        }
        for (int i = 0; i < clip.length * 30; i++)
        {
            Mesh mesh = new Mesh();
            float t = i * 1f / 30f;
            clip.SampleAnimation(animator.gameObject, t);
            player.BakeMesh(mesh);

            Debug.Log(mesh.vertices.Length);

            GameObject shadow = new GameObject("Shadow" + num);
            shadow.transform.position = player.transform.position;
            shadow.AddComponent<MeshFilter>().mesh = mesh;
            shadow.AddComponent<MeshRenderer>().material = player.material;

            for (int j = 0; j < mesh.vertices.Length; j++)
            {
                Vector3 pos = mesh.vertices[j] + player.transform.position;

                int x = (i * (int)(clip.length * 30) + j) % size;
                int y = (i * (int)(clip.length * 30) + j) / size;

                float cx = (pos.x - min) / (max - min);
                float cy = (pos.x - min) / (max - min);
                float cz = (pos.x - min) / (max - min);

                texture.SetPixel(x, y, new Color(cx, cy, cz));
                //GameObject xxx = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                //xxx.transform.position = pos;
                //xxx.transform.localScale = Vector3.one * 0.1f;
            }
        }
        texture.Apply();
        File.WriteAllBytes(Application.dataPath + "/" + clip.name + ".png", texture.EncodeToPNG());
        AssetDatabase.Refresh();
    }
    public void OnBtnJie(int index)
    {
        Debug.Log(anim.width);
        for (int i = 0; i < player.sharedMesh.vertexCount; i++)
        {
            int x = (index * player.sharedMesh.vertexCount + i) % anim.width;
            int y = (index * player.sharedMesh.vertexCount + i) / anim.width;
            Color color = anim.GetPixel(x, y);
            float cx = color.r * (max - min) + min;
            float cy = color.g * (max - min) + min;
            float cz = color.b * (max - min) + min;

            GameObject xxx = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            xxx.transform.position = new Vector3(cx, cy, cz) + Vector3.right * 2;
            xxx.transform.localScale = Vector3.one * 0.1f;
        }
    }
}
