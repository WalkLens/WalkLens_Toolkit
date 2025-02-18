using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateOB : MonoBehaviour
{
    public DataType dataType;
    public Toggle toggle;
    public TMP_InputField inputField;
    private string old_text;

    void Awake()
    {
        toggle = GetComponent<Toggle>();
        inputField = GetComponentInChildren<TMP_InputField>();
        old_text = inputField.text;
    }
    
    public void UpdateText()
    {
        toggle.isOn = true;
        UIManager.instance.project.Remove(old_text);
        UIManager.instance.project.Add(inputField.text);
        old_text = inputField.text;
    }

    public void OnToggle()
    {
        if(toggle.isOn)
        {
            UIManager.instance.project.Add(inputField.text);
        }
        else
        {
            UIManager.instance.project.Remove(inputField.text);
        }
    }
}
