using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class General : PieceBase
{

    public override void UpdateMovablePoint()
    {
        MovablePoints.Clear();
        Vector2Int tempPoint;
        tempPoint = Index + new Vector2Int(1, 0);
        if(CheckMovable(tempPoint))
            MovablePoints.Add(tempPoint);
        tempPoint = Index + new Vector2Int(-1, 0);
        if(CheckMovable(tempPoint))
            MovablePoints.Add(tempPoint);
        tempPoint = Index + new Vector2Int(0, 1);
        if(CheckMovable(tempPoint))
            MovablePoints.Add(tempPoint);
        tempPoint = Index + new Vector2Int(0, -1);
        if(CheckMovable(tempPoint))
            MovablePoints.Add(tempPoint);
    }

    public override bool CheckMovable(Vector2Int point)
    {
        if (PieceState == ChessboardManager.PointState.Red)
        {
            if (point.x is > 6 and < 10 && point.y is > 2 and < 6)
            {
                if (ChessboardManager.Instance.PointStateTable[point.x, point.y] == null)
                    return true;
            }

            return false;
        }
        else
        {
            if (point.x is >=0  and <= 2 && point.y is > 2 and < 6)
            {
                if (ChessboardManager.Instance.PointStateTable[point.x, point.y] == null)
                    return true;
            }

            return false;
        }
    }
}
