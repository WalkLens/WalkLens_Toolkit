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

    public void OnLoadButtonClicked()
    {
        // Implement loading functionality if required
    }
}
