using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MRTK.Tutorials.AzureCloudServices.Scripts.Managers;

public class UIManager : MonoBehaviour
{
    [Header("User Info")]
    public TMP_InputField partitionKeyInput;
    public TMP_InputField nameInput;
    public TMP_InputField passwordInput;
    public TMP_InputField universityInput;
    public TMP_InputField majorInput;
    public TMP_InputField selfIntroductionInput;
    
    [Header("XREAL Info")]
    public TMP_Text groupBlank;
    public TMP_Text generationBlank;
    public TMP_Text projectBlank;
    
    
    public TMP_Text jobBlank;
    [Header("Job")]
    public TMP_InputField jobInput;
    public TMP_InputField companyNameInput;
    public TMP_InputField dutyInput;

    public TMP_Text skillBlank;
    public TMP_Text interestBlank;

    public TMP_InputField interestInput;
    
    public DataManagerCtrl dataManagerCtrl;

    public TMP_InputField numInput;

    public TMP_Text nameLoad;
    public TMP_Text jobLoad;
    public TMP_Text hobbyLoad;

    [Header("List")] 
    public List<string> group;
    public List<string> project;
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
        jobBlank.text = "";
        // hobbyBlank.text = "";

        // Add listeners to input fields to open keyboard when selected
        nameInput.onSelect.AddListener(delegate { OpenSystemKeyboard(nameInput); });
        numInput.onSelect.AddListener(delegate { OpenSystemKeyboard(numInput); });
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
    }

    public void OpenSystemKeyboard(TMP_InputField inputField)
    {
        keyboard = TouchScreenKeyboard.Open(inputField.text, TouchScreenKeyboardType.Default, false, false, false, false);
    }

    public void OnJobButtonClicked(string job)
    {
        jobBlank.text = job;
    }

    /*public void OnHobbyButtonClicked(string hobby)
    {
        hobbyBlank.text = hobby;
    }*/

    public void OnSaveButtonClicked()
    {
        string partitionKey = partitionKeyInput.text;
        string name = nameInput.text;
        string password = passwordInput.text;
        string university = universityInput.text;
        string major = majorInput.text;
        string selfIntroduction = selfIntroductionInput.text;
        
        string[] groupArray = group.ToArray();
        string generation = generationBlank.text;
        string[] projectArray = project.ToArray();
        
        string job = jobBlank.text;
        string companyName = companyNameInput.text;
        string duty = dutyInput.text;

        string[] skillArray = skill.ToArray();
        string[] interestArray = interest.ToArray();

        if (dataManagerCtrl != null && dataManagerCtrl.IsReady)
        {
            dataManagerCtrl.SaveUser(partitionKey, name, password, university, major, selfIntroduction, 
                groupArray, generation,  projectArray, 
                job, companyName, duty, 
                skillArray, interestArray);
        }
        else
        {
            Debug.LogError("DataManagerCtrl is not ready.");
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
