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
    private enum SkillType
    {
        Research,
        Design,
        Develop
    }

    [SerializeField] private SkillType type;
    [SerializeField] private GameObject toggleButtonPrefab;
    [SerializeField] private List<GameObject> buttons = new List<GameObject>();
    [SerializeField] private GameObject targetBar;
    [SerializeField] private GameObject buttonRemovablePrefab;

    [SerializeField] private string[] researchSkillStrings =
    {
        "게임 기획", "서비스 기획", "콘텐츠 기획", "데이터 분석", "마케팅", "UX/UI",
        "사업 기획", "프로젝트 매니징", "영업 기획", "PM", "백엔드 기획", "MD"
    };
    [SerializeField] private string[] designSkillStrings =
    {
        "3D 디자인", "건축, 인테리어 디자인", "일러스트레이션", "그래픽 디자인",
        "패션 디자인", "브랜드 디자인", "영상 디자인", "UX/UI 디자인", "순수 예술",
        "캐릭터 디자인", "레벨 디자인", "에셋 디자인", "리깅&애니메이팅"
    };
    [SerializeField] private string[] developSkillStrings =
    {
        "게임 기획", "서비스 기획", "콘텐츠 기획", "데이터 분석", "마케팅", "UX/UI",
        "사업 기획", "프로젝트 매니징", "영업 기획", "PM", "백엔드 기획", "MD"
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
            buttons.Add(Instantiate(toggleButtonPrefab, Vector3.zero, Quaternion.identity, this.gameObject.transform));
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentStrings[i];
            buttons[i].GetComponent<AddItem>().targetBar = this.targetBar;
            buttons[i].GetComponent<AddItem>().buttonRemovablePrefab = this.buttonRemovablePrefab;
            buttons[i].GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool arg0)
            {
                //UIManager.instance.SetButtonString("skill", currentStrings[i], buttons[i].GetComponent<Toggle>().isOn); 
            });
        }

        //GetComponent<RectTransform>().sizeDelta = new Vector2(0, 70*(currentStrings.Length/4 + 1)); // TODO Hardcoded
    }
}
