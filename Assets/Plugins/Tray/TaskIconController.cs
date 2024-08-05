using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class TaskIconController : MonoBehaviour
{
    TrayForm trayform;

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_STANDALONE_WIN && !UNITY_EDITOR 
        trayform = new TrayForm();
#endif
    }
    public  string EncryptMD5_16(string _encryptContent)
    {
        var md5 = new MD5CryptoServiceProvider();
        string t2 = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(_encryptContent)), 4, 8);
        t2 = t2.Replace("-", "");
        return t2;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            print(EncryptMD5_16("985725"));
        }
    }

    private void OnApplicationFocus(bool focus)
    {
       // Debug.Log("OnApplicationFocus : " + focus);

#if UNITY_STANDALONE_WIN && !UNITY_EDITOR
        trayform.OnApplicationFocusChange(focus);
#endif
    }

    public void ToTray()
    {
#if UNITY_STANDALONE_WIN && !UNITY_EDITOR 
        WinAPI.Minimize();
      trayform.MinimizeWindow();
#endif
    }

}
