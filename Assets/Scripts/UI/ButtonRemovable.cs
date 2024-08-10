using System.Collections;
using System.Collections.Generic;
using RealityCollective.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ButtonRemovable : MonoBehaviour
{
    [HideInInspector] public Toggle targetToggle; // Editor
    void Awake()
    {

    }
    public void OnClick()
    {
        targetToggle.isOn = false;
        this.gameObject.Destroy();
    }
}
