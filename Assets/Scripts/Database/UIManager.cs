using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MRTK.Tutorials.AzureCloudServices.Scripts.Managers;

public class UIManager : MonoBehaviour
{
    public TMP_InputField nameInput;
    public TMP_Text jobBlank;
    public TMP_Text hobbyBlank;

    public DataManagerCtrl dataManagerCtrl;

    public TMP_InputField numInput;

    public TMP_Text nameLoad;
    public TMP_Text jobLoad;
    public TMP_Text hobbyLoad;

    private void Start()
    {
        jobBlank.text = "";
        hobbyBlank.text = "";
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
        string job = jobBlank.text;
        string hobby = hobbyBlank.text;

        if (dataManagerCtrl != null && dataManagerCtrl.IsReady)
        {
            dataManagerCtrl.SaveUser(name, job, hobby);
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
