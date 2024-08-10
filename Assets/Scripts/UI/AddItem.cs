using System.Collections;
using System.Collections.Generic;
using RealityCollective.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class AddItem : MonoBehaviour
{
    private GameObject targetItem;
    public GameObject targetBar;
    public GameObject buttonRemovablePrefab;
    private Toggle toggle;
    void Awake()
    {
        toggle = GetComponent<Toggle>();
    }

    public void OnToggle()
    {
        if(toggle.isOn)
        {
            targetItem = Instantiate(buttonRemovablePrefab, Vector3.zero, Quaternion.identity, targetBar.transform);
            targetItem.GetComponent<ButtonRemovable>().targetToggle = toggle;
            Debug.Log(this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text);
            targetItem.GetComponentInChildren<TextMeshProUGUI>().text = this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text;
        }
        else
        {
            if(targetItem != null)
            {
                targetItem.Destroy();
            }
        }
    }
}
