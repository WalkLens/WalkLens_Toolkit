using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAttributeSetter : MonoBehaviour
{
   public Toggle[] researchSkillToggle;
   public Toggle[] designSkillToggle;
   public Toggle[] developSkillToggle;

   public string[] researchSkillStrings =
   {
      "게임 기획", "광고 기획", "기술 기획", "데이터 분석/설계", "백엔드/어드민 기획",
      "사업 기획", "상품 기획", "생산 기획", "서비스 기획", "영업 기획", "운영 기획", "웹/앱 기획", "전략 기획", "콘텐츠 기획", "MD", "PM"
   };
   public string[] designSkillStrings =
   {
      "3D 모델링", "건축 디자인", "공간 디자인", "그래픽 디자인", "리깅&애니메이팅",
      "브랜드 디자인", "실내 디자인", "원화", "웹 디자인", "일러스트레이터", "제품 디자인", "조형 예술", "컨셉 디자인", "패션 디자인", "TA", "UX/UI 디자인",
      "캐릭터 디자인", "월드 디자인", "에셋 디자인", "게임 디자인"
   };
   public string[] developSkillStrings =
   {
      "프론트엔드 개발", "백엔드 개발", "풀스택 개발", "미들티어 개발", "웹개발",
      "앱개발", "데스크톱 개발", "모바일 개발", "AR개발", "VR개발", "임베디드 개발", "정보/통신", "게임 클라이언트", "그래픽 개발", "게임 엔진", "네트워크 개발"
      ,"서버 개발", "모션 캡쳐", "데브옵스", "개발 설계", "Web3.0","블록체인/크립토","머신러닝","딥러닝","대화형 AI","생성형 AI","빅데이터 개발","데이터 사이언스"
      ,"데이터 엔지니어","CRM","QA","R&D","보안"
   };

   [ContextMenu("Set Toggle Text")]
   public void SetToggleText()
   {
      for (int i = 0; i < researchSkillStrings.Length; i++)
      {
         researchSkillToggle[i].GetComponentInChildren<TextMeshProUGUI>().text = researchSkillStrings[i];
         researchSkillToggle[i].onValueChanged.AddListener(delegate(bool arg0)
         {
            UIManager.instance.SetButtonString("skill", researchSkillStrings[i], researchSkillToggle[i].isOn); 
         });
      }
      for (int i = 0; i < designSkillStrings.Length; i++)
      {
         designSkillToggle[i].GetComponentInChildren<TextMeshProUGUI>().text = designSkillStrings[i];
         designSkillToggle[i].onValueChanged.AddListener(delegate(bool arg0)
         {
            UIManager.instance.SetButtonString("skill", designSkillStrings[i], designSkillToggle[i].isOn); 
         });
      }
      for (int i = 0; i < developSkillStrings.Length; i++)
      {
         developSkillToggle[i].GetComponentInChildren<TextMeshProUGUI>().text = developSkillStrings[i];
         developSkillToggle[i].onValueChanged.AddListener(delegate(bool arg0)
         {
            UIManager.instance.SetButtonString("skill", developSkillStrings[i], developSkillToggle[i].isOn); 
         });
      }
   }
   
}
