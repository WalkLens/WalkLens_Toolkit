using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JobsBar : MonoBehaviour
{
    public jobType jobType;
    private Toggle toggle;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnToggle);
    }

    public void OnToggle(bool isOn)
    {
        if (isOn)
        {
            UIManager.instance.job = jobType.ToString();
        }
        else
        {
            UIManager.instance.job = "";
        }
    }
}

public enum jobType
{
    학생,
    직장인,
    해당사항없음
}