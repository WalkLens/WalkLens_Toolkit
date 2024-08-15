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
    public GameObject eyegazeUI;
    public float yOffset = 0.0f;
    public DataManagerCtrl dataManagerCtrl;
    public string myPinNum;
    public GameObject flexibleButton;
    private GameObject eyegazeUIClone;

    private void Awake()
    {
        main = this;
    }

    public void ActivateEyegazeUI(RaycastHit photonUser)
    {
        PhotonUser photonUserInfo = photonUser.collider.GetComponent<PhotonUser>();
        string pinNum = photonUserInfo.GetPIN();

        Vector3 newPosition = photonUserInfo.gameObject.transform.position + Vector3.up * yOffset;

        if (myPinNum != pinNum)
        {
            eyegazeUIClone = Instantiate(eyegazeUI, newPosition, Quaternion.identity);

            Transform cameraTransform = Camera.main.transform;
            eyegazeUIClone.transform.LookAt(cameraTransform);
            eyegazeUIClone.transform.Rotate(0, 180, 0);

            Transform eyegazeLeft = eyegazeUIClone.transform.Find("Eyegaze_Left");
            Transform eyegazeRight = eyegazeUIClone.transform.Find("Eyegaze_Right");

            TextMeshProUGUI[] leftInfo = eyegazeLeft.gameObject.GetComponentsInChildren<TextMeshProUGUI>();

            DatabaseOnLeft(pinNum, leftInfo);
            DatabaseOnRight(pinNum, eyegazeRight);
        }
        else
        {
            Debug.Log("You can't see me!!!");
        }
    }

    public void DeactivateEyegazeUI()
    {
        if (eyegazeUIClone != null)
        {
            Destroy(eyegazeUIClone);
        }
    }

    public async void DatabaseOnLeft(string hitPIN, TextMeshProUGUI[] _profileInfo)
    {
        string rowKey = hitPIN;

        if (dataManagerCtrl != null && dataManagerCtrl.IsReady)
        {
            var user = await dataManagerCtrl.LoadUser(rowKey);
            if (user != null)
            {
                _profileInfo[0].text = user.Name;
                _profileInfo[1].text = user.Generation + " " + user.Group + ", " + user.Job;
                _profileInfo[2].text = user.University + "\n" + user.Major;
                _profileInfo[3].text = user.CompanyName + " " + user.Duty;
                _profileInfo[4].text = user.Project;
                _profileInfo[5].text = user.SelfIntroduction;
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

    public async void DatabaseOnRight(string hitPIN, Transform eyegazeRight)
    {
        string rowKey = hitPIN;

        if (dataManagerCtrl != null && dataManagerCtrl.IsReady)
        {
            var user = await dataManagerCtrl.LoadUser(rowKey);
            if (user != null)
            {
                HorizontalLayoutGroup[] layout = eyegazeRight.gameObject.GetComponentsInChildren<HorizontalLayoutGroup>(); // 0~2 : Skill, 3~6 : INTERESTS
                foreach (HorizontalLayoutGroup group in layout)
                {
                    Debug.Log("Grid : " + group.name);
                }

                string[] skillSplit = user.Skill.Split(",");
                string[] interestsSplit = user.Interest.Split(",");

                // foreach (string skillText in skillSplit)
                // {
                //     Debug.Log("skillSplit : " + skillText);
                // }
                // foreach (string interestText in interestsSplit)
                // {
                //     Debug.Log("interestSplit : " + interestText);
                // }

                int skillIndex = 0;
                for (int i = 0; i < skillSplit.Length; i++)
                {
                    GameObject buttonClone = Instantiate(flexibleButton);
                    TextMeshProUGUI text = buttonClone.GetComponentInChildren<TextMeshProUGUI>();
                    text.text = skillSplit[i];
                    buttonClone.transform.SetParent(layout[skillIndex].transform, false);
                    LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)buttonClone.transform);

                    if ((i + 1) % 4 == 0)
                    {
                        skillIndex++;
                        if (skillIndex >= layout.Length)
                        {
                            skillIndex = layout.Length - 1;
                        }
                    }
                }
                int interestIndex = 3;
                for (int i = 0; i < interestsSplit.Length; i++)
                {
                    GameObject buttonClone = Instantiate(flexibleButton);
                    TextMeshProUGUI text = buttonClone.GetComponentInChildren<TextMeshProUGUI>();
                    text.text = interestsSplit[i];
                    buttonClone.transform.SetParent(layout[interestIndex].transform, false);
                    LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)buttonClone.transform);

                    if ((i + 1) % 4 == 0)
                    {
                        interestIndex++;
                        if (interestIndex >= layout.Length)
                        {
                            interestIndex = layout.Length - 1;
                        }
                    }
                }

                foreach (var e in layout)
                {
                    LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)e.gameObject.transform);
                }
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


