using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
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
    public ObjectPool<GameObject> MovablePointPool;
    
    private List<List<int>> tempIndex;
    public CinemachineVirtualCamera CurrentVirtualCamera;
    private bool redView;
    public enum PieceName
    {
        卒, //0
        红炮, //1
        红车, //2
        红马, //3
        相, //4
        红士, //5
        帅, //6
        兵, //7
        黑炮, //8
        黑车, //9
        黑马, //10
        象, //11
        黑士, //12
        将, //13
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
        tempIndex = JsonMapper.ToObject<List<List<int>>>(jsonTextAsset.text);
/*后三个函数解释：启用安全检查为true，默认池容量10，最大池容量1000*/
        MovablePointPool =
            new ObjectPool<GameObject>(createFunc, actionOnGet, actionOnRelease, actionOnDestroy, true, 17, 100);
        StandardInitialize();
    }

    GameObject createFunc()
    {
        var obj = Instantiate(Resources.Load<GameObject>("MovablePoint"), Chessboard.transform);
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
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 9; y++)
            {
                PointStateTable[x, y] = null;
            }
        }

        foreach (Transform obj in Chessboard.transform)
        {
            Destroy(obj.gameObject);
        }
    }

    /// <summary>
    /// 生成标准盘
    /// </summary>
    [Button]
    public void StandardInitialize()
    {
        Clear();
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 9; y++)
            {
                if (tempIndex[x][y] >= 0)
                {
                    GameObject pieceObj = Instantiate(PieceList[tempIndex[x][y]], Chessboard.transform);
                    PieceBase piece = pieceObj.GetComponent<PieceBase>();
                    piece.Move(x, y);
                    piece.PieceState = x < 5 ? PointState.Black : PointState.Red;
                    PointStateTable[x, y] = pieceObj;
                }
            }
        }
        ToRedView();
        
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
                        if(hit.collider.gameObject.GetComponent<PieceBase>()==SelectedPiece)
                            return;
                        SelectedPiece.GetComponent<PieceBase>().EndSelected();
                    }

                    SelectedPiece = hit.collider.gameObject.GetComponent<PieceBase>();
                    SelectedPiece.OnSelected();
                }
                else if(hit.collider.GetComponent<MovablePoint>())
                {
                    hit.collider.GetComponent<MovablePoint>().OnChick();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeView();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    [Button]
    public void ReadJson(int x, int y)
    {
        print(tempIndex[x][y]);
        
        
    }
    [Button]
    public void ChangeView()
    {
        if (redView)
        {
            ToBlackView();
        }
        else
        {
            ToRedView();
        }
    }

    private void ToRedView()
    {
        CurrentVirtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 0;
        redView = true;
    }
    private void ToBlackView()
    {
        CurrentVirtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = CurrentVirtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_Path.MaxPos;
        redView = false;
    }
}