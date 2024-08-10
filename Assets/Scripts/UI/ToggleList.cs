using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ToggleList : MonoBehaviour
{
    [SerializeField] private Outline background; // Editor
    [SerializeField] private GameObject selectedCircle; // Editor
    [SerializeField] private TMP_Text TMP; // Editor
    private Toggle toggle;
    void Awake()
    {
        toggle = GetComponent<Toggle>();
    }
    public void OnToggle()
    {
        if(toggle.isOn)
        {
            UIManager.instance.project.Add(TMP.text);
            background.effectColor = new Color32(0, 0, 0, 255);
            selectedCircle.SetActive(true);
            TMP.color = new Color32(0, 0, 0, 255);
        }
        else
        {
            UIManager.instance.project.Remove(TMP.text);
            background.effectColor = new Color32(217, 217, 217, 255);
            selectedCircle.SetActive(false);
            TMP.color = new Color32(139, 139, 139, 255);
        }
    }
}
