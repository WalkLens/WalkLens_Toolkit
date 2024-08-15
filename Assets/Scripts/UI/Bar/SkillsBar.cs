using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillsBar : MonoBehaviour
{
    private enum SkillType
    {
        Research,
        Design,
        Develop
    }
    private enum DesignType
    {
        None,
        General,
        Game
    }
    private enum DevelopType
    {
        None,
        Ability,
        Part,
        Web,
        Data,
        Backend
    }

    [SerializeField] private SkillType type;
    [SerializeField] private DesignType designType;
    [SerializeField] private DevelopType developType;
    [SerializeField] private GameObject toggleButtonPrefab;
    [SerializeField] private List<GameObject> buttons = new List<GameObject>();
    [SerializeField] private GameObject targetBar;
    [SerializeField] private GameObject buttonRemovablePrefab;

    [SerializeField] private string[] researchAllStrings =
    {
        "게임 기획", "서비스 기획", "콘텐츠 기획", "데이터 분석", "마케팅", "UX/UI",
        "사업 기획", "프로젝트 매니징", "영업 기획", "PM", "백엔드 기획", "MD"
    };

    [SerializeField] private string[] designAllStrings = 
    {
        "3D 디자인", "건축, 인테리어 디자인", "일러스트레이션", "그래픽 디자인",
        "패션 디자인", "브랜드 디자인", "영상 디자인", "UX/UI 디자인", "순수 예술",

        "캐릭터 디자인", "레벨 디자인", "에셋 디자인", "리깅&애니메이팅"
    };
    [SerializeField] private string[] designGeneralStrings =
    {
        "3D 디자인", "건축, 인테리어 디자인", "일러스트레이션", "그래픽 디자인",
        "패션 디자인", "브랜드 디자인", "영상 디자인", "UX/UI 디자인", "순수 예술"
    };
    [SerializeField] private string[] designGameStrings = 
    {
        "캐릭터 디자인", "레벨 디자인", "에셋 디자인", "리깅&애니메이팅"
    };

    [SerializeField] private string[] developAllStrings = 
    {
        "프론트엔드 개발", "백엔드 개발", "풀스택 개발", "미들티어 개발",
        "웹 개발", "앱 개발", "데스크톱 개발", "모바일 개발",
        "AR 개발", "VR 개발", "임베디드 개발", "정보/통신",

        "게임 클라이언트", "그래픽 개발", "게임 엔진", "네트워크 개발",
        "서버 개발", "모션 캡쳐", "데브 옵스", "개발 설계",

        "Web 3.0", "블록체인", "암호 화폐",

        "머신러닝", "딥러닝", "대화형 AI", "생성형 AI",
        "빅데이터 개발", "데이터 사이언스", "데이터 엔지니어",

        "CRM", "QA", "R&D", "보안"
    };
    [SerializeField] private string[] developAbilityStrings =
    {
        "프론트엔드 개발", "백엔드 개발", "풀스택 개발", "미들티어 개발",
        "웹 개발", "앱 개발", "데스크톱 개발", "모바일 개발",
        "AR 개발", "VR 개발", "임베디드 개발", "정보/통신"
    };
    [SerializeField] private string[] developPartStrings = 
    {
        "게임 클라이언트", "그래픽 개발", "게임 엔진", "네트워크 개발",
        "서버 개발", "모션 캡쳐", "데브 옵스", "개발 설계"
    };
    [SerializeField] private string[] developWebStrings = 
    {
        "Web 3.0", "블록체인", "암호 화폐"
    };
    [SerializeField] private string[] developDataStrings = 
    {
        "머신러닝", "딥러닝", "대화형 AI", "생성형 AI",
        "빅데이터 개발", "데이터 사이언스", "데이터 엔지니어"
    };
    [SerializeField] private string[] developBackendStrings = 
    {
        "CRM", "QA", "R&D", "보안"
    };

    private string[] currentStrings;

    void Awake()
    {
        switch(type){
            case SkillType.Research:
                currentStrings = researchAllStrings;
                break;
            case SkillType.Design:
                switch(designType){
                    case DesignType.None:
                        currentStrings = designAllStrings;
                        break;
                    case DesignType.General:
                        currentStrings = designGeneralStrings;
                        break;
                    case DesignType.Game:
                        currentStrings = designGameStrings;
                        break;
                }
                break;
            case SkillType.Develop:
                switch(developType){
                    case DevelopType.None:
                        currentStrings = developAllStrings;
                        break;
                    case DevelopType.Ability:
                        currentStrings = developAbilityStrings;
                        break;
                    case DevelopType.Part:
                        currentStrings = developPartStrings;
                        break;
                    case DevelopType.Web:
                        currentStrings = developWebStrings;
                        break;
                    case DevelopType.Data:
                        currentStrings = developDataStrings;
                        break;
                    case DevelopType.Backend:
                        currentStrings = developBackendStrings;
                        break;
                }
                break;
        }

        for(int i=0; i<currentStrings.Length; i++)
        {
            buttons.Add(Instantiate(toggleButtonPrefab, Vector3.zero, Quaternion.identity, this.gameObject.transform));
            string value = currentStrings[i];
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = value;
            buttons[i].GetComponent<AddItem>().targetBar = this.targetBar;
            buttons[i].GetComponent<AddItem>().buttonRemovablePrefab = this.buttonRemovablePrefab;
            buttons[i].GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool arg0)
            {
                if (arg0)
                {
                    UIManager.instance.skill.Add(value);
                }
                else
                {
                    UIManager.instance.skill.Remove(value);
                }
                //UIManager.instance.SetButtonString("skill", currentStrings[i], buttons[i].GetComponent<Toggle>().isOn); 
            });
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)buttons[i].transform);
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)this.gameObject.transform);
        //GetComponent<RectTransform>().sizeDelta = new Vector2(0, 70*(currentStrings.Length/4 + 1)); // TODO Hardcoded
    }
}
