using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleSheetsToUnity;
using UnityEngine.Serialization;

#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public struct UserData
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
    [TextArea] public string selfIntroduction;

    
    [Header("XREAL Info")]
    public string xrealGroup;
    [Range(1,7)]
    public int xrealGeneration;
    public string xrealProjects;

    
    [Header("Job")]
    [Tooltip("Enter your job like 'Backend Developer'.")]
    public string companyJob;
    public string companyName;
    [Tooltip("Enter your duty like 'Senior Developer'.")]
    public string companyDuty;

    
    [Header("Personal Info")] 
    [Tooltip("Enter Skills you have. EX) 3D Modeling, UI Design, etc")]
    public string skills;
    [Tooltip("Enter Interests you have. EX) AR, 3D UX, Machine Learning, etc")]
    public string interests;

    public UserData(int partitionKey, string userName, int password
        , string university, string major, string selfIntroduction
        , string xrealGroup, int xrealGeneration, string xrealProjects
        , string companyJob, string companyName, string companyDuty
        , string skills, string interests)
    {
        this.partitionKey = partitionKey;
        this.userName = userName;
        this.password = password;

        this.university = university;
        this.major = major;
        this.selfIntroduction = selfIntroduction;

        this.xrealGroup = xrealGroup;
        this.xrealGeneration = xrealGeneration;
        this.xrealProjects = xrealProjects;

        this.companyJob = companyJob;
        this.companyName = companyName;
        this.companyDuty = companyDuty;

        this.skills = skills;
        this.interests = interests;
    }
}
















