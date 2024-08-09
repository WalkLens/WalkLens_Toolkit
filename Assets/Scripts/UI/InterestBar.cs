using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InterestBar : MonoBehaviour
{
    // "": Blank, "//~~//": Header
    private enum InterestType
    {
        XR,
        AI,
        Web3,
        Industry,
        Purpose
    }

    [SerializeField] private InterestType type;
    [SerializeField] private GameObject ToggleButtonPrefab;
    [SerializeField] private List<GameObject> buttons = new List<GameObject>();
    [SerializeField] private string[] XRStrings =
    {
        "VR", "AR", "메타버스", "공간컴퓨팅", "하드웨어"
    };
    [SerializeField] private string[] AIStrings =
    {
        "AI", "생성형 AI", "대화형 AI", "LLM"
    };
    [SerializeField] private string[] web3Strings =
    {
        "Web3.0", "블록체인", "암호화폐", "탈중앙 생태계"
    };
    [SerializeField] private string[] industryStrings =
    {
        "게임", "소셜", "의료", "커머스", "모빌리티", "버추얼휴먼", "교육",
        "스포츠", "라이프스타일", "금융", "연구", "엔터테인먼트", "패션"
    };
    [SerializeField] private string[] purposeStrings =
    {
        "창업", "공모전", "스터디", "자기계발", "팀원 구인"
    };


    private string[] currentStrings;

    void Awake()
    {
        switch(type){
            case InterestType.XR:
                currentStrings = XRStrings;
                break;
            case InterestType.AI:
                currentStrings = AIStrings;
                break;
            case InterestType.Web3:
                currentStrings = web3Strings;
                break;
            case InterestType.Industry:
                currentStrings = industryStrings;
                break;
            case InterestType.Purpose:
                currentStrings = purposeStrings;
                break;
        }

        for(int i=0; i<currentStrings.Length; i++)
        {
            if(currentStrings[i] == "") // Blank
            {
                buttons.Add(Instantiate(new GameObject(), Vector3.zero, Quaternion.identity, this.gameObject.transform));
                buttons[i].AddComponent<RectTransform>();
            }
            else // Not blank
            {
                buttons.Add(Instantiate(ToggleButtonPrefab, Vector3.zero, Quaternion.identity, this.gameObject.transform));
                buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentStrings[i];
                buttons[i].GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool arg0)
                {
                    UIManager.instance.SetButtonString("interest", currentStrings[i], buttons[i].GetComponent<Toggle>().isOn); 
                });
            }
        }
    }
}
