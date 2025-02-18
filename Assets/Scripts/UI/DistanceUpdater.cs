using System.Collections;
using System.Collections.Generic;
using RealityCollective.Extensions;
using TMPro;
using UnityEngine;

public class DistanceUpdater : MonoBehaviour
{
    public TextMeshProUGUI[] distanceText;
    public Transform matchedUser;

    void Update()
    {
        if (this.gameObject.GetComponent<MeshRenderer>().enabled)
        {
            foreach (var m in distanceText)
            {
                Transform myPos = GameObject.Find(EyegazeUIManager.main.myPinNum).GetComponent<Transform>();
                // 유니티 상의 거리 계산
                float distance = Vector3.Distance(matchedUser.position, myPos.transform.position);

                // 스케일 팩터를 통해 현실 거리로 변환
                float scaleFactor = 1.0f; // HoloLens에서는 1 유니티 단위가 1 미터
                float realityDistance = distance * scaleFactor;

                // 소수점 4자리까지 반올림
                float roundedDistance = Mathf.Round(realityDistance * 10000f) / 10000f;

                m.text = roundedDistance.ToString() + "m";
                m.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (var m in distanceText)
            {
                m.gameObject.SetActive(false);
            }
        }

    }
}
