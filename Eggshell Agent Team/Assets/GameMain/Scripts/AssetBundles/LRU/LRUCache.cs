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
    private readonly TimeSpan _timeThreshed;//时间阈值
    private readonly Dictionary<Tkey, CacheItem> _secondartCache;//二级缓存
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="capacity"></param>
    public LRUCache(int capacity,TimeSpan timeThresHold)
    {
        if (capacity < 0)
            
        _capacity = capacity;
        _timeThreshed = timeThresHold;
        _cacheMap = new Dictionary<Tkey, LinkedListNode<CacheItem>>();
        _lruList = new LinkedList<CacheItem>();
        _secondartCache = new Dictionary<Tkey, CacheItem>();
    }
    /// <summary>
    /// 从缓存中获取值
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool TryGetValue(Tkey key, out TValue value)
    {
        //先查找一级缓存
        if (_cacheMap.TryGetValue(key, out var node))
        {
            node.Value.LastAccessTime=DateTime.Now;
            // 如果缓存命中，将节点移动到链表头部（表示最近使用）
            value = node.Value.Value;
            _lruList.Remove(node);
            //添加到lruList
            _lruList.AddFirst(node);
            return true;
        }
        if (_secondartCache.TryGetValue(key, out var cacheItem))
        {
            // 将资源重新加载到一级缓存
            Add(key, cacheItem.Value);
            // 从二级缓存移除
            _secondartCache.Remove(key);
            value = cacheItem.Value;
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
            // 如果键已存在，更新值并移动到链表头部
            existingNode.Value.Value = value;
            existingNode.Value.LastAccessTime = DateTime.Now;
            // 如果键已存在，移除旧节点
            _lruList.Remove(existingNode);
            //添加到lruList
            _lruList.AddFirst(existingNode);
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
            // 将节点移动到二级缓存
            _secondartCache[lastnode.Value.Key] = lastnode.Value;
            _cacheMap.Remove(lastnode.Value.Key); // 从一级缓存移除
            _lruList.RemoveLast();//从链表移除
        }
    }
     /// <summary>
     /// 检查并移除超时未访问的节点
     /// </summary>
    public void CheckAndMoveExpiredNodes()
    {
        var current = _lruList.Last;
        while (current != null && current!=_lruList.First)
        {
            if (DateTime.Now - current.Value.LastAccessTime < _timeThreshed)
            {
                // 将节点移动到二级缓存
                _secondartCache[current.Value.Key] = current.Value;
                // 从一级缓存移除
                var prev = current.Previous;
                _cacheMap.Remove(current.Value.Key);
                _lruList.Remove(current);
                current = prev;
            }
            else
            {
                break;
            }
        }
    }
    /// <summary>
    /// 清除缓存
    /// </summary>
    public void Clear()
    {
        _cacheMap.Clear();
        _lruList.Clear();
        _secondartCache.Clear();
    }
    private class CacheItem
    {
        public Tkey Key { get; set; }
        public TValue Value { get; set; }
        public DateTime LastAccessTime { get; set; } // 最后访问时间
    }
}
