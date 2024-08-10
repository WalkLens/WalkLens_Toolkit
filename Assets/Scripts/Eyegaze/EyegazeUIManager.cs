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

    public void ActivateEyegazeUI(RaycastHit hit)
    {
        PhotonUser photonUserInfo = hit.collider.GetComponent<PhotonUser>();
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
                    buttonClone.transform.SetParent(layout[skillIndex].transform, false);
                    TextMeshProUGUI text = buttonClone.GetComponentInChildren<TextMeshProUGUI>();
                    text.text = skillSplit[i];
                    // UpdateMinWidth(buttonClone);

                    if ((i + 1) % 5 == 0)
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
                    buttonClone.transform.SetParent(layout[interestIndex].transform, false);
                    TextMeshProUGUI text = buttonClone.GetComponentInChildren<TextMeshProUGUI>();
                    text.text = interestsSplit[i];
                    // UpdateMinWidth(buttonClone);

                    if ((i + 1) % 5 == 0)
                    {
                        interestIndex++;
                        if (interestIndex >= layout.Length)
                        {
                            interestIndex = layout.Length - 1;
                        }
                    }
                }
                // _skillAndInterestInfo[0].text = "SKILLS";
                // _skillAndInterestInfo[1].text = skillSplit[0];
                // _skillAndInterestInfo[2].text = skillSplit[1];
                // _skillAndInterestInfo[3].text = skillSplit[2];
                // _skillAndInterestInfo[4].text = skillSplit[3];
                // _skillAndInterestInfo[2].text = user.University + "\n" + user.Major;
                // _skillAndInterestInfo[3].text = user.CompanyName + " " + user.Duty;
                // _skillAndInterestInfo[4].text = user.Project;
                // _skillAndInterestInfo[5].text = user.SelfIntroduction;
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

    public void UpdateMinWidth(GameObject button)
    {
        Vector3[] corners = new Vector3[4];
        RectTransform textRect = button.transform.GetComponent<RectTransform>();
        LayoutElement layoutElement= button.GetComponent<LayoutElement>();
        textRect.GetWorldCorners(corners);
        double preferredWidth = Vector3.Distance(corners[0], corners[3]) * 200 * 6.024 * 2;

        // 부모 오브젝트의 Layout Element의 min width 설정
        layoutElement.minWidth = (float)preferredWidth;
    }
}


