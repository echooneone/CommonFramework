using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knights : PieceBase
{
    public override void UpdateMovablePoint()
    {
        MovablePoints.Clear();
        if (CheckMovable(Index + new Vector2Int(1, 0)))
        {
            if (CheckMovable(Index + new Vector2Int(2, -1)))
            {
                MovablePoints.Add(Index + new Vector2Int(2, -1));
            }
            if (CheckMovable(Index + new Vector2Int(2, 1)))
            {
                MovablePoints.Add(Index + new Vector2Int(2, 1));
            }
        }
        if (CheckMovable(Index + new Vector2Int(-1, 0)))
        {
            if (CheckMovable(Index + new Vector2Int(-2, -1)))
            {
                MovablePoints.Add(Index + new Vector2Int(-2, -1));
            }
            if (CheckMovable(Index + new Vector2Int(-2, 1)))
            {
                MovablePoints.Add(Index + new Vector2Int(-2, 1));
            }
        }
        if (CheckMovable(Index + new Vector2Int(0, 1)))
        {
            if (CheckMovable(Index + new Vector2Int(1, 2)))
            {
                MovablePoints.Add(Index + new Vector2Int(1, 2));
            }
            if (CheckMovable(Index + new Vector2Int(-1, 2)))
            {
                MovablePoints.Add(Index + new Vector2Int(-1, 2));
            }
        }
        if (CheckMovable(Index + new Vector2Int(0, -1)))
        {
            if (CheckMovable(Index + new Vector2Int(1, -2)))
            {
                MovablePoints.Add(Index + new Vector2Int(1, -2));
            }
            if (CheckMovable(Index + new Vector2Int(-1, -2)))
            {
                MovablePoints.Add(Index + new Vector2Int(-1, -2));
            }
        }
    }
    public override bool CheckMovable(Vector2Int point)
    {
        if (point.x is >= 0 and < 10 && point.y is >= 0 and < 9)
        {
            if (ChessboardManager.Instance.PointStateTable[point.x, point.y] == null)
                return true;
        }

        return false;
    }
}
