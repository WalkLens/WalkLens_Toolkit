using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleSheetsToUnity;
using UnityEngine.Serialization;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "Survey Data", menuName = "Scriptable Object/User Data", order = int.MaxValue)]
public class UserData : ScriptableObject
{
    [Header("User Data(SO)")]
    [Range(0,9999)]
    public int partitionKey;
    public string userName;
    [Range(0,9999)]
    [Tooltip("정수형 비밀 번호로 제한")]
    public int password;
    
    
    [Space]
    [Tooltip("Enter university name like '서울대학교'.")]
    public string university;
    public string major;
    public string selfIntroduction;

    
    [Header("XREAL Info")]
    public XREAL_GROUP xrealGroup;
    [Range(1,7)]
    public int xrealGeneration;
    public string xrealProjects;

    
    [Header("Job")] 
    public string job;
    public string companyName;
    public string companyDuty;

    
    [Header("Personal Info")] 
    [Tooltip("Enter Skills you have. EX) 3D Modeling, UI Design, etc")]
    public string skills;
    [Tooltip("Enter Interests you have. EX) AR, 3D UX, Machine Learning, etc")]
    public string interest;

}

public enum XREAL_GROUP
{
    Developer,
    Designer,
    Researcher
}
















