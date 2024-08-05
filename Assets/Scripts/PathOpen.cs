using System;
using System.Collections;
using System.Collections.Generic;
using Doozy.Runtime.Common.Extensions;
using Doozy.Runtime.UIManager.Components;
using SFB;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UIButton))]
public class PathOpen : MonoBehaviour
{
    public RawImage output;

    private void Start()
    {
        var button = GetComponent<UIButton>();
        button.onClickEvent.AddListener(OnClick);
    }

    private void OnClick()
    {
        var paths = StandaloneFileBrowser.OpenFilePanel("", "",
            new[] { new ExtensionFilter("Image Files", "png","jpg","jpeg") }, false);
        if (paths.Length > 0)
        {
            StartCoroutine(OutputRoutine(new Uri(paths[0]).AbsoluteUri));
        }
    }

    private IEnumerator OutputRoutine(string url)
    {
        var loader = new WWW(url);
        yield return loader;
        output.texture = loader.texture;
        output.SetNativeSize();
        output.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
}