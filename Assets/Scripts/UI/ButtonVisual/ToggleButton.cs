using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ToggleButton : MonoBehaviour
{
    private Image background;
    private Toggle toggle;
    private TMP_Text TMP;
    void Awake()
    {
        background = GetComponent<Image>();
        toggle = GetComponent<Toggle>();
        TMP = GetComponentInChildren<TMP_Text>();
    }
    public void OnToggle()
    {
        if(toggle.isOn)
        {
            background.color = new Color32(134, 134, 134, 255);
            TMP.color = new Color32(255, 255, 255, 255);
        }
        else
        {
            background.color = new Color32(255, 255, 255, 255);
            TMP.color = new Color32(134, 134, 134, 255);
        }
    }
}
