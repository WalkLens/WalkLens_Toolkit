using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MRTK.Tutorials.AzureCloudServices.Scripts.Managers;
using UnityEngine.Serialization;
using MixedReality.Toolkit.UX;
using RealityCollective.Extensions;

public class ARUIManager : MonoBehaviour
{
    [Header("XREAL Info")]
    public PressableButton[] xrealButtons;
    public PressableButton[] generationButtons;
    public PressableButton[] projectButtons;

    [Header("SKILLS Info")]
    public PressableButton[] researchButtons;
    public PressableButton[] designButtons;
    public PressableButton[] devButtons;

    [Header("INTERESTS Info")]
    public PressableButton[] interestButtons;
    public DataManagerCtrl dataManagerCtrl;

    [Header("Matched Profile")]
    public GameObject searchUI;
    public MatchedProfileDialog matchedProfilePrefab;
    public GameObject notmatched;
    public Transform content;

    private List<string> xreal = new List<string>();
    private string generation;
    private List<string> project = new List<string>();
    private List<string> skill = new List<string>();
    private List<string> interest = new List<string>();

    // private List<string> nameLoad, selfIntroduction, university, major, jobLoad, hobbyLoad;

    void Start()
    {
        // XREAL 버튼에 이벤트 리스너 추가
        foreach (var button in xrealButtons)
        {
            AddToggleListener(button, "XREAL");
        }

        // Generation 버튼에 이벤트 리스너 추가
        foreach (var button in generationButtons)
        {
            AddToggleListener(button, "Generation");
        }

        // Project 버튼에 이벤트 리스너 추가
        foreach (var button in projectButtons)
        {
            AddToggleListener(button, "Project");
        }

        // Research 버튼에 이벤트 리스너 추가
        foreach (var button in researchButtons)
        {
            AddToggleListener(button, "Research");
        }

        // Design 버튼에 이벤트 리스너 추가
        foreach (var button in designButtons)
        {
            AddToggleListener(button, "Design");
        }

        // Dev 버튼에 이벤트 리스너 추가
        foreach (var button in devButtons)
        {
            AddToggleListener(button, "Develop");
        }

        // Interest 버튼에 이벤트 리스너 추가
        foreach (var button in interestButtons)
        {
            AddToggleListener(button, "Interest");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            LogInputData();
        }
    }

    private void LogInputData()
    {
        Debug.Log($"xreal: {GetStringValue(xreal.ToArray())}, generation: {generation}, project: {GetStringValue(project.ToArray())}, skill: {GetStringValue(skill.ToArray())}, interest: {GetStringValue(interest.ToArray())}");
        // Debug.Log($"selfIntroductionInput : {selfIntroductionInput.text}");
        // Debug.Log($"group: {dataManagerCtrl.GetStringValue(group.ToArray())}, generation: {generation}, project: {dataManagerCtrl.GetStringValue(project.ToArray())}");
        // Debug.Log($"job: {job}, company: {companyNameInput.text}, duty: {dutyInput.text}");
        // Debug.Log($"skill: {dataManagerCtrl.GetStringValue(skill.ToArray())}, interest: {dataManagerCtrl.GetStringValue(interest.ToArray())}");
    }

    public string GetStringValue(string[] values)
    {
        // 배열 요소들을 콤마로 구분된 하나의 문자열로 결합합니다.
        return string.Join(",", values);
    }

    private void AddToggleListener(PressableButton button, string category)
    {
        button.OnClicked.AddListener(() =>
            {
                if (button.IsToggled)
                {
                    OnButtonToggled(button, category);
                }
                else
                {
                    OnButtonUntoggled(button, category);
                }
            });
    }

    private void OnButtonToggled(PressableButton button, string category)
    {
        TextMeshProUGUI text = button.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log($"{category} button toggled on: {text.text}");
        switch (category)
        {
            case "XREAL":
                xreal.Add(text.text);
                break;
            case "Generation":
                generation = text.text;
                break;
            case "Project":
                project.Add(text.text);
                break;
            case "Research":
            case "Design":
            case "Develop":
                skill.Add(text.text);
                break;
            case "Interest":
                interest.Add(text.text);
                break;
        }
    }

    private void OnButtonUntoggled(PressableButton button, string category)
    {
        TextMeshProUGUI text = button.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log($"{category} button untoggled on: {text.text}");
        switch (category)
        {
            case "XREAL":
                xreal.Remove(text.text);
                break;
            case "Generation":
                generation = "";
                break;
            case "Project":
                project.Remove(text.text);
                break;
            case "Research":
            case "Design":
            case "Develop":
                skill.Remove(text.text);
                break;
            case "Interest":
                interest.Remove(text.text);
                break;
        }
    }

    public void OnSearchButtonClicked()
    {
        Debug.Log("Search Button Clicked.");
        string xrealGroup = GetStringValue(xreal.ToArray());
        string projectGroup = GetStringValue(project.ToArray());
        string skillGroup = GetStringValue(skill.ToArray());
        string interestGroup = GetStringValue(interest.ToArray());

        DatabaseSearch(xrealGroup, projectGroup, skillGroup, interestGroup);
    }

    public async void DatabaseSearch(string xreal, string project, string skill, string interest)
    {
        Debug.Log("Database Search.");

        if (dataManagerCtrl != null && dataManagerCtrl.IsReady)
        {
            // 비동기 메서드 호출 시 await 사용
            var userList = await dataManagerCtrl.FilterUsers(xreal, generation, project, skill, interest);

            if (userList != null)
            {
                Debug.Log($"userList : {userList}");
                if (userList.Count > 0)
                {
                    Debug.Log($"userList is more than 0");
                    foreach (var user in userList)
                    {
                        Debug.Log($"Name : {user.Name}, Group : {user.Group}, Generation : {user.Generation}, Project : {user.Project}, Skill : {user.Skill}, Interest : {user.Interest}");
                    }
                }
                else
                {
                    Debug.LogError("No user found with the specified User.");
                    notmatched.SetActive(true);
                    searchUI.SetActive(false);
                }
            }
            else
            {
                Debug.LogError("FilterUsers returned null.");
            }
        }
        else
        {
            Debug.LogError("DataManagerCtrl is not ready.");
        }
    }
}
