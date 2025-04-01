using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//定义一个类 表示四叉树
public class Tree
{
    public Bounds bounds;           //四叉树的边界
    public Node root;               //四叉树的根节点
    public int maxDepth = 6;        //四叉树的最大深度
    public int maxChildCount = 4;   //每个节点的最大子节点数
    public Tree(Bounds bound)
    {
        this.bounds = bound;
        this.root = new Node(bound, 0, this);
    }
    //插入游戏对象数据到四叉树
    public void InserData(ObjData data)
    {
        //调用根节点的InserData方法插入数据
        root.InserData(data);
    }
    //绘制四叉树的边界
    public void DrawBound()
    {
        //调用根节点的DrawBound方法绘制边界
        root.DrawBound();
    }
    //根据视锥体的六个面判断四叉树内的游戏对象是否显示
    public void TriggerMove(Plane[] planes)
    {
        //调用根节点的TriigerMove方法判断游戏对象是否显示
        root.TriggerMove(planes);
    }
}
