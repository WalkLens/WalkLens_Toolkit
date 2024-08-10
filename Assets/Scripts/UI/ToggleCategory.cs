using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;


public class ToggleCategory : MonoBehaviour
{
    // [SerializeField] private GameObject activeObject; // Editor
    // [SerializeField] private GameObject inactiveObject; // Editor
    // private Toggle toggle;
    // void Awake()
    // {
    //     toggle = GetComponent<Toggle>();
    //     activeObject.SetActive(toggle.isOn);
    //     inactiveObject.SetActive(!toggle.isOn);
    // }
    // public void OnToggle()
    // {
    //     if(toggle.isOn)
    //     {
    //         activeObject.gameObject.SetActive(true);
    //         inactiveObject.gameObject.SetActive(false);
    //     }
    //     else
    //     {
    //         activeObject.gameObject.SetActive(false);
    //         inactiveObject.gameObject.SetActive(true);
    //     }
    // }
    [SerializeField] private TMP_Text TMP;
    [SerializeField] private TMP_FontAsset bold;
    [SerializeField] private TMP_FontAsset regular;
    [SerializeField] private Image underline;
    private Toggle toggle;
    void Awake()
    {
        toggle = GetComponent<Toggle>();

        if(toggle.isOn)
        {
            TMP.font = bold;
            underline.enabled = true;
        }
        else
        {
            TMP.font = regular;
            underline.enabled = false;
        }
    }

    public void OnToggle()
    {
        if(toggle.isOn)
        {
            TMP.font = bold;
            underline.enabled = true;
        }
        else
        {
            TMP.font = regular;
            underline.enabled = false;
        }
    }
}
