using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroupBar : MonoBehaviour
{
    public GroupType groupType;
    private Toggle toggle;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnToggle);
    }

    public void OnToggle(bool isOn)
    {
        string value = groupType.ToString();

        if (isOn)
        {
            UIManager.instance.group.Add(value);
        }
        else
        {
            UIManager.instance.group.Remove(value);
        }
    }
}
public enum GroupType
{
    리서치,
    데브,
    디자인,
    뉴미디어,
    브랜딩,
    운영진,
    게스트
}