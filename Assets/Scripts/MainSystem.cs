using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Bitsplash.DatePicker;
using Doozy.Runtime.UIManager.Components;
using LitJson;
using SFB;
using Sirenix.OdinInspector;
using UnityEngine;

public class MainSystem : SingletonMono<MainSystem>
{
    public UIButton DateButton;
    public DatePickerDropDown StartDDD;
    public DatePickerDropDown EndDDD;
    public DataBundle[] bundles;
    public FormatData Format;
    public Dictionary<string, DataBundle[]> bundlesDict = new Dictionary<string, DataBundle[]>();
    private void Start()
    { 
        LoadFormat();
        // 获取当前应用程序的目录
        string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        // 获取上级目录
        string parentDirectory = Directory.GetParent(currentDirectory)?.FullName;
        // 构建目标路径
        string targetPath = Path.Combine(parentDirectory, "resources", "public", "Data");
        
        LoadData(targetPath);
        StartDDD.DropDownContent.Content.OnSelectionChanged.AddListener(() =>
        {
            if (StartDDD.DropDownContent.Content.Selection.Count != 0 && EndDDD.DropDownContent.Content.Selection.Count!=0)
            {
                DateButton.interactable = true;
            }
            else
            {
                DateButton.interactable = false;
            }
        });
        EndDDD.DropDownContent.Content.OnSelectionChanged.AddListener(() =>
        {
            if (StartDDD.DropDownContent.Content.Selection.Count != 0 && EndDDD.DropDownContent.Content.Selection.Count!=0)
            {
                DateButton.interactable = true;
            }
            else
            {
                DateButton.interactable = false;
            }
                
        });
    }

    // public void LoadData()
    // {
    //     string path = Application.streamingAssetsPath + "/Data_2024-08-06.json";
    //     string jsonStr = File.ReadAllText(path);
    //
    //     bundles = JsonMapper.ToObject<DataBundle[]>(jsonStr);
    // }
    public void LoadData(string path)
    {
        string[] files = Directory.GetFiles(path, "Data_*.json");

        foreach (string file in files)
        {
            string fileName = Path.GetFileNameWithoutExtension(file);
            string dateStr = fileName.Substring(5); // 获取日期部分
            string jsonStr = File.ReadAllText(file);

            DataBundle[] dataBundles = JsonMapper.ToObject<DataBundle[]>(jsonStr);
            bundlesDict[dateStr] = dataBundles;
        }
    }

    private void LoadFormat()
    {
        string jsonStr = File.ReadAllText(Application.streamingAssetsPath+"/format.json");
        Format = JsonMapper.ToObject<FormatData>(jsonStr);
    }
    [Button]
    public void Test()
    {
        LoadData( Application.streamingAssetsPath);
        foreach (var keyValuePair in bundlesDict)
        {
            string date = keyValuePair.Key;
            DataBundle[] bundles = keyValuePair.Value;
            Type type = typeof(DataBundle);

            //获取所有属性
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo info in properties)
            {
                print(date + " - " + info.Name + " :" + info.GetValue(bundles[0]));
            }
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

public class FormatData
{
    public string ShopId { set; get; }
    public string[] chapter1 { set; get; }
    public string[] chapter2 { set; get; }
    public string[] chapter3 { set; get; }
    public string[] chapter4 { set; get; }
    public string[] chapter5 { set; get; }
    public string[] chapter6 { set; get; }

}