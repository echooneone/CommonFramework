
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
        var paths = StandaloneFileBrowser.OpenFolderPanel("Select Folder", "", true);
        print(paths[0]);
    }
    

}