using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//定义类 表示四叉树的节点
public class Node
{
    public Bounds bounds;           //节点的边界
    public int myDepth;             //节点的深度
    public Tree tree;               //所属的四叉树
    public List<ObjData> datas = new List<ObjData>();   //存储在该节点的游戏对象数据列表
    public Node[] childs;           //子节点数组
    public Vector2[] bif = new Vector2[]    //用于划分节点的二维向量数组
    {
        new Vector2 (-1, 1),
        new Vector2 (1, 1),
        new Vector2 (-1, -1),
        new Vector2 (1, -1),
    };
    public Node(Bounds bound, int myDepth, Tree tree)
    {
        this.bounds = bound;
        this.myDepth = myDepth;
        this.tree = tree;
    }
    /// <summary>
    /// 插入游戏对象数据到节点
    /// </summary>
    /// <param name="data">游戏对象数据</param>
    public void InserData(ObjData data)
    {
        //如果这个节点深度小于最大深度且没有子节点 则创建子节点
        if (myDepth < tree.maxDepth && childs == null)
        {
            creatChild();
        }
        //如果有子节点
        if (childs != null)
        {
            //遍历子节点
            for (int i = 0; i < childs.Length; i++)
            {
                //如果子节点的边界包含游戏对象的位置
                if (childs[i].bounds.Contains(data.pos))
                {
                    //将数据插入到子节点中
                    childs[i].InserData(data);
                    break;
                }
            }
        }
        else
        {
            //如果没有子节点,将数据添加到当前节点的数据列表中
            datas.Add(data);
        }
    }
    /// <summary>
    /// 创建子节点
    /// </summary>
    private void creatChild()
    {
        //初始化子节点数组
        childs = new Node[tree.maxChildCount];
        //遍历子节点
        for (int i = 0; i < tree.maxChildCount; i++)
        {
            //计算子节点的中心相对坐标
            Vector3 center = new Vector3(bif[i].x * bounds.size.x / 4, 0, bif[i].y * bounds.size.z / 4);
            //计算子节点的大小
            Vector3 size = new Vector3(bounds.size.x / 2, 0, bounds.size.z / 2);
            //创建子节点的边界
            Bounds childBound = new Bounds(center + bounds.center, size);
            //创建子节点并赋值
            childs[i] = new Node(childBound, myDepth + 1, tree);
        }
    }
    /// <summary>
    /// 绘制节点的边界
    /// </summary>
    public void DrawBound()
    {
        //有数据画蓝色框
        if (datas.Count != 0)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(bounds.center, bounds.size - Vector3.one * 0.1f);
        }
        //没有数据的画绿色框
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(bounds.center, bounds.size - Vector3.one * 0.1f);
        }
        //如果有子节点,递归调用DrawBound方法绘制子节点的边界
        if (childs != null)
        {
            for (int i = 0; i < childs.Length; i++)
            {
                childs[i].DrawBound();
            }
        }
    }
    /// <summary>
    /// 根据视锥体的六个面判断节点内的游戏对象是否显示
    /// </summary>
    /// <param name="planes"></param>
    public void TriggerMove(Plane[] planes)
    {
        //如果有子节点,递归调用TriigerMove方法
        if (childs != null)
        {
            for (int i = 0; i < childs.Length; i++)
            {
                childs[i].TriggerMove(planes);
            }
        }
        //遍历节点内的游戏对象数据
        for (int i = 0; i < datas.Count; i++)
        {
            //判断节点的边界是否在视锥体内,决定游戏对象是否显示
            datas[i].prefab.SetActive(GeometryUtility.TestPlanesAABB(planes, bounds));
        }
    }
}
