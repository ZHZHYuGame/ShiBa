using UnityEngine;
using System.Collections.Generic;

public class ChunkController : MonoBehaviour
{
    /// <summary>
    /// 所有的块
    /// </summary>
    Dictionary<ChunkVector2, Chunk> m_chunkMap = new Dictionary<ChunkVector2, Chunk>();

    /// <summary>
    /// 当前玩家
    /// </summary>
    [SerializeField]
    Transform m_player;

    /// <summary>
    /// 当前玩家所在块位置
    /// </summary>
    ChunkVector2 m_currentPos;

    /// <summary>
    /// 当前的块列表
    /// </summary>
    [SerializeField]
    List<ChunkVector2> m_currentChunkList = new List<ChunkVector2>();

    /// <summary>
    /// 单个块的边长
    /// </summary>
    [SerializeField]
    float m_chunkLength;

    [SerializeField]
    private int m_loadRange = 1; // 1 表示九宫格，2 表示 5x5，以此类推

    void Start()
    {
        // 初始化玩家所在的块
        InitMap();
    }

    protected virtual void InitMap()
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
        if (IsChange(realtimePos)) // 当前块位置发生改变，则更新当前块列表
        {
            var list = GetActualChunkList(realtimePos);
            UpdateCurrentChunkList(list, realtimePos);
        }
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

    List<ChunkVector2> GetActualChunkList(ChunkVector2 currentVector)
    {
        List<ChunkVector2> expectChunkPosList = new List<ChunkVector2>();
        int currentRow = currentVector.rowNum;
        int currentCol = currentVector.colNum;

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

    /// <summary>
    /// 对比当前块列表与实际块列表，并更新当前块列表
    /// </summary>
    /// <param name="actulChunkList">实际块列表</param>
    /// <param name="currentPos">当前中心块位置</param>
    private void UpdateCurrentChunkList(List<ChunkVector2> actulChunkList, ChunkVector2 currentPos)
    {
        // 卸载不再需要的块
        for (int i = 0; i < m_currentChunkList.Count; i++)
        {
            ChunkVector2 pos = m_currentChunkList[i];
            if (!actulChunkList.Contains(pos)) // 实际块列表里若不存在当前列表的指定元素，则卸载删除
            {
                if (m_chunkMap.ContainsKey(pos))
                {
                    m_chunkMap[pos].Unload(); // 卸载不存在于实际块列表的块
                    m_chunkMap.Remove(pos);  // 从字典中移除
                }

                m_currentChunkList.RemoveAt(i); // 移除当前块列表中不存在与实际块列表的块
                i--; // 在遍历列表时删除列表元素，记得索引-1，否则无法正确遍历
            }
            else
            {
                actulChunkList.Remove(pos); // 实际块列表移除和当前块列表中相同的元素
                // 更新块的状态
                ChunkState actualState = GetChunkStateByRelativePosition(pos, currentPos);
                m_chunkMap[pos].Update(actualState);
            }
        }

        // 加载新的块
        for (int i = 0; i < actulChunkList.Count; i++)
        {
            ChunkVector2 pos = actulChunkList[i];
            if (!m_chunkMap.ContainsKey(pos)) // 如果块不存在，则创建新的块
            {
                Chunk newChunk = new Chunk(pos); // 创建新块
                m_chunkMap[pos] = newChunk; // 添加到字典中
            }

            // 更新块的状态
            ChunkState actualState = GetChunkStateByRelativePosition(pos, currentPos);
            m_chunkMap[pos].Update(actualState);

            m_currentChunkList.Add(pos); // 添加到当前块列表
        }

        //Resources.UnloadUnusedAssets(); // 释放未使用的资源
    }

    /// <summary>
    /// 获取块坐标
    /// </summary>
    /// <param name="position">玩家的具体vector3位置</param>
    /// <returns></returns>
    ChunkVector2 GetCurrentChunkVector(Vector3 position)
    {
        int col = (int)(position.x / m_chunkLength);
        int row = (int)(position.y / m_chunkLength);
        return new ChunkVector2(row, col);
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
        if (rowAmount <= 1 || colAmount <= 1)
        {
            return ChunkState.Display;
        }

        return ChunkState.UnLoad;
    }
}