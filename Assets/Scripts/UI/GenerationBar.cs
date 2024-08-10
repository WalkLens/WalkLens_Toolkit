using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerationBar : MonoBehaviour
{
    public int generationNum;
    private Toggle toggle;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnToggle);
    }

    public void OnToggle(bool isOn)
    {
        string value = generationNum.ToString() + "기";

        if (isOn)
        {
            UIManager.instance.generation = value;
        }
        else
        {
            UIManager.instance.generation = "";
        }
    }
}

