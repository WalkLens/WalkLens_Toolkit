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
        SpatialComputing,
        AI,
        Web3,
        Game,
        Category,
        Purpose
    }

    [SerializeField] private InterestType type;
    [SerializeField] private GameObject ToggleButtonPrefab;
    [SerializeField] private List<GameObject> buttons = new List<GameObject>();
    [SerializeField] private string[] spatialComputingStrings =
    {
        "VR", "AR", "메타버스", "공간컴퓨팅",
        "HMD"
    };
    [SerializeField] private string[] AIStrings =
    {
        "AI", "대화형 AI", "생성형 AI", "LLM"
    };
    [SerializeField] private string[] web3Strings =
    {
        "Web3.0", "블록체인", "암호화폐", "탈중앙 생태계"
    };
    [SerializeField] private string[] gameStrings =
    {
        "게임", "게임제작", "게임설계"
    };
    [SerializeField] private string[] categoryStrings =
    {
        "커머스", "의료", "소셜", "버추얼 휴먼",
        "모빌리티", "라이프스타일", "패션", "헬스",
        "교육", "생산성", "쇼핑", "스포츠",
        "엔터테인먼트", "스포츠", "투자", "금융",
        "연구/개발"
    };
    [SerializeField] private string[] purposeStrings =
    {
        "창업", "공모전", "자기계발", "스터디",
        "팀원 구인"
    };


    private string[] currentStrings;

    void Awake()
    {
        switch(type){
            case InterestType.SpatialComputing:
                currentStrings = spatialComputingStrings;
                break;
            case InterestType.AI:
                currentStrings = AIStrings;
                break;
            case InterestType.Web3:
                currentStrings = web3Strings;
                break;
            case InterestType.Game:
                currentStrings = gameStrings;
                break;
            case InterestType.Category:
                currentStrings = categoryStrings;
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
