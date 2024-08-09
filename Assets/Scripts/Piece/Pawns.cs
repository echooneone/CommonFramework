using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawns : PieceBase
{
    public override void UpdateMovablePoint()
    {
        MovablePoints.Clear();
        Vector2Int tempPoint;
        if (PieceState == ChessboardManager.PointState.Red)
        {
            tempPoint = Index + new Vector2Int(-1, 0);
            if (CheckMovable(tempPoint))
                MovablePoints.Add(tempPoint);
            if (Index.x is >= 0 and < 5)
            {
                tempPoint = Index + new Vector2Int(0, 1);
                if (CheckMovable(tempPoint))
                    MovablePoints.Add(tempPoint);
                tempPoint = Index + new Vector2Int(0, -1);
                if (CheckMovable(tempPoint))
                    MovablePoints.Add(tempPoint);
            }
        }
        else
        {
            tempPoint = Index + new Vector2Int(1, 0);
            if (CheckMovable(tempPoint))
                MovablePoints.Add(tempPoint);
            if (Index.x is >= 5 and < 10)
            {
                tempPoint = Index + new Vector2Int(0, 1);
                if (CheckMovable(tempPoint))
                    MovablePoints.Add(tempPoint);
                tempPoint = Index + new Vector2Int(0, -1);
                if (CheckMovable(tempPoint))
                    MovablePoints.Add(tempPoint);
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