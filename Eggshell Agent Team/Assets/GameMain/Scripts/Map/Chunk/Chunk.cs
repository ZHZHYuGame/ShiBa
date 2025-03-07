using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChunkState
{
    Display,
    Cache,
    UnLoad
}
public class Chunk
{
    // 在块列表中所处的位置
    ChunkVector2 m_position;

    //块当前的状态
    public ChunkState m_currentState = ChunkState.UnLoad;

    Dictionary<uint, EntityBase> m_entityDic;

    public int m_count
    {
        get { return m_entityDic.Count; }
    }
    /// <summary>
    /// 创建一个块对象
    /// </summary>
    /// <param name="rowNum">在块列表中的第几行</param>
    /// <param name="colNum">在块列表中的第几列</param>
    public Chunk(int rowNum, int colNum)
    {
        m_position = new ChunkVector2(rowNum, colNum);
        m_entityDic = new Dictionary<uint, EntityBase>();
    }
    public Chunk(ChunkVector2 position) : this(position.rowNum, position.colNum)
    {
        m_position = new ChunkVector2(position.rowNum, position.colNum);
        m_entityDic = new Dictionary<uint, EntityBase>();
    }

    public void Display() { MonoThread.Instance.Excute(CoroutineDisplay()); }
    public void Cache() { MonoThread.Instance.Excute(CoroutineUnload()); }
    public void Unload() { MonoThread.Instance.Excute(CoroutineCache()); }

    //添加对象
    public void AddEntity(EntityBase entity)
    {
        if (m_entityDic.ContainsKey(entity.index)) return;
        m_entityDic.Add(entity.index, entity);
    }
    //移除
    public void RemoveEntity(uint index)
    {
        if (!m_entityDic.ContainsKey(index)) return;
            m_entityDic[index].Destory();
            m_entityDic.Remove(index);
    }


    //显示
    IEnumerator CoroutineDisplay()
    {
        foreach (var item in m_entityDic.Values)
        {
            yield return item;
            item.Show();
        }
    }
    //卸载
    IEnumerator CoroutineUnload()
    {
        //foreach (var item in m_entityDic.Values)
        //{
        //    item.Destory();
        //}
        //m_entityDic.Clear();
        yield return null;
    }
    //缓存
    IEnumerator CoroutineCache()
    {
        if(m_entityDic!=null&&m_entityDic.Count>0)
        {
            foreach (var item in m_entityDic.Values)
            {
                item.Hide();
            }
        }
        yield return null;

    }

    /// <summary>
    /// 更新自身状态
    /// </summary>
    /// <param name="state"></param>
    public void Update(ChunkState state)
    {
        if (m_currentState == state)
        {
            Debug.LogErrorFormat(" {0} is already {1} ", m_position, m_currentState);
            return;
        }
        switch (state)
        {
            case ChunkState.Display:
                Display();
                break;
            case ChunkState.Cache:
                Cache();
                break;
            case ChunkState.UnLoad:
                Unload();
                break;

        }
    }

}
