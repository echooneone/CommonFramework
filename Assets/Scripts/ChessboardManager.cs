using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Doozy.Runtime.Colors.Models;
using Doozy.Runtime.Common;
using LitJson;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Pool;

public class ChessboardManager : SingletonMono<ChessboardManager>
{
    public GameObject[] PieceList;
    public GameObject Chessboard;
    private ObjectPool<GameObject> movablePointPool;
   
    public List<List<int>> tempIndex;
    public enum PieceName
    {
        卒,//0
        红炮,//1
        红车,//2
        红马,//3
        相,//4
        红士,//5
        帅,//6
        兵,//7
        黑炮,//8
        黑车,//9
        黑马,//10
        象,//11
        黑士,//12
        将,//13
    }

    public PieceBase SelectedPiece;

    public enum PointState
    {
        Red,
        Black,
        None
    }

    public GameObject[,] PointStateTable = new GameObject[10, 9];

    public readonly Vector2[,] ChessboardPoint =
    {
        { new(8, 9), new(6, 9), new(4, 9), new(2, 9), new(0, 9), new(-2, 9), new(-4, 9), new(-6, 9), new(-8, 9) },
        { new(8, 7), new(6, 7), new(4, 7), new(2, 7), new(0, 7), new(-2, 7), new(-4, 7), new(-6, 7), new(-8, 7) },
        { new(8, 5), new(6, 5), new(4, 5), new(2, 5), new(0, 5), new(-2, 5), new(-4, 5), new(-6, 5), new(-8, 5) },
        { new(8, 3), new(6, 3), new(4, 3), new(2, 3), new(0, 3), new(-2, 3), new(-4, 3), new(-6, 3), new(-8, 3) },
        { new(8, 1), new(6, 1), new(4, 1), new(2, 1), new(0, 1), new(-2, 1), new(-4, 1), new(-6, 1), new(-8, 1) },
        {
            new(8, -1), new(6, -1), new(4, -1), new(2, -1), new(0, -1), new(-2, -1), new(-4, -1), new(-6, -1),
            new(-8, -1)
        },
        {
            new(8, -3), new(6, -3), new(4, -3), new(2, -3), new(0, -3), new(-2, -3), new(-4, -3), new(-6, -3),
            new(-8, -3)
        },
        {
            new(8, -5), new(6, -5), new(4, -5), new(2, -5), new(0, -5), new(-2, -5), new(-4, -5), new(-6, -5),
            new(-8, -5)
        },
        {
            new(8, -7), new(6, -7), new(4, -7), new(2, -7), new(0, -7), new(-2, -7), new(-4, -7), new(-6, -7),
            new(-8, -7)
        },
        {
            new(8, -9), new(6, -9), new(4, -9), new(2, -9), new(0, -9), new(-2, -9), new(-4, -9), new(-6, -9),
            new(-8, -9)
        },
    };

    private void Start()
    {
        TextAsset jsonTextAsset = Resources.Load<TextAsset>("Standard");
        tempIndex=JsonMapper.ToObject<List<List<int>>>(jsonTextAsset.text);
/*后三个函数解释：启用安全检查为true，默认池容量10，最大池容量1000*/
        movablePointPool =
            new ObjectPool<GameObject>(createFunc, actionOnGet, actionOnRelease, actionOnDestroy, true, 17, 100);
        StandardInitialize();
    }

    GameObject createFunc()
    {
        var obj = Instantiate(Resources.Load<GameObject>("MovablePoint"), transform);
        return obj;
    }

/*actionOnGet:从池中获取实例时调用*/
    void actionOnGet(GameObject obj)
    {
        obj.gameObject.SetActive(true);
    }

/*
actionOnRelease:在实例返回到池时调用，可以用于清理或者禁用实例*/
    void actionOnRelease(GameObject obj)
    {
        obj.gameObject.SetActive(false);
    }

/*actionOnDestroy:操作销毁。当元素由于池达到最大大小而无法返回到池时调用*/
    void actionOnDestroy(GameObject obj)
    {
        Destroy(obj);
    }

    public void Clear()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                PointStateTable[i, j] = null;
            }
        }
    }

    [Button]
    public void StandardInitialize()
    {
        Clear();
        foreach (Transform piece in Chessboard.transform.Find("StandardHolder"))
        {
            piece.gameObject.SetActive(true);
            Vector2Int index = new Vector2Int(int.Parse(piece.name.First().ToString()), piece.name.Last() - 'a');
            piece.GetComponent<PieceBase>().Move(index.x, index.y);
            if (index.x < 5)
            {
                PointStateTable[index.x, index.y] = piece.gameObject;
                piece.GetComponent<PieceBase>().PieceState = PointState.Black;
            }
            else
            {
                PointStateTable[index.x, index.y] = piece.gameObject;
                piece.GetComponent<PieceBase>().PieceState = PointState.Red;
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.GetComponent<PieceBase>())
                {
                    if (SelectedPiece != null)
                    {
                        SelectedPiece.GetComponent<PieceBase>().EndSelected();
                    }

                    SelectedPiece = hit.collider.gameObject.GetComponent<PieceBase>();
                    SelectedPiece.OnSelected();
                }
            }
        }
    }
}