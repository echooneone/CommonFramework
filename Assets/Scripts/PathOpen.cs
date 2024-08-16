
using System;
using System.IO;
using System.Reflection;
using Bitsplash.DatePicker;
using Doozy.Runtime.UIManager.Components;
using OfficeOpenXml;
using SFB;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
[RequireComponent(typeof(UIButton))]
public class PathOpen : MonoBehaviour
{
    public DatePickerDropDown StartDDD;
    public DatePickerDropDown EndDDD;

    public void OnClick()
    {
        var paths = StandaloneFileBrowser.OpenFolderPanel("Select Folder", "", true);
        if (paths.Length > 0)
        {
            string filePath = paths[0] + @"\Jaeger-LeCoultre_Data.xlsx";
        
            // Check if the file exists and delete it if it does
            if (File.Exists(filePath))
            {
                  File.Delete(filePath);
            }

            using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Data");//创建worksheet、
                
                worksheet.Cells[1, 1].Value = "Shop ID";
                worksheet.Cells[1, 2].Value = "Date";
                worksheet.Cells[1, 3].Value = "Stay Time(sec)";
                int colIndex = 4;
        
                if (MainSystem.Instance.Format.chapter1 != null &&
                    MainSystem.Instance.Format.chapter1.Length > 0)
                {
                    foreach (string title in MainSystem.Instance.Format.chapter1)
                    {
                        worksheet.Cells[1, colIndex].Value = title + " Play";
                        worksheet.Cells[1, colIndex+1].Value = title + " Stay(sec)";
                        colIndex += 2;
                    }
                }
                if (MainSystem.Instance.Format.chapter2 != null &&
                    MainSystem.Instance.Format.chapter2.Length > 0)
                {
                    foreach (string title in MainSystem.Instance.Format.chapter2)
                    {
                        worksheet.Cells[1, colIndex].Value = title + " Play";
                        worksheet.Cells[1, colIndex+1].Value = title + " Stay(sec)";
                        colIndex += 2;
                    }
                }
                if (MainSystem.Instance.Format.chapter3 != null &&
                    MainSystem.Instance.Format.chapter3.Length > 0)
                {
                    foreach (string title in MainSystem.Instance.Format.chapter3)
                    {
                        worksheet.Cells[1, colIndex].Value = title + " Play";
                        worksheet.Cells[1, colIndex+1].Value = title + " Stay(sec)";
                        colIndex += 2;
                    }
                }
                if (MainSystem.Instance.Format.chapter4 != null &&
                    MainSystem.Instance.Format.chapter4.Length > 0)
                {
                    foreach (string title in MainSystem.Instance.Format.chapter4)
                    {
                        worksheet.Cells[1, colIndex].Value = title + " Play";
                        worksheet.Cells[1, colIndex+1].Value = title + " Stay(sec)";
                        colIndex += 2;
                    }
                }
                if (MainSystem.Instance.Format.chapter5 != null &&
                    MainSystem.Instance.Format.chapter5.Length > 0)
                {
                    foreach (string title in MainSystem.Instance.Format.chapter5)
                    {
                        worksheet.Cells[1, colIndex].Value = title + " Play";
                        worksheet.Cells[1, colIndex+1].Value = title + " Stay(sec)";
                        colIndex += 2;
                    }
                }
                if (MainSystem.Instance.Format.chapter6 != null &&
                    MainSystem.Instance.Format.chapter6.Length > 0)
                {
                    foreach (string title in MainSystem.Instance.Format.chapter6)
                    {
                        worksheet.Cells[1, colIndex].Value = title + " Play";
                        worksheet.Cells[1, colIndex+1].Value = title + " Stay(sec)";
                        colIndex += 2;
                    }
                }
                int index = 2;
                int stay = 15;
                foreach (var keyValuePair in MainSystem.Instance.bundlesDict)
                {
                    string date = keyValuePair.Key;
                    DataBundle[] bundles = keyValuePair.Value;
                    Type type = typeof(DataBundle);
                
                    //获取所有属性
                    // PropertyInfo[] properties = type.GetProperties();
                    // foreach (PropertyInfo info in properties)
                    // {
                    //     print(date + " - " + info.Name + " :" + info.GetValue(bundles[0]));
                    // }
                    foreach (DataBundle data in bundles)
                    {
                        worksheet.Cells[index, 1].Value = MainSystem.Instance.Format.ShopId;
                        worksheet.Cells[index, 2].Value = Convert.ToDateTime(data.Date).ToString("yyyy-MM-dd");
                        
                        worksheet.Cells[index, 3].Value = 15 + data.Chapter1_Episode1_Stay +data.Chapter1_Episode2_Stay + data.Chapter1_Episode3_Stay +
                                                          data.Chapter2_Episode1_Stay +data.Chapter2_Episode2_Stay + data.Chapter2_Episode3_Stay +
                                                          data.Chapter3_Episode1_Stay +data.Chapter3_Episode2_Stay + data.Chapter3_Episode3_Stay +
                                                          data.Chapter4_Episode1_Stay +data.Chapter4_Episode2_Stay + data.Chapter4_Episode3_Stay +
                                                          data.Chapter5_Episode1_Stay +data.Chapter5_Episode2_Stay + data.Chapter5_Episode3_Stay +
                                                          data.Chapter6_Episode1_Stay +data.Chapter6_Episode2_Stay + data.Chapter6_Episode3_Stay;
                        colIndex = 4;
                        for (int c = 1; c <= 6; c++)
                        {
                            string chapterPropertyName = $"chapter{c}";
                            var chapterValue=typeof(FormatData).GetProperty(chapterPropertyName)?.GetValue(MainSystem.Instance.Format, null);
                            if (chapterValue != null )
                            {
                                int length = (int)chapterValue.GetType().GetProperty("Length").GetValue(chapterValue);
                                for (int i = 0; i < length; i++)
                                {
                                    string episodePlayPropertyName = $"Chapter{c}_Episode{i + 1}_Play";
                                    string episodeStayPropertyName = $"Chapter{c}_Episode{i + 1}_Stay";
                                    var episodePlayValue = typeof(DataBundle).GetProperty(episodePlayPropertyName)?.GetValue(data, null);
                                    var episodeStayValue = typeof(DataBundle).GetProperty(episodeStayPropertyName)?.GetValue(data, null);
                                    worksheet.Cells[index, colIndex].Value = episodePlayValue;
                                    worksheet.Cells[index, colIndex + 1].Value = episodeStayValue;
                                    colIndex += 2;  
                                }
                            }
                        }
                        index++;
                    }
                    
                }
                
                
                package.Save();//保存excel
            }
        }
    }

   public void OnDateClick()
    {
        var paths = StandaloneFileBrowser.OpenFolderPanel("Select Folder", "", true);
        DateTime startTime = StartDDD.DropDownContent.Content.Selection.GetItem(0);
        DateTime endTime=EndDDD.DropDownContent.Content.Selection.GetItem(0);
         if (paths.Length > 0)
        {
            string filePath = paths[0] + @$"\Jaeger-LeCoultre_{startTime:yyyy-MM-dd}_{endTime:yyyy-MM-dd}.xlsx";
        
            // Check if the file exists and delete it if it does
            if (File.Exists(filePath))
            {
                  File.Delete(filePath);
            }

            using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Data");//创建worksheet、
                
                worksheet.Cells[1, 1].Value = "Shop ID";
                worksheet.Cells[1, 2].Value = "Date";
                worksheet.Cells[1, 3].Value = "Stay Time(sec)";
                int colIndex = 4;
        
                if (MainSystem.Instance.Format.chapter1 != null &&
                    MainSystem.Instance.Format.chapter1.Length > 0)
                {
                    foreach (string title in MainSystem.Instance.Format.chapter1)
                    {
                        worksheet.Cells[1, colIndex].Value = title + " Play";
                        worksheet.Cells[1, colIndex+1].Value = title + " Stay(sec)";
                        colIndex += 2;
                    }
                }
                if (MainSystem.Instance.Format.chapter2 != null &&
                    MainSystem.Instance.Format.chapter2.Length > 0)
                {
                    foreach (string title in MainSystem.Instance.Format.chapter2)
                    {
                        worksheet.Cells[1, colIndex].Value = title + " Play";
                        worksheet.Cells[1, colIndex+1].Value = title + " Stay(sec)";
                        colIndex += 2;
                    }
                }
                if (MainSystem.Instance.Format.chapter3 != null &&
                    MainSystem.Instance.Format.chapter3.Length > 0)
                {
                    foreach (string title in MainSystem.Instance.Format.chapter3)
                    {
                        worksheet.Cells[1, colIndex].Value = title + " Play";
                        worksheet.Cells[1, colIndex+1].Value = title + " Stay(sec)";
                        colIndex += 2;
                    }
                }
                if (MainSystem.Instance.Format.chapter4 != null &&
                    MainSystem.Instance.Format.chapter4.Length > 0)
                {
                    foreach (string title in MainSystem.Instance.Format.chapter4)
                    {
                        worksheet.Cells[1, colIndex].Value = title + " Play";
                        worksheet.Cells[1, colIndex+1].Value = title + " Stay(sec)";
                        colIndex += 2;
                    }
                }
                if (MainSystem.Instance.Format.chapter5 != null &&
                    MainSystem.Instance.Format.chapter5.Length > 0)
                {
                    foreach (string title in MainSystem.Instance.Format.chapter5)
                    {
                        worksheet.Cells[1, colIndex].Value = title + " Play";
                        worksheet.Cells[1, colIndex+1].Value = title + " Stay(sec)";
                        colIndex += 2;
                    }
                }
                if (MainSystem.Instance.Format.chapter6 != null &&
                    MainSystem.Instance.Format.chapter6.Length > 0)
                {
                    foreach (string title in MainSystem.Instance.Format.chapter6)
                    {
                        worksheet.Cells[1, colIndex].Value = title + " Play";
                        worksheet.Cells[1, colIndex+1].Value = title + " Stay(sec)";
                        colIndex += 2;
                    }
                }
                int index = 2;
                foreach (var keyValuePair in MainSystem.Instance.bundlesDict)
                {
                    string date = keyValuePair.Key;
                    DataBundle[] bundles = keyValuePair.Value;
                    Type type = typeof(DataBundle);
                
                    //获取所有属性
                    // PropertyInfo[] properties = type.GetProperties();
                    // foreach (PropertyInfo info in properties)
                    // {
                    //     print(date + " - " + info.Name + " :" + info.GetValue(bundles[0]));
                    // }
                    foreach (DataBundle data in bundles)
                    {
                        if(Convert.ToDateTime(data.Date)>endTime||Convert.ToDateTime(data.Date)<startTime)
                            continue;
                        worksheet.Cells[index, 1].Value = MainSystem.Instance.Format.ShopId;
                        worksheet.Cells[index, 2].Value = Convert.ToDateTime(data.Date).ToString("yyyy-MM-dd");
                        
                        worksheet.Cells[index, 3].Value = 15 + data.Chapter1_Episode1_Stay +data.Chapter1_Episode2_Stay + data.Chapter1_Episode3_Stay +
                                                          data.Chapter2_Episode1_Stay +data.Chapter2_Episode2_Stay + data.Chapter2_Episode3_Stay +
                                                          data.Chapter3_Episode1_Stay +data.Chapter3_Episode2_Stay + data.Chapter3_Episode3_Stay +
                                                          data.Chapter4_Episode1_Stay +data.Chapter4_Episode2_Stay + data.Chapter4_Episode3_Stay +
                                                          data.Chapter5_Episode1_Stay +data.Chapter5_Episode2_Stay + data.Chapter5_Episode3_Stay +
                                                          data.Chapter6_Episode1_Stay +data.Chapter6_Episode2_Stay + data.Chapter6_Episode3_Stay;
                        colIndex = 4;
                        for (int c = 1; c <= 6; c++)
                        {
                            string chapterPropertyName = $"chapter{c}";
                            var chapterValue=typeof(FormatData).GetProperty(chapterPropertyName)?.GetValue(MainSystem.Instance.Format, null);
                            if (chapterValue != null )
                            {
                                int length = (int)chapterValue.GetType().GetProperty("Length").GetValue(chapterValue);
                                for (int i = 0; i < length; i++)
                                {
                                    string episodePlayPropertyName = $"Chapter{c}_Episode{i + 1}_Play";
                                    string episodeStayPropertyName = $"Chapter{c}_Episode{i + 1}_Stay";
                                    var episodePlayValue = typeof(DataBundle).GetProperty(episodePlayPropertyName)?.GetValue(data, null);
                                    var episodeStayValue = typeof(DataBundle).GetProperty(episodeStayPropertyName)?.GetValue(data, null);
                                    worksheet.Cells[index, colIndex].Value = episodePlayValue;
                                    worksheet.Cells[index, colIndex + 1].Value = episodeStayValue;
                                    colIndex += 2;  
                                }
                            }
                        }
                        index++;
                    }
                    
                }
                
                
                package.Save();//保存excel
            }
        }
    }

    private void Export()
    {
        //Jaeger-LeCoultre Data
 
    
    }
}