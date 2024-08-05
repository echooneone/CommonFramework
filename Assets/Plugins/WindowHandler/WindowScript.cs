using System;
using System.Collections;
using ExtendTool;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(WindowCustomizer))]
public class WindowScript : MonoBehaviour, IDragHandler
{
    public Image MaximalImage;
    public Sprite WindowSprite;
    public Sprite MaximalSprite;
    public Vector2Int defaultWindowSize;
    public Vector2Int borderSize;

    private Vector2 _deltaValue = Vector2.zero;
    public bool Maximized;
    private WindowCustomizer windowCustomizer;
    private void Awake()
    {
        windowCustomizer = GetComponent<WindowCustomizer>();
    }
#if !UNITY_EDITOR
    private void Start()
    {
        OnNoBorderBtnClick();
    }
#endif


    public void OnBorderBtnClick()
    {
        if (BorderlessWindow.framed)
            return;
        BorderlessWindow.SetFramedWindow();
        BorderlessWindow.MoveWindowPos(Vector2Int.zero, Screen.width + borderSize.x,
            Screen.height + borderSize.y); // Compensating the border offset.
    }

    public void OnNoBorderBtnClick()
    {
        if (!BorderlessWindow.framed)
            return;

        BorderlessWindow.SetFramelessWindow();
        BorderlessWindow.MoveWindowPos(Vector2Int.zero, defaultWindowSize.x, defaultWindowSize.y);
    }

    public void ResetWindowSize()
    {
        BorderlessWindow.MoveWindowPos(Vector2Int.zero, defaultWindowSize.x, defaultWindowSize.y);
    }

    public void OnCloseBtnClick()
    {
        EventSystem.current.SetSelectedGameObject(null);
        Application.Quit();
    }

    public void OnMinimizeBtnClick()
    {
        EventSystem.current.SetSelectedGameObject(null);
        BorderlessWindow.MinimizeWindow();
    }

    public void OnMaximizeBtnClick()
    {
        EventSystem.current.SetSelectedGameObject(null);

        if (Maximized)
        {
            MaximalImage.sprite = MaximalSprite;
            windowCustomizer.SetRound();
            BorderlessWindow.RestoreWindow();
        }
        else
        {
            MaximalImage.sprite = WindowSprite;
            windowCustomizer.SetDefault();
            BorderlessWindow.MaximizeWindow();
        }


        Maximized = !Maximized;
    }

    public void OnDrag(PointerEventData data)
    {
        if (BorderlessWindow.framed)
            return;

        _deltaValue += data.delta;
        if (data.dragging)
        {
            BorderlessWindow.MoveWindowPos(_deltaValue, Screen.width, Screen.height);
        }
    }
    /// <summary>  
    /// 退出  
    /// </summary>  
    public void Quit()  
    {
#if UNITY_EDITOR  
        UnityEditor.EditorApplication.isPlaying = false;  
#else  
            Application.Quit();  
#endif  
    }


    
}