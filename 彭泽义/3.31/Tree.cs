
using UnityEngine;

public class Tree : INode//实现INode接口
{

    public Bounds bound { get; set; }//树的边界
    private Node root;//根节点
    public int maxDepth { get;}//最大深度
    public int maxChildCount { get; }//每个节点的最大子节点数
    public  Tree(Bounds bound)
    {
        this.bound = bound;
        this.maxDepth = 5;//设置最大深度为5(可根据需求进行调整)
        this.maxChildCount = 4;//设置每个节点的最大子节点数为4(可根据需求进行调整)
        root=new Node(bound,0,this);//创建根节点
    }
    public void InsertObj(ObjData obj)
    {
       root.InsertObj(obj);//在根节点中插入物体数据
    }
    public void DrawBound()
    {
        root.DrawBound();//绘制根节点的边界及其子节点的边界
    }

 

    public void TriggerMove(Camera camera)
    {
       root.TriggerMove(camera);//触发移动事件，刷新树节点和物体数据
    }
}
