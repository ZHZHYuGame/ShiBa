using UnityEngine;
using System.Collections.Generic;
using System;

public class ChunkController : MonoSingleton<ChunkController>
{
    //
    public int m_row=-1;

    public int m_col=-1;


    //所有的块
    Dictionary<ChunkVector2, Chunk> m_chunkMap;

    //玩家
    [SerializeField]
    Transform m_player;

    //玩家所在块位置
    ChunkVector2 m_currentPos;

    //预加载的地图块列表
    [SerializeField]
    HashSet<ChunkVector2> expectChunkVectorList = new HashSet<ChunkVector2>();

    //显示加载的块列表
    [SerializeField]
    HashSet<Chunk> m_currentChunkList = new HashSet<Chunk>();

    //单个地图块的边长
    [SerializeField]
    float m_chunkLength = 10;

    [SerializeField]
    private int m_loadRange = 1; // 1 表示九宫格，2 表示 5x5，以此类推

    void Start()
    {
        m_chunkMap = new Dictionary<ChunkVector2, Chunk>();
        // 初始化玩家所在的块
        InitMap();
    }


    protected void InitMap()
    {
        // 先确定玩家位置，得到玩家所在块的位置  
        ChunkVector2 currentPos = GetCurrentChunkVector(m_player.position);
        m_currentPos = currentPos;
        // 利用块的位置获取实际块列表
        List<ChunkVector2> actChunkList = GetActualChunkList(currentPos);
        // 再加载实际列表中的所有块
        UpdateCurrentChunkList(actChunkList, currentPos);
    }

    void Update()
    {
        // 检测玩家是否移动到了新的块
        var realtimePos = GetCurrentChunkVector(m_player.position);
        if (IsChange(realtimePos))
        {
            var list = GetActualChunkList(realtimePos);
            UpdateCurrentChunkList(list, realtimePos);
        }
        // 延迟调用 Resources.UnloadUnusedAssets
        if (Time.time - _lastUnloadTime > UnloadInterval)
        {
            Resources.UnloadUnusedAssets();
            _lastUnloadTime = Time.time;
        }
    }

    //预加载
    private void PreloadChunks(ChunkVector2 currentPos)
    {
        var preloadList = GetPreloadChunks(currentPos);
    }
    //获取预加载的块列表
    private List<ChunkVector2> GetPreloadChunks(ChunkVector2 currentPos)
    {
        List<ChunkVector2> preloadList = new List<ChunkVector2>();
        int preloadRange = m_loadRange + 1;
        for (int i = -preloadRange; i <= preloadRange; i++)
        {
            for (int j = -preloadRange; j <= preloadRange; j++)
            {
                int row = currentPos.rowNum + i;
                int col = currentPos.colNum + j;
                preloadList.Add(new ChunkVector2(row, col));
            }
        }
        return preloadList;
    }

    /// <summary>
    /// 玩家所在块是否发生改变
    /// </summary>
    /// <param name="realtimePos"></param>
    /// <returns></returns>
    bool IsChange(ChunkVector2 realtimePos)
    {
        if (m_currentPos != realtimePos)
        {
            m_currentPos = realtimePos;
            return true;
        }
        return false;
    }


    /// <summary>
    /// 获取实际块列表
    /// </summary>
    /// <param name="currentVector">当前中心块位置</param>
    /// <returns></returns>

    List<ChunkVector2> GetActualChunkList(ChunkVector2 currentPos)
    {
        List<ChunkVector2> expectChunkPosList = new List<ChunkVector2>();
        int currentRow = currentPos.rowNum;
        int currentCol = currentPos.colNum;

        for (int i = -m_loadRange; i <= m_loadRange; i++)
        {
            for (int j = -m_loadRange; j <= m_loadRange; j++)
            {
                int expRow = currentRow + i;
                int expCol = currentCol + j;
                expectChunkPosList.Add(new ChunkVector2(expRow, expCol));
            }
        }
        return expectChunkPosList;
    }

    private float _lastUnloadTime;
    private const float UnloadInterval = 10f; // 每 10 秒调用一次

    /// <summary>
    /// 更新当前块列表
    /// </summary>
    /// <param name="actulChunkList">实际块列表</param>
    /// <param name="currentPos">当前中心块位置</param>
    private void UpdateCurrentChunkList(List<ChunkVector2> actulChunkList, ChunkVector2 currentPos)
    {
        // 卸载不再需要的块
        UnloadUnnecessaryChunks(actulChunkList);
        //加载新的块
        LoadNewChunks(actulChunkList, currentPos);
        //统一更新所有块的状态
        UpdateChunkStates(currentPos);
        #region List更新块列表
        //for (int i = m_currentChunkList.Count - 1; i >= 0; i--)
        //{
        //    ChunkVector2 pos = m_currentChunkList[i];
        //    if (!actulChunkList.Contains(pos))
        //    {
        //        if (m_chunkMap.ContainsKey(pos))
        //        {
        //            m_chunkMap[pos].Unload();
        //            m_chunkMap.Remove(pos);
        //        }
        //        m_currentChunkList.RemoveAt(i);// 移除当前块列表中不存在与实际块列表的块
        //    }
        //    else
        //    {
        //        actulChunkList.Remove(pos);// 实际块列表移除和当前块列表中相同的元素
        //         // 更新块的状态
        //        ChunkState actualState = GetChunkStateByRelativePosition(pos, currentPos);
        //        m_chunkMap[pos].Update(actualState);
        //    }
        //}
        //for (int i = 0; i < actulChunkList.Count; i++)
        //{
        //    ChunkVector2 pos = actulChunkList[i];
        //    if (!m_chunkMap.ContainsKey(pos)) // 如果块不存在，则创建新的块
        //    {
        //        Chunk newChunk = new Chunk(pos); // 创建新块
        //        m_chunkMap[pos] = newChunk; // 添加到字典中
        //    }

        //    // 更新块的状态
        //    ChunkState actualState = GetChunkStateByRelativePosition(pos, currentPos);
        //    m_chunkMap[pos].Update(actualState);

        //    m_currentChunkList.Add(pos); // 添加到当前块列表
        //}
        #endregion
    }

    private void UpdateChunkStates(ChunkVector2 currentPos)
    {
        foreach (var pos in m_currentChunkList)
        {
            ChunkState actualState = GetChunkStateByRelativePosition(pos.m_position, currentPos);
            if (m_chunkMap[pos.m_position].m_currentState != actualState) // 仅当状态不同时更新
            {
                m_chunkMap[pos.m_position].Update(actualState);
            }
        }
    }
    //加载地图块
    private void LoadNewChunks(List<ChunkVector2> actualChunkList, ChunkVector2 currentPos)
    {
        foreach (var pos in actualChunkList)
        {
            if (!m_chunkMap.ContainsKey(pos))
            {
                Debug.Log($"加载新的地图块 ({pos.rowNum}, {pos.colNum})");
                Chunk newChunk = new Chunk(pos);
                m_chunkMap[pos] = newChunk;
            }
            m_currentChunkList.Add(m_chunkMap[pos]);
        }
    }
    //卸载
    private void UnloadUnnecessaryChunks(List<ChunkVector2> actualChunkList)
    {
        var chunksToRemove = new HashSet<Chunk>();
        foreach (var pos in m_currentChunkList)
        {
            if (!actualChunkList.Contains(pos.m_position)&& m_chunkMap.ContainsKey(pos.m_position))

            {
                if (CanUnloadChunk(pos.m_position)) // 检查块是否可以安全卸载
                {
                    chunksToRemove.Add(pos);
                }
            }
        }
        foreach (var pos in chunksToRemove)
        {
            Debug.Log($"卸载的地图块：({pos.m_position})");
            m_chunkMap[pos.m_position].Unload();
            m_chunkMap.Remove(pos.m_position);
        }
        m_currentChunkList.ExceptWith(chunksToRemove); // 批量移除
    }

    private bool CanUnloadChunk(ChunkVector2 pos)
    {
        if (m_chunkMap.ContainsKey(pos))
        {
            return m_chunkMap[pos].m_count == 0;
        }
        return true;
    }



    /// <summary>
    /// 获取块坐标
    /// </summary>
    /// <param name="position">玩家的具体vector3位置</param>
    /// <returns></returns>
    public ChunkVector2 GetCurrentChunkVector(Vector3 position)
    {
        int col = Mathf.FloorToInt(position.x / m_chunkLength);
        int row = Mathf.FloorToInt(position.y / m_chunkLength);
        return new ChunkVector2(row, col);
    }

    /// <summary>
    /// 获取或创建块
    /// </summary>
    public Chunk GetOrCreateChunk(ChunkVector2 chunkPos)
    {
        if (!m_chunkMap.ContainsKey(chunkPos))
        {
            m_chunkMap[chunkPos] = new Chunk(chunkPos);
        }
        return m_chunkMap[chunkPos];
    }

    /// <summary>
    /// 获取指定块的相对状态
    /// </summary>
    /// <param name="specified">指定块</param>
    /// <param name="relativeVector">参照块坐标</param>
    /// <returns>相对块状态</returns>
    ChunkState GetChunkStateByRelativePosition(ChunkVector2 specified, ChunkVector2 relative)
    {
        int rowAmount = Mathf.Abs(specified.rowNum - relative.rowNum);
        int colAmount = Mathf.Abs(specified.colNum - relative.colNum);

        if (rowAmount > 2 || colAmount > 2)
        {
            return ChunkState.UnLoad;
        }
        if (rowAmount == 2 || colAmount == 2)
        {
            return ChunkState.Cache;
        }
        return ChunkState.Display; // 其他情况均为 Display
    }
}