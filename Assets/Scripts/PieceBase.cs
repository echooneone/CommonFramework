using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PieceBase : MonoBehaviour
{

    [ReadOnly]
    public Vector2Int Index=new (0,0);

    [Button]
    public void Move()
    {
        var point = ChessboardManager.Instance.ChessboardPoint[Index.x,Index.y];
        transform.localPosition = new Vector3(point.x, point.y, transform.localPosition.z);

        print($"{Index.x},{(char)(Index.y + 97)}");
    }

    [Button]
    public void MoveUp()
    {
        if (Index.x > 0)
        {
            Index.x--;
            Move();
        }
    }
    [Button]
    public void MoveDown()
    {
        if (Index.x < 9)
        {
            Index.x++;
            Move();
        }
    }
    [Button]
    public void MoveLeft()
    {
        if (Index.y > 0)
        {
            Index.y--;
            Move();
        }
    }
    [Button]
    public void MoveRight()
    {
        if (Index.y < 8)
        {
            Index.y++;
            Move();
        }
    }
}
