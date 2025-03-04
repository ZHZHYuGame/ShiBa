using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ChunkVector2
{
    public int rowNum;
    public int colNum;

    public ChunkVector2(int rowNum, int colNum)
    {
        this.rowNum = rowNum;
        this.colNum = colNum;
    }

    public static bool operator ==(ChunkVector2 v1, ChunkVector2 v2)
    {
        return (v1.colNum == v2.colNum) && (v2.rowNum == v1.rowNum);
    }
    public static bool operator !=(ChunkVector2 v1, ChunkVector2 v2)
    {
        return (v1.colNum == v2.colNum) || (v2.rowNum == v1.rowNum);
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    public override string ToString()
    {
        return string.Format("[{0}][{1}]", this.rowNum, this.colNum);
    }
}