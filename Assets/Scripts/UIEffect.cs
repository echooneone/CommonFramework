using System;
using System.Collections;
using System.Collections.Generic;
using Doozy.Runtime.Reactor;
using Doozy.Runtime.UIManager.Animators;
using Doozy.Runtime.UIManager.Components;
using UnityEngine;

public class UIEffect : MonoBehaviour
{
    public List<UITab> TabList;
    private UITab lastActiveToggle;
    private int lastIndex;

    public UIStepper StepperList;
    private void Start()
    {
    }

    public static void Copy(string pStrFilePath, string pPreFilePath, Action<string> finish = null)
    {
        if (string.IsNullOrEmpty(pPreFilePath) || string.IsNullOrEmpty(pStrFilePath))
        {
            Debug.Log("Path is null!");
            return;
        }
    }

    public void Testtt()
    {
        Debug.Log("IIIII");
    }
}