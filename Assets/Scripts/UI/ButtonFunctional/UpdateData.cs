using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum DataType
{
    Group,
    Generation,
    Project
}
public class UpdateData : MonoBehaviour
{
    public DataType dataType;
    public Toggle toggle;
    public TMP_Text TMP;

    void Awake()
    {
        toggle = GetComponent<Toggle>();
        TMP = GetComponentInChildren<TMP_Text>();
    }
    
    public void OnToggle()
    {
        if(toggle.isOn)
        {
            switch(dataType)
            {
                case DataType.Group:
                    UIManager.instance.group.Add(TMP.text);
                    break;
                case DataType.Generation:
                    UIManager.instance.generation = TMP.text;
                    break;
                case DataType.Project:
                    UIManager.instance.project.Add(TMP.text);
                    break;
            }
        }
        else
        {
            switch(dataType)
            {
                case DataType.Group:
                    UIManager.instance.group.Remove(TMP.text);
                    break;
                case DataType.Generation:
                    // Do nothing
                    break;
                case DataType.Project:
                    UIManager.instance.project.Remove(TMP.text);
                    break;
            }
        }
    }
}
