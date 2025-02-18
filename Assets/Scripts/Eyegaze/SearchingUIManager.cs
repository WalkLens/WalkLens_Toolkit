using MixedReality.Toolkit.SpatialManipulation;
using MRTK.Tutorials.MultiUserCapabilities;
using RealityCollective.Extensions;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System;
using Unity.VisualScripting;
using MixedReality.Toolkit.UX;
using MixedReality.Toolkit;

public class SearchingUIManager : MonoBehaviour
{
    public PressableButton[] toggleGroup;
    public PressableButton[] togglePartGroup;
    void Start()
    {
        foreach (var toggle in toggleGroup)
        {
            toggle.OnClicked.AddListener(() => OnButtonClicked(toggle));
        }
        foreach (var togglePart in togglePartGroup)
        {
            togglePart.OnClicked.AddListener(() => OnPartButtonClicked(togglePart));
        }
    }

    void OnButtonClicked(PressableButton clickedButton)
    {
        foreach (var button in toggleGroup)
        {
            if (button != clickedButton)
            {
                var interactable = button.GetComponent<StatefulInteractable>();
                if (interactable != null && interactable.IsToggled)
                {
                    interactable.ForceSetToggled(false);
                }
            }

        }
    }

    void OnPartButtonClicked(PressableButton clickedPartButton)
    {
        foreach (var partButton in togglePartGroup)
        {
            if (partButton != clickedPartButton)
            {
                var interactable = partButton.GetComponent<StatefulInteractable>();
                if (interactable != null && interactable.IsToggled)
                {
                    interactable.ForceSetToggled(false);
                }
            }
        }
    }
}
