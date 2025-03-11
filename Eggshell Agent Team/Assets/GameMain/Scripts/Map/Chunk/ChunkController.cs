using UnityEngine;
using System.Collections.Generic;

public class ChunkController : MonoSingleton<ChunkController>
{
    // 地图块数量
    int m_row;
    int m_col;

    // 所有的块
    Dictionary<ChunkVector2, Chunk> m_chunkMap;

    // 玩家
    Transform m_player;

    // 玩家所在块位置
    ChunkVector2 m_currentPos;

    // 显示加载的块列表
    [SerializeField]
    HashSet<Chunk> m_currentChunkList = new HashSet<Chunk>();

    // 单个地图块的边长
    float m_chunkLength;

    float mapScale;
    int oneMapScale;

    [SerializeField]
    private int m_loadRange = 1;
    int x;
    int y ;
    private float m_lastUnloadTime;
    private const float UnloadInterval = 10f; // 每 10 秒调用一次

    public void Init(MapData data)
    {
        oneMapScale = data.oneMapScale;
        m_chunkLength = data.mapLength;
        mapScale = m_chunkLength * oneMapScale;
        m_row = data.mapWidth;
        m_col = data.mapHeight;
        x = m_row / 2;
        y = m_col / 2;
    }
    private void Start()
    {
        m_player = PlayerRole.Instance.transform;
        m_chunkMap = new Dictionary<ChunkVector2, Chunk>();
        InitMap();
    }

    protected void InitMap()
    {
        m_currentPos = GetCurrentChunkVector(m_player.position);
        var list = GetActualChunkList(m_currentPos);
        switch (MapManager.Instance.currType)
        {
            case MapType.One:
                UpdateCurrentChunkList(list, m_currentPos);
                break;
            case MapType.Two:
                UpdateCurrentChunkList(list, m_currentPos);
                break;
            case MapType.Three:

                for (int i = -x; i <=x; i++)
                {
                    for (int j = -y; j <=y; j++)
                    {
                        //Vector3 position = new Vector3(j * mapScale, 0, i * mapScale);
                        ChunkVector2 chunkPos = new ChunkVector2(i, j);
                        Chunk newChunk = new Chunk(chunkPos);
                        m_chunkMap[chunkPos] = newChunk;
                    }
                }
                UpdateCurrentChunkList(list, m_currentPos);
                break;
        }
        list.Clear();
    }

    void Update()
    {

        var realtimePos = GetCurrentChunkVector(m_player.position);
        if (IsChange(realtimePos))
        {
            var list = GetActualChunkList(realtimePos);
            UpdateCurrentChunkList(list, realtimePos);
        }

        if (Time.time - m_lastUnloadTime > UnloadInterval)
        {
            Resources.UnloadUnusedAssets();
            m_lastUnloadTime = Time.time;
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


                switch (MapManager.Instance.currType)
                {
                    case MapType.Three:
                        if (expRow < -x || expRow >= x || expCol <-y || expCol >= y)
                        {
                            continue;
                        }
                        break;

                    case MapType.Two: 
                        if (m_row > 0 && (expRow < 0 || expRow >= m_row)) continue; 
                        break;

                    case MapType.One:
                        break;
                }

                expectChunkPosList.Add(new ChunkVector2(expRow, expCol));
            }
        }

        if (MapManager.Instance.currType != MapType.Three)
        {
            for (int i = -m_loadRange - 1; i <= m_loadRange + 1; i++)
            {
                for (int j = -m_loadRange - 1; j <= m_loadRange + 1; j++)
                {
                    if (Mathf.Abs(i) > m_loadRange || Mathf.Abs(j) > m_loadRange)
                    {
                        int expRow = currentRow + i;
                        int expCol = currentCol + j;

                        switch (MapManager.Instance.currType)
                        {
                            case MapType.Two: 
                                if (m_row > 0 && (expRow < 0 || expRow >= m_row)) continue;
                                break;

                            case MapType.One: 
                                break;
                        }

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
                switch (MapManager.Instance.currType)
                {
                    case MapType.Three:
                        if (pos.rowNum < x || pos.rowNum >= -x || pos.colNum >= -y || pos.colNum < y)
                        {
                            continue;
                        }
                        break;

                    case MapType.Two: 
                        if (m_row > 0 && (pos.rowNum < 0 || pos.rowNum >= m_row)) continue;
                        break;

                    case MapType.One:
                        break;
                }

                //Debug.Log($"加载新的地图块 ({pos.rowNum}, {pos.colNum})");
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
                // 根据地图类型处理边界
                switch (MapManager.Instance.currType)
                {
                    case MapType.Three: // 有限地图

                        if (chunk.m_position.rowNum < x || chunk.m_position.rowNum >= -x || chunk.m_position.colNum < y || chunk.m_position.colNum >= -y)
                        {
                            chunksToRemove.Add(chunk);
                        }
                        break;
                    case MapType.Two: // 部分有限地图

                        if (m_col > -y && (chunk.m_position.colNum < y || chunk.m_position.colNum >= -y)) // y 轴有限
                        {
                            //Debug.Log($"准备卸载的地图块：({chunk.m_position.rowNum}, {chunk.m_position.colNum})");
                            chunksToRemove.Add(chunk);
                        }
                        break;

                    case MapType.One:
                        chunksToRemove.Add(chunk);
                        break;
                }
            }
        }
        foreach (var chunk in chunksToRemove)
        {
            //Debug.Log($"卸载的地图块：({chunk.m_position.rowNum}, {chunk.m_position.colNum})");
            if (chunk.IsUnLoad())
            {
                chunk.Unload();
                m_chunkMap.Remove(chunk.m_position);
            }
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