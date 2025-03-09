using UnityEngine;
using System.Collections.Generic;

public class ChunkController : MonoSingleton<ChunkController>
{
    // 地图块数量
    public int m_row = -1;
    public int m_col = -1;

    // 所有的块
    Dictionary<ChunkVector2, Chunk> m_chunkMap;

    // 玩家
    Transform m_player;

    // 玩家所在块位置
    ChunkVector2 m_currentPos;

    // 预加载的地图块列表
    [SerializeField]
    HashSet<ChunkVector2> expectChunkVectorList = new HashSet<ChunkVector2>();

    // 显示加载的块列表
    [SerializeField]
    HashSet<Chunk> m_currentChunkList = new HashSet<Chunk>();

    // 单个地图块的边长
    [SerializeField]
    float m_chunkLength = 10;

    float mapScale;
    int oneMapScale;

    [SerializeField]
    private int m_loadRange = 1; // 1 表示九宫格，2 表示 5x5，以此类推

    private float _lastUnloadTime;
    private const float UnloadInterval = 10f; // 每 10 秒调用一次

    void Start()
    {
        m_player = PlayerRole.Instance.transform;
        oneMapScale = MapManager.Instance.oneMapScale;
        m_chunkMap = new Dictionary<ChunkVector2, Chunk>();
        mapScale = m_chunkLength * oneMapScale;
        InitMap();
    }

    protected void InitMap()
    {
        m_currentPos = GetCurrentChunkVector(m_player.position);
        var list = GetActualChunkList(m_currentPos);
        switch (MapManager.Instance.currType)
        {
            case MapType.One:
                // 无限地图
                UpdateCurrentChunkList(list, m_currentPos);
                break;
            case MapType.Two:

                // 部分有限地图
                m_currentPos = GetCurrentChunkVector(m_player.position);
                UpdateCurrentChunkList(list, m_currentPos);
                break;
            case MapType.Three:
                // 有限地图
                for (int i = 0; i < m_row; i++)
                {
                    for (int j = 0; j < m_col; j++)
                    {
                        Vector3 position = new Vector3(j * m_chunkLength, 0, i * m_chunkLength);
                        ChunkVector2 chunkPos = new ChunkVector2(i, j);
                        Chunk newChunk = new Chunk(chunkPos);
                        m_chunkMap[chunkPos] = newChunk;
                    }
                }
                break;
            default:
                break;
        }
        list.Clear();
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
        bool isChange = m_currentPos != realtimePos;
        if (isChange)
        {
            m_currentPos = realtimePos;
        }
        return isChange;
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

                // 检查地图块是否在有限地图的范围内
                if (m_row > 0 && (expRow < 0 || expRow >= m_row)) continue;
                if (m_col > 0 && (expCol < 0 || expCol >= m_col)) continue;

                expectChunkPosList.Add(new ChunkVector2(expRow, expCol));
            }
        }

        // 如果是无限地图，预加载外围一圈的地图块
        if (m_col <= 0 && m_row <= 0)
        {
            for (int i = -m_loadRange - 1; i <= m_loadRange + 1; i++)
            {
                for (int j = -m_loadRange - 1; j <= m_loadRange + 1; j++)
                {
                    if (Mathf.Abs(i) > m_loadRange || Mathf.Abs(j) > m_loadRange)
                    {
                        int expRow = currentRow + i;
                        int expCol = currentCol + j;
                        expectChunkPosList.Add(new ChunkVector2(expRow, expCol));
                    }
                }
            }
        }

        return expectChunkPosList;
    }

    private void UpdateCurrentChunkList(List<ChunkVector2> actualChunkList, ChunkVector2 currentPos)
    {
        UnloadUnnecessaryChunks(actualChunkList);
        LoadNewChunks(actualChunkList, currentPos);
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
                Debug.Log($"准备卸载的地图块：({chunk.m_position.rowNum}, {chunk.m_position.colNum})");
                chunksToRemove.Add(chunk);
            }
        }
        foreach (var chunk in chunksToRemove)
        {
            Debug.Log($"卸载的地图块：({chunk.m_position.rowNum}, {chunk.m_position.colNum})");
            chunk.Unload();
            m_chunkMap.Remove(chunk.m_position);
        }
        m_currentChunkList.ExceptWith(chunksToRemove);
    }

    public ChunkVector2 GetCurrentChunkVector(Vector3 position)
    {
        int col = Mathf.FloorToInt((position.y + mapScale / 2) / mapScale);
        int row = Mathf.FloorToInt((position.x + mapScale / 2) / mapScale);
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

    private void OnDrawGizmos()
    {
        if (m_currentChunkList == null) return;

        // 设置绘制颜色为绿色
        Gizmos.color = Color.green;

        // 遍历当前加载的地图块
        foreach (var chunk in m_currentChunkList)
        {
            // 计算地图块的中心点
            Vector3 center = new Vector3(
               chunk.m_position.rowNum * mapScale, // X 坐标
               chunk.m_position.colNum * mapScale, // Y 坐标
                0 // Z 坐标
            );

            // 绘制地图块的边框
            Gizmos.DrawWireCube(center, new Vector3(mapScale, mapScale, 0));
        }

        // 设置绘制颜色为红色
        Gizmos.color = Color.red;

        // 计算玩家所在块的中心点
        Vector3 playerChunkCenter = new Vector3(
            m_currentPos.rowNum * mapScale, // X 坐标
            m_currentPos.colNum * mapScale, // Y 坐标
            0 // Z 坐标
        );

        // 绘制玩家所在块的边框
        Gizmos.DrawWireCube(playerChunkCenter, new Vector3(mapScale, mapScale, 0));
    }
}