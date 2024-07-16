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
    public DataManagerCtrl dataManagerCtrl;
    public string myPinNum;
    private GameObject photonInfoUIClone;

    private void Awake()
    {
        main = this;
    }

    public void ActivateEyegazeUI(RaycastHit hit)
    {
        PhotonUser photonUserInfo = hit.collider.GetComponent<PhotonUser>();
        string pinNum = photonUserInfo.GetPIN();
        
        Vector3 newPosition = hit.point + Vector3.up * yOffset;

        if(myPinNum != pinNum)
        {
            photonInfoUIClone = Instantiate(photonInfoUI, newPosition, Quaternion.identity);
        }
        else
        {
            Debug.Log("You can't see me!!!");
        }


        Transform cameraTransform = Camera.main.transform;
        photonInfoUIClone.transform.LookAt(cameraTransform);
        photonInfoUIClone.transform.Rotate(0, 180, 0);

        TextMeshProUGUI[] profileInfo = photonInfoUIClone.GetComponentsInChildren<TextMeshProUGUI>();

        DatabaseOnLoad(pinNum, profileInfo);
    }

    public void DeactivateEyegazeUI()
    {
        if (photonInfoUIClone != null)
        {
            Destroy(photonInfoUIClone);
        }
    }

    public async void DatabaseOnLoad(string hitPIN, TextMeshProUGUI[] _profileInfo)
    {
        string rowKey = hitPIN;

        if (dataManagerCtrl != null && dataManagerCtrl.IsReady)
        {
            var user = await dataManagerCtrl.LoadUser(rowKey);
            if (user != null)
            {
                _profileInfo[0].text += user.Name;
                _profileInfo[1].text += user.Job;
                _profileInfo[2].text += user.Hobby;
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
