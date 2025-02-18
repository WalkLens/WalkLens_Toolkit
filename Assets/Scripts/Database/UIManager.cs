using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MRTK.Tutorials.AzureCloudServices.Scripts.Managers;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    [Header("User Info")]
    public TMP_InputField partitionKeyInput;
    public TMP_Text pinNumber;
    public TMP_InputField nameInput;
    [FormerlySerializedAs("passwordInput")] public string password;
    public TMP_InputField universityInput;
    public TMP_InputField majorInput;
    public TMP_InputField selfIntroductionInput;
    
    [Header("XREAL Info")]
    public List<string> group;
    public string generation;
    public List<string> project;
    
    [FormerlySerializedAs("jobInput")] [Header("Job")]
    public string job;
    public TMP_InputField companyNameInput;
    public TMP_InputField dutyInput;
    
    public DataManagerCtrl dataManagerCtrl;

    public TMP_InputField numInput;

    public TMP_Text nameLoad;
    public TMP_Text jobLoad;
    public TMP_Text hobbyLoad;

    
    [Space]
    public List<string> skill;
    public List<string> interest;

    public Button button;
    [SerializeField]
    private TouchScreenKeyboard keyboard;
    
    /*(string partitionKey, string name, string password, string university, string major, [CanBeNull] string selfIntroduction, 
        string group, string generation, string project, [CanBeNull] string job, [CanBeNull] string companyName, [CanBeNull] string duty, 
        string skill, string interest)*/
    private void Start()
    {
        nameInput.onSelect.AddListener(delegate { OpenSystemKeyboard(nameInput); });
        // button.onClick.AddListener(OnSaveButtonClicked);
    }

    public void SetButtonsAction()
    {
        // TODO : button에 해당하는 listName, value 기입
        button.onClick.AddListener(delegate { AddString("group", "Developer"); });
        button.onClick.AddListener(delegate { RemoveString("group", "Developer"); });
    }
    private void Update()
    {
        if (keyboard != null)
        {
            // Example of how to read the keyboard input and update the input field
            if (keyboard.status == TouchScreenKeyboard.Status.Visible)
            {
                // You can add code here to update your input fields if necessary
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            LogInputData();
        }
    }

    public void OpenSystemKeyboard(TMP_InputField inputField)
    {
        keyboard = TouchScreenKeyboard.Open(inputField.text, TouchScreenKeyboardType.Default, false, false, false, false);
    }

    private void LogInputData()
    {
        Debug.Log($"pinNumber: {pinNumber.text}, nameInput: {nameInput.text}, passwordInput: {password}, universityInput: {universityInput.text}, majorInput: {majorInput.text}");
        Debug.Log($"selfIntroductionInput : {selfIntroductionInput.text}");
        Debug.Log($"group: {dataManagerCtrl.GetStringValue(group.ToArray())}, generation: {generation}, project: {dataManagerCtrl.GetStringValue(project.ToArray())}");
        Debug.Log($"job: {job}, company: {companyNameInput.text}, duty: {dutyInput.text}");
        Debug.Log($"skill: {dataManagerCtrl.GetStringValue(skill.ToArray())}, interest: {dataManagerCtrl.GetStringValue(interest.ToArray())}");
    }
    
    public void OnSaveButtonClicked()
    {
        int partitionKey = UserEntityManager.GetIntValue(pinNumber.text); // partitionKeyInput.text;
        string name = nameInput.text;
        int password = UserEntityManager.GetIntValue(pinNumber.text);
        
        string university = universityInput.text;
        string major = majorInput.text;
        string selfIntroduction = selfIntroductionInput.text;

        string xrealGroup = string.Join("_", group);// string[] groupArray = group.ToArray();
        string xrealGeneration = generation;
        string xrealProjects = string.Join("_", project); //string[] projectArray = project.ToArray();
        
        string companyJob = job;
        string companyName = companyNameInput.text;
        string companyDuty = dutyInput.text;

        string skills = string.Join("_", skill);// string[] skillArray = skill.ToArray();
        string interests = string.Join("_", interest);// string[] interestArray = interest.ToArray();
        
        UserData inputData = new UserData(partitionKey, name, password,
                                            university, major, selfIntroduction,
                                            xrealGroup, xrealGeneration, xrealProjects,
                                            companyJob, companyName, companyDuty,
                                                skills, interests);
        ItemDataReaderEditor.AppendToSheet(inputData);
        
        if (dataManagerCtrl != null && dataManagerCtrl.IsReady)
        {
            // Google Sheet DB로 변경
            
            // Azure DB  사용 X
            /*dataManagerCtrl.SaveUser(partitionKey, name, password, university, major, selfIntroduction, 
                groupArray, generation,  projectArray, 
                job, companyName, duty, 
                skillArray, interestArray);*/
        }
        else
        {
            Debug.LogError("DataManagerCtrl is not ready.");
        }
    }

    public void SetButtonString(string listName, string value, bool isToggleOn)
    {
        if (isToggleOn)
        {
            AddString(listName, value);
        }
        else
        {
            RemoveString(listName, value);
        }
    }
    // Method to add a string to the list (prevents duplicates)
    public void AddString(string listName, string value)
    {
        List<string> list = new List<string>();
        switch (listName)
        {
            case "group":
                list = new List<string>(group);
                break;
            case "project":
                list = new List<string>(project);
                break;
            case "skill":
                list = new List<string>(skill);
                break;
            case "interest":
                list = new List<string>(interest);
                break;
            default:
                Debug.LogWarning("List name match failed");
                break;
        }
        
        if (!list.Contains(value))
        {
            list.Add(value);
            Debug.Log($"'{value}' was added to the list.");
        }
        else
        {
            Debug.Log($"'{value}' is already in the list.");
        }
    }

    // Method to remove a string from the list
    public void RemoveString(string listName, string str)
    {
        List<string> list = new List<string>();
        switch (listName)
        {
            case "group":
                list = new List<string>(group);
                break;
            case "project":
                list = new List<string>(project);
                break;
            case "skill":
                list = new List<string>(skill);
                break;
            case "interest":
                list = new List<string>(interest);
                break;
            default:
                Debug.LogWarning("List name match failed");
                break;
        }
        bool isRemoved = list.Remove(str);
        if (isRemoved)
        {
            Debug.Log($"'{str}' was removed from the list.");
        }
        else
        {
            Debug.Log($"'{str}' was not found in the list.");
        }
    }
    public async void OnLoadButtonClicked()
    {
        string rowKey = numInput.text;

        if (dataManagerCtrl != null && dataManagerCtrl.IsReady)
        {
            var user = await dataManagerCtrl.LoadUser(rowKey);
            if (user != null)
            {
                nameLoad.text = user.Name;
                jobLoad.text = user.Job;
                hobbyLoad.text = user.Group;
            }
            else
            {
                Debug.LogError("No user found with the specified RowKey.");
            }
        }
        else
        {
            Debug.LogError("DataManagerCtrl is not ready.");
        }
    }
}
