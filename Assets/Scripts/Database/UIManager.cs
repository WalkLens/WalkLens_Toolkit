using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MRTK.Tutorials.AzureCloudServices.Scripts.Managers;

public class UIManager : MonoBehaviour
{
    public TMP_InputField nameInput;
    public TMP_InputField universityInput;
    public TMP_InputField majorInput;
    public TMP_InputField interestInput;
    public TMP_Text jobBlank;
    public TMP_Text hobbyBlank;

    public DataManagerCtrl dataManagerCtrl;

    public TMP_InputField numInput;

    public TMP_Text nameLoad;
    public TMP_Text jobLoad;
    public TMP_Text hobbyLoad;

    [SerializeField]
    private TouchScreenKeyboard keyboard;

    private void Start()
    {
        jobBlank.text = "";
        hobbyBlank.text = "";

        // Add listeners to input fields to open keyboard when selected
        nameInput.onSelect.AddListener(delegate { OpenSystemKeyboard(nameInput); });
        numInput.onSelect.AddListener(delegate { OpenSystemKeyboard(numInput); });
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

    public void OnHobbyButtonClicked(string hobby)
    {
        hobbyBlank.text = hobby;
    }

    public void OnSaveButtonClicked()
    {
        string name = nameInput.text;
        string university = universityInput.text;
        string major = majorInput.text;
        string job = jobBlank.text;
        string hobby = hobbyBlank.text;
        string[] interests = new[] { "AR", "Web", "Unity", "UI/UX", "DataAnalysis" };
        if (dataManagerCtrl != null && dataManagerCtrl.IsReady)
        {
            dataManagerCtrl.SaveUser(name, job, university, major, interests, hobby);
        }
        else
        {
            Debug.LogError("DataManagerCtrl is not ready.");
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
                hobbyLoad.text = user.Hobby;
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
