
using Sirenix.OdinInspector;
using UnityEngine;

public class PieceBase : MonoBehaviour
{
    [ReadOnly] public ChessboardManager.PointState PieceState;
    [ReadOnly] public Vector2Int Index = new(0, 0);
    private GameObject selectedEffect;
    
    private void Start()
    {
        selectedEffect = Instantiate(Resources.Load<GameObject>("SelectedPoint"),transform);
        selectedEffect.SetActive(false);
    }

    [Button]
    public void Move()
    {
        var point = ChessboardManager.Instance.ChessboardPoint[Index.x, Index.y];
        transform.localPosition = new Vector3(point.x, point.y, transform.localPosition.z);

        print($"{Index.x},{(char)(Index.y + 97)}");
    }

    public bool Move(int x, int y)
    {
        if (ChessboardManager.Instance.PointStateTable[x, y] != null &&
            ChessboardManager.Instance.PointStateTable[x, y].GetComponent<PieceBase>().PieceState == PieceState)
            return false;
        Index = new(x, y);
        var point = ChessboardManager.Instance.ChessboardPoint[Index.x, Index.y];
        transform.localPosition = new Vector3(point.x, point.y, transform.localPosition.z);
        return true;
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

    public void OnSelected()
    {
        selectedEffect.SetActive(true);
    }

    public void EndSelected()
    {
        selectedEffect.SetActive(false);
    }
}