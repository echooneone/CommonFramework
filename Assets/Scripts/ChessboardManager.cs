using System;
using System.Collections;
using System.Collections.Generic;
using Doozy.Runtime.Common;
using UnityEngine;

public class ChessboardManager : SingletonMono<ChessboardManager>
{
    public GameObject[] PieceList;
    public enum  PieceName
    {
        卒,
        红炮,
        红车,
        红马,
        相,
        红士,
        帅,
        兵,
        黑炮,
        黑车,
        黑马,
        象,
        黑士,
        将,
    }

    public PieceName SelectedPiece;
    public enum PointState
    {
        Red,
        Black,
        None
    }
    public PointState[,] PointStateTable = new PointState[10,9];
    public   readonly Vector2[,] ChessboardPoint =
    {
        {new (8,9),new (6,9),new (4,9),new (2,9),new (0,9),new (-2,9),new (-4,9),new (-6,9),new (-8,9)},
        {new (8,7),new (6,7),new (4,7),new (2,7),new (0,7),new (-2,7),new (-4,7),new (-6,7),new (-8,7)},
        {new (8,5),new (6,5),new (4,5),new (2,5),new (0,5),new (-2,5),new (-4,5),new (-6,5),new (-8,5)},
        {new (8,3),new (6,3),new (4,3),new (2,3),new (0,3),new (-2,3),new (-4,3),new (-6,3),new (-8,3)},
        {new (8,1),new (6,1),new (4,1),new (2,1),new (0,1),new (-2,1),new (-4,1),new (-6,1),new (-8,1)},
        {new (8,-1),new (6,-1),new (4,-1),new (2,-1),new (0,-1),new (-2,-1),new (-4,-1),new (-6,-1),new (-8,-1)},
        {new (8,-3),new (6,-3),new (4,-3),new (2,-3),new (0,-3),new (-2,-3),new (-4,-3),new (-6,-3),new (-8,-3)},
        {new (8,-5),new (6,-5),new (4,-5),new (2,-5),new (0,-5),new (-2,-5),new (-4,-5),new (-6,-5),new (-8,-5)},
        {new (8,-7),new (6,-7),new (4,-7),new (2,-7),new (0,-7),new (-2,-7),new (-4,-7),new (-6,-7),new (-8,-7)},
        {new (8,-9),new (6,-9),new (4,-9),new (2,-9),new (0,-9),new (-2,-9),new (-4,-9),new (-6,-9),new (-8,-9)},
    };

    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                PointStateTable[i, j] = PointState.None;
            }
        }
    }

    public void Clear()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                PointStateTable[i, j] = PointState.None;
            }
        }
    }
}
