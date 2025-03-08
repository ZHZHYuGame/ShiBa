using UnityEngine;
using System.Collections.Generic;
using System;

public class ChunkController : MonoSingleton<ChunkController>
{
    //地图块数量
    public int m_row = -1;
    public int m_col = -1;

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

    int oneMapScale;

    [SerializeField]
    private int m_loadRange = 1; // 1 表示九宫格，2 表示 5x5，以此类推

    void Start()
    {
        oneMapScale = MapManager.Instance.oneMapScale;
        m_chunkMap = new Dictionary<ChunkVector2, Chunk>();
        InitMap();
    }

    protected void InitMap()
    {
        for (int i = 0; i < m_row; i++)
        {
            for (int j = 0; j < m_col; j++)
            {
                Vector3 position = new Vector3(j * m_chunkLength* oneMapScale, 0, i * m_chunkLength* oneMapScale);
                ChunkVector2 chunkPos = new ChunkVector2(i, j);
                Chunk newChunk = new Chunk(chunkPos);
                m_chunkMap[chunkPos] = newChunk;
            }
        }

        // 初始化玩家所在的块
        m_currentPos = GetCurrentChunkVector(m_player.position);
        var list = GetActualChunkList(m_currentPos);
        UpdateCurrentChunkList(list, m_currentPos);
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

    bool IsChange(ChunkVector2 realtimePos)
    {
        if (m_currentPos != realtimePos)
        {
            m_currentPos = realtimePos;
            return true;
        }
        return false;
    }

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

    private void UpdateCurrentChunkList(List<ChunkVector2> actulChunkList, ChunkVector2 currentPos)
    {
        UnloadUnnecessaryChunks(actulChunkList);
        LoadNewChunks(actulChunkList, currentPos);
        UpdateChunkStates(currentPos);
    }

    private void UpdateChunkStates(ChunkVector2 currentPos)
    {
        foreach (var chunk in m_currentChunkList)
        {
            ChunkState actualState = GetChunkStateByRelativePosition(chunk.m_position, currentPos);
            if (chunk.m_currentState != actualState)
            {
                chunk.Update(actualState);
            }
        }
    }

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

    private void UnloadUnnecessaryChunks(List<ChunkVector2> actualChunkList)
    {
        var chunksToRemove = new HashSet<Chunk>();
        foreach (var chunk in m_currentChunkList)
        {
            if (!actualChunkList.Contains(chunk.m_position))
            {
                chunksToRemove.Add(chunk);
            }
        }
        foreach (var chunk in chunksToRemove)
        {
            Debug.Log($"卸载的地图块：({chunk.m_position})");
            chunk.Unload();
            m_chunkMap.Remove(chunk.m_position);
        }
        m_currentChunkList.ExceptWith(chunksToRemove);
    }

    public ChunkVector2 GetCurrentChunkVector(Vector3 position)
    {
        int col = Mathf.FloorToInt(position.y / (m_chunkLength* oneMapScale));
        int row = Mathf.FloorToInt(position.x / (m_chunkLength * oneMapScale));
        return new ChunkVector2(row, col);
    }

    public Chunk GetOrCreateChunk(ChunkVector2 chunkPos)
    {
        if (!m_chunkMap.ContainsKey(chunkPos))
        {
            m_chunkMap[chunkPos] = new Chunk(chunkPos);
        }
        return m_chunkMap[chunkPos];
    }

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
        return ChunkState.Display;
    }
}