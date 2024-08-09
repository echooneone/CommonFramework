using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class PieceBase : MonoBehaviour
{
    [ReadOnly] public ChessboardManager.PointState PieceState;
    [ReadOnly] public Vector2Int Index = new(0, 0);
    private GameObject selectedEffect;
    public List<Vector2Int> MovablePoints = new List<Vector2Int>();
    private List<GameObject> movablePointObjs = new List<GameObject>();

    private void Start()
    {
        selectedEffect = Instantiate(Resources.Load<GameObject>("SelectedPoint"), transform);
        selectedEffect.SetActive(false);
    }


    public bool Move(int x, int y)
    {
        if (ChessboardManager.Instance.PointStateTable[x, y] != null &&
            ChessboardManager.Instance.PointStateTable[x, y].GetComponent<PieceBase>().PieceState == PieceState)
            return false;
        ChessboardManager.Instance.PointStateTable[Index.x, Index.y] = null;
        Index = new(x, y);
        var point = ChessboardManager.Instance.ChessboardPoint[Index.x, Index.y];
        ChessboardManager.Instance.PointStateTable[x, y] = gameObject;

        transform.localPosition = new Vector3(point.x, point.y, transform.localPosition.z);
        return true;
    }


    public void OnSelected()
    {
        // if(ChessboardManager.Instance.SelectedPiece==this)
        //     return;
        UpdateMovablePoint();
        selectedEffect.SetActive(true);
        foreach (Vector2Int point in MovablePoints)
        {
            GameObject obj = ChessboardManager.Instance.MovablePointPool.Get();
            obj.GetComponent<MovablePoint>().Index = point;
            movablePointObjs.Add(obj);
            var movablePoint = ChessboardManager.Instance.ChessboardPoint[point.x, point.y];
            obj.transform.localPosition = new Vector3(movablePoint.x, movablePoint.y, obj.transform.localPosition.z);
        }
    }

    public void EndSelected()
    {
        selectedEffect.SetActive(false);
        foreach (GameObject obj in movablePointObjs)
        {
            ChessboardManager.Instance.MovablePointPool.Release(obj);
        }

        movablePointObjs.Clear();
    }

    public List<Vector2Int> MovablePoint()
    {
        return MovablePoints;   
    }
    public abstract void UpdateMovablePoint();
    public abstract bool CheckMovable(Vector2Int point);

    #region DebugTool

    [Button]
    public void Move()
    {
        var point = ChessboardManager.Instance.ChessboardPoint[Index.x, Index.y];
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

    #endregion
}