using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillsBar : MonoBehaviour
{
    // "": Blank, "//~~//": Header
    private enum SkillType
    {
        Research,
        Design,
        Develop
    }

    [SerializeField] private SkillType type;
    [SerializeField] private GameObject ToggleButtonPrefab;
    [SerializeField] private List<GameObject> buttons = new List<GameObject>();

    [SerializeField] private string[] researchSkillStrings =
    {
        "게임 기획", "광고 기획", "기술 기획", "데이터 분석/설계",
        "백엔드/어드민 기획", "사업 기획", "상품 기획", "생산 기획",
        "서비스 기획", "영업 기획", "운영 기획", "웹/앱 기획",
        "전략 기획", "콘텐츠 기획", "MD", "PM"
    };
    [SerializeField] private string[] designSkillStrings =
    {
        "3D 모델링", "건축 디자인", "공간 디자인", "그래픽 디자인",
        "리깅&애니메이팅", "브랜드 디자인", "실내 디자인", "원화",
        "웹 디자인", "일러스트레이터", "제품 디자인", "조형 예술",
        "컨셉 디자인", "패션 디자인", "TA", "UX/UI 디자인",
        // "//게임 디자이너//", // Header
        "", "", "", "",
        "캐릭터 디자인", "월드 디자인", "에셋 디자인", "게임 디자인"
    };
    [SerializeField] private string[] developSkillStrings =
    {
        "프론트엔드 개발", "백엔드 개발", "풀스택 개발", "미들티어 개발",
        "웹개발", "앱개발", "데스크톱 개발", "모바일 개발",
        "AR개발", "VR개발", "임베디드 개발", "정보/통신",
        "게임 클라이언트", "그래픽 개발", "게임 엔진", "네트워크 개발",
        "서버 개발", "모션 캡쳐", "데브옵스", "개발 설계",
        "","","","", // Blank
        "Web3.0","블록체인/크립토","","",
        "","","","",
        "머신러닝","딥러닝","대화형 AI","생성형 AI",
        "빅데이터 개발","데이터 사이언스","데이터 엔지니어","",
        "","","","",
        "CRM","QA","R&D","보안"
    };

    private string[] currentStrings;

    void Awake()
    {
        switch(type){
            case SkillType.Research:
                currentStrings = researchSkillStrings;
                break;
            case SkillType.Design:
                currentStrings = designSkillStrings;
                break;
            case SkillType.Develop:
                currentStrings = developSkillStrings;
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
                    UIManager.instance.SetButtonString("skill", currentStrings[i], buttons[i].GetComponent<Toggle>().isOn); 
                });
            }
        }

        GetComponent<RectTransform>().sizeDelta = new Vector2(0, 70*(currentStrings.Length/4 + 1)); // TODO Hardcoded
    }
}
