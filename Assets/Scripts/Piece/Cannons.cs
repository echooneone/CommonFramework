using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannons : PieceBase
{
    public override void UpdateMovablePoint()
    {
        MovablePoints.Clear();
        for (int x = Index.x-1; x >= 0; x--)
        {
            var tempPoint = new Vector2Int(x, Index.y);
            if (CheckMovable(tempPoint))
            {
                MovablePoints.Add(tempPoint);
            }
            else
            {
                break;
            }
        }
        for (int x = Index.x+1; x <= 9; x++)
        {
            var tempPoint = new Vector2Int(x, Index.y);
            if (CheckMovable(tempPoint))
            {
                MovablePoints.Add(tempPoint);
            }
            else
            {
                break;
            }
        }
        for (int y = Index.y-1; y >= 0; y--)
        {
            var tempPoint = new Vector2Int(Index.x,y);
            if (CheckMovable(tempPoint))
            {
                MovablePoints.Add(tempPoint);
            }
            else
            {
                break;
            }
        }
        for (int y = Index.y+1; y <= 8; y++)
        {
            var tempPoint = new Vector2Int(Index.x,y);
            if (CheckMovable(tempPoint))
            {
                MovablePoints.Add(tempPoint);
            }
            else
            {
                break;
            }
        }
    }

    public override bool CheckMovable(Vector2Int point)
    {
        if (ChessboardManager.Instance.PointStateTable[point.x, point.y] == null)
            return true;
        return false;
    }
}
