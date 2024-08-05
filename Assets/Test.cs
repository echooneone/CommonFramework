using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using uPalette.Generated;
using uPalette.Runtime.Core;

public class Test : MonoBehaviour
{

    private int themeId;


    private void Start()
    {
        themeId=ConfigIni.GetInstance().ThemeId;
        ChangeTheme(themeId);
    }


    public void ChangeTheme(int id)
    {
        switch (id)
        {
            case 1:PaletteStore.Instance.ColorPalette.SetActiveTheme(ColorTheme.Default.ToThemeId());
                break;
            case 2:PaletteStore.Instance.ColorPalette.SetActiveTheme(ColorTheme.Light.ToThemeId());
                break;
            case 3:PaletteStore.Instance.ColorPalette.SetActiveTheme(ColorTheme.Dark.ToThemeId());
                break;
        }

        themeId = id;
        ConfigIni.GetInstance().ThemeId = themeId;
        ConfigIni.GetInstance().Save();

    }
 
}
