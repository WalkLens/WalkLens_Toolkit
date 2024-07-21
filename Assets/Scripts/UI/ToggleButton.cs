using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ToggleButton : MonoBehaviour
{
    private Image background;
    private Toggle toggle;
    void Awake()
    {
        background = GetComponent<Image>();
        toggle = GetComponent<Toggle>();
    }
    public void OnToggle()
    {
        if(toggle.isOn)
        {
            background.color = new Color32(200, 200, 200, 255);
        }
        else
        {
            background.color = new Color32(255, 255, 255, 255);
        }
    }
}
