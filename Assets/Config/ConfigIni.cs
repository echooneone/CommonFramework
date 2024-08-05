using System.IO;
using UnityEngine;
public class ConfigIni : MonoBehaviour
{
    private static ConfigIni instance;
    public string IniPath;

    public int ThemeId;
        
    public static ConfigIni GetInstance()

    {
        if (instance != null) return instance;
        GameObject obj = new GameObject
        {
            name = typeof(ConfigIni).ToString()
        };
        //过场景 不移除
        DontDestroyOnLoad(obj);
        instance = obj.AddComponent<ConfigIni>();

        return instance;
    }

    private void Awake()
    {
        IniPath = Application.streamingAssetsPath + "/Config.ini";
        IniReadFile(IniPath);
    }

    /// <summary>
    /// 读取配置文件
    /// </summary>
    /// <param name="path">配置文件路径</param>
    private void IniReadFile(string path)
    {
        CheckConfigFile();
        INIParser iniParser = new INIParser();
        iniParser.Open(path);
        ThemeId = iniParser.ReadValue("Setting", "ThemeId", 1);
        iniParser.Close();
    }
    /// <summary>
    /// 保存配置文件
    /// </summary>
    /// <param name="path">配置文件路径</param>
    public void Save()
    {
        CheckConfigFile();
        INIParser iniParser = new INIParser();
        iniParser.Open(IniPath);
        iniParser.WriteValue("Setting","ThemeId",ThemeId);
        iniParser.Close();
    }
    /// <summary>
    /// 检测配置文件是否存在
    /// </summary>
    /// <exception cref="FileNotFoundException"></exception>
    private void CheckConfigFile()
    {
        if (!File.Exists(IniPath))
        {
            throw (new FileNotFoundException($"配置文件{IniPath}不存在"));
        }
    }
}