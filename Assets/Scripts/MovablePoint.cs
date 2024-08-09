using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class MovablePoint : MonoBehaviour
{
    [ReadOnly]
    public Vector2Int Index;
    public void OnChick()
    {
        ChessboardManager.Instance.SelectedPiece.Move(Index.x, Index.y);
        ChessboardManager.Instance.SelectedPiece.EndSelected();
        ChessboardManager.Instance.SelectedPiece = null;
    }
}
