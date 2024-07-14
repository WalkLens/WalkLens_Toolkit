using MRTK.Tutorials.AzureCloudServices.Scripts.Managers;
using MRTK.Tutorials.MultiUserCapabilities;
using RealityCollective.Extensions;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EyegazeUIManager : MonoBehaviour
{
    public static EyegazeUIManager main;
    public GameObject photonInfoUI;
    public float yOffset = 0.0f;
    public TextMeshProUGUI nameLoad;
    public TextMeshProUGUI jobLoad;
    public TextMeshProUGUI hobbyLoad;
    private GameObject photonInfoUIClone;
    private DataManagerCtrl dataManagerCtrl;

    private void Awake()
    {
        main = this;
    }

    public void ActivateEyegazeUI(RaycastHit hit)
    {
        PhotonUser photonUserInfo = hit.collider.GetComponent<PhotonUser>();
        string pinNum = photonUserInfo.GetPIN();
        
        Vector3 newPosition = hit.point + Vector3.up * yOffset;

        photonInfoUIClone = Instantiate(photonInfoUI, newPosition, Quaternion.identity);

        Transform cameraTransform = Camera.main.transform;
        photonInfoUIClone.transform.LookAt(cameraTransform);
        photonInfoUIClone.transform.Rotate(0, 180, 0);

        //TextMeshProUGUI textMeshPro = photonInfoUIClone.GetComponentInChildren<TextMeshProUGUI>();
        //textMeshPro.text = photonUserInfo.GetNickName();

        DatabaseOnLoad(pinNum);
    }

    public void DeactivateEyegazeUI()
    {
        if (photonInfoUIClone != null)
        {
            Destroy(photonInfoUIClone);
        }
    }

    public async void DatabaseOnLoad(string hitPIN)
    {
        string rowKey = hitPIN;

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
