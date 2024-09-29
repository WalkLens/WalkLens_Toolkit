using UnityEngine;
using GoogleSheetsToUnity;
using System.Collections.Generic;
using System;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "Reader", menuName = "Scriptable Object/ItemDataReader", order = int.MaxValue)]
public class ItemDataReader : DataReaderBase
{
    [Header("스프레드시트에서 읽혀져 직렬화 된 오브젝트")][SerializeField] public List<UserData> DataList = new List<UserData>();

    // UpdateStats -> 구글 스프레드 시트의 데이터 읽어오기
    
    internal void UpdateStats(List<GSTU_Cell> list, int itemID)
    {
        int partitionKey = -1; string userName = null; int password = -1;
        string university = null; string major = null; string selfIntroduction = null;
        string xrealGroup = null; int xrealGeneration = -1; string xrealProjects = null;
        string companyJob = null; string companyName = null; string companyDuty = null;
        string skills = null; string interests = null;    
    
        for (int i = 0; i < list.Count; i++)
        {
            switch (list[i].columnId)
            {
                case "Partition_Key":
                    partitionKey = (int)int.Parse(list[i].value);
                    break;
                case "User_Name":
                    userName = list[i].value;
                    break;
                case "Password":
                    password = (int)int.Parse(list[i].value);
                    break;
                case "University":
                    university = list[i].value;
                    break;
                case "Major":
                    major = list[i].value;
                    break;
                case "Self_Introduction":
                    selfIntroduction = list[i].value; 
                    break;
                case "XREAL_Group":
                    xrealGroup = list[i].value; 
                    break;
                case "XREAL_Generation":
                    xrealGeneration = (int)int.Parse(list[i].value);; 
                    break;
                case "XREAL_Projects":
                    xrealProjects = list[i].value; 
                    break;
                case "Company_Job":
                    companyJob = list[i].value; 
                    break;
                case "Company_Name":
                    companyName = list[i].value; 
                    break;
                case "Company_Duty":
                    companyDuty = list[i].value; 
                    break;
                case "Skills":
                    skills = list[i].value;
                    break;
                case "Interests":
                    interests = list[i].value;
                    break;
            }
        }

        Debug.Log($"Add User Data {list.Count}");
        DataList.Add(new UserData(
            partitionKey, userName, password
            , university, major, selfIntroduction
            , xrealGroup, xrealGeneration, xrealProjects
            , companyJob, companyName, companyDuty
            , skills, interests));
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(ItemDataReader))]
public class ItemDataReaderEditor : Editor
{
    ItemDataReader data;

    void OnEnable()
    {
        data = (ItemDataReader)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Label("\n\n스프레드 시트 읽어오기");

        if (GUILayout.Button("데이터 읽기(API 호출)"))
        {
            UpdateStats(UpdateMethodOne);
            data.DataList.Clear();
        }
        
        GUILayout.Label("Add New Data");
        if (GUILayout.Button("Add Via Append"))
        {
            AppendToSheet();
        }
    }

    void UpdateStats(UnityAction<GstuSpreadSheet> callback, bool mergedCells = false)
    {
        SpreadsheetManager.Read(new GSTU_Search(data.associatedSheet, data.associatedWorksheet), callback, mergedCells);
    }
    // SpreadsheetManager.Write()로 쓴다.
    
    // 기존 데이터에 새로운 데이터 타입을 추가한다.
    void WriteAdditionally(UnityAction<GstuSpreadSheet> callback, UserData newUserData)
    {
        // SpreadsheetManager.WriteBatch();
    }

    void UpdateMethodOne(GstuSpreadSheet ss)
    {
        for (int i = data.START_ROW_LENGTH; i <= data.END_ROW_LENGTH; ++i)
        {
            data.UpdateStats(ss.rows[i], i);
        }

        EditorUtility.SetDirty(target);
    }
    
    
    /// <summary>
    /// Appends the new User Data to the spreadsheet online
    /// </summary>
    void AppendToSheet()
    {
        // TODO : PC 뷰에서 선택된 데이터 UserData 타입으로 불러오기
        UserData newUserData = new UserData();
        
        List<string> list = new List<string>() {
            newUserData.partitionKey.ToString(),
            newUserData.userName,
            newUserData.password.ToString(),
            
            newUserData.university,
            newUserData.major,
            newUserData.selfIntroduction,
            
            newUserData.xrealGroup,
            newUserData.xrealGeneration.ToString(),
            newUserData.xrealProjects,
            
            newUserData.companyJob,
            newUserData.companyName,
            newUserData.companyDuty,
            
            newUserData.skills,
        };

        Debug.Log("Append new Data To Google Sheet");
        SpreadsheetManager.Append(new GSTU_Search(data.associatedSheet, data.associatedWorksheet), new ValueRange(list), null);
    }
}
#endif