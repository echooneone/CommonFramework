using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using LitJson;
using SFB;
using Sirenix.OdinInspector;
using UnityEngine;

public class MainSystem : MonoBehaviour
{

    public DataBundle[] bundles;

    private void Start()
    {
        Test();
    }

    public void LoadData()
    {
        string path = Application.streamingAssetsPath + "/Data_2024-08-06.json";
        string jsonStr = File.ReadAllText(path);

        bundles = JsonMapper.ToObject<DataBundle[]>(jsonStr);
    }

    [Button]
    public void Test()
    {
        LoadData();
        Type type = typeof(DataBundle);
        //获取所有属性。
        PropertyInfo[] properties = type.GetProperties();
        foreach (PropertyInfo info in properties)
        {
            print(info.Name + " :" + info.GetValue(bundles[0]));
        }
    }
}

public class DataBundle
{
    public string ShopID { set; get; }
    public string Date { set; get; }
    public int Chapter1_Episode1_Stay { set; get; }
    public int Chapter1_Episode2_Stay { set; get; }
    public int Chapter1_Episode3_Stay { set; get; }
    public int Chapter2_Episode1_Stay { set; get; }
    public int Chapter2_Episode2_Stay { set; get; }
    public int Chapter2_Episode3_Stay { set; get; }
    public int Chapter3_Episode1_Stay { set; get; }
    public int Chapter3_Episode2_Stay { set; get; }
    public int Chapter3_Episode3_Stay { set; get; }
    public int Chapter4_Episode1_Stay { set; get; }
    public int Chapter4_Episode2_Stay { set; get; }
    public int Chapter4_Episode3_Stay { set; get; }
    public int Chapter5_Episode1_Stay { set; get; }
    public int Chapter5_Episode2_Stay { set; get; }
    public int Chapter5_Episode3_Stay { set; get; }
    public int Chapter6_Episode1_Stay { set; get; }
    public int Chapter6_Episode2_Stay { set; get; }
    public int Chapter6_Episode3_Stay { set; get; }

    public int Chapter1_Episode1_Play { set; get; }
    public int Chapter1_Episode2_Play { set; get; }
    public int Chapter1_Episode3_Play { set; get; }
    public int Chapter2_Episode1_Play { set; get; }
    public int Chapter2_Episode2_Play { set; get; }
    public int Chapter2_Episode3_Play { set; get; }
    public int Chapter3_Episode1_Play { set; get; }
    public int Chapter3_Episode2_Play { set; get; }
    public int Chapter3_Episode3_Play { set; get; }
    public int Chapter4_Episode1_Play { set; get; }
    public int Chapter4_Episode2_Play { set; get; }
    public int Chapter4_Episode3_Play { set; get; }
    public int Chapter5_Episode1_Play { set; get; }
    public int Chapter5_Episode2_Play { set; get; }
    public int Chapter5_Episode3_Play { set; get; }
    public int Chapter6_Episode1_Play { set; get; }
    public int Chapter6_Episode2_Play { set; get; }
    public int Chapter6_Episode3_Play { set; get; }
}