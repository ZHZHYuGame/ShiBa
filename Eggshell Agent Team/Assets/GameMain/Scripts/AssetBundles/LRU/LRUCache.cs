using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// LRU缓存
/// </summary>
public class LRUCache<Tkey, TValue>
{
    //缓存最大容量
    private readonly int _capacity;
    //快速查找缓存内容
    private readonly Dictionary<Tkey, LinkedListNode<CacheItem>> _cacheMap;
    private readonly LinkedList<CacheItem> _lruList;
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="capacity"></param>
    public LRUCache(int capacity)
    {
        if (capacity < 0)
            throw new ArgumentException("Capacity must be greater than 0.");
        _capacity = capacity;
        _cacheMap = new Dictionary<Tkey, LinkedListNode<CacheItem>>();
        _lruList = new LinkedList<CacheItem>();
    }
    /// <summary>
    /// 从缓存中获取值
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool TryGetValue(Tkey key, out TValue value)
    {
        if (_cacheMap.TryGetValue(key, out var node))
        {
            // 如果缓存命中，将节点移动到链表头部（表示最近使用）
            value = node.Value.Value;
            _lruList.Remove(node);
            //添加到lruList
            _lruList.AddFirst(node);
            return true;
        }
        value = default;
        return false;
    }
    /// <summary>
    /// 添加缓存项
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void Add(Tkey key, TValue value)
    {
        if (_cacheMap.TryGetValue(key, out var existingNode))
        {
            // 如果键已存在，移除旧节点
            _lruList.Remove(existingNode);
        }
        else if (_cacheMap.Count >= _capacity)
        {
            // 如果缓存已满，移除最久未使用的节点
            RemoveLastUsed();
        }
        // 创建新节点并添加到链表头部
        var cacheItem = new CacheItem { Key = key, Value = value };
        var newNode = new LinkedListNode<CacheItem>(cacheItem);
        _lruList.AddFirst(newNode);
        _cacheMap[key] = newNode;
    }
    public void Remove(Tkey key)
    {
        if (_cacheMap.TryGetValue(key, out var node))
        {
            _lruList.Remove(node);
            _cacheMap.Remove(key);
        }

    }
    /// <summary>
    /// 移除长时间未使用的节点
    /// </summary>
    private void RemoveLastUsed()
    {
        var lastnode = _lruList.Last;
        if (lastnode != null)
        {
            _cacheMap.Remove(lastnode.Value.Key);//从字典中移除
            _lruList.RemoveLast();//从链表移除
        }
    }
    /// <summary>
    /// 清除缓存
    /// </summary>
    public void Clear()
    {
        _cacheMap.Clear();
        _lruList.Clear();
    }
    private class CacheItem
    {
        public Tkey Key { get; set; }
        public TValue Value { get; set; }
    }
}
