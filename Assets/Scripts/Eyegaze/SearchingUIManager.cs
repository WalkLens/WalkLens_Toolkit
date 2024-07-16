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

public class SearchingUIManager : MonoBehaviour
{
    [SerializeField] private DirectionalIndicator indicator;
    [SerializeField] private Text searchingPINNum;

    public void OnSearchButtonClicked()
    {
        try
        {
            Transform userTransform = GameObject.Find(searchingPINNum.text).GetComponent<Transform>();
            indicator.DirectionalTarget = userTransform;
        }
        catch (NullReferenceException ie)
        {
            Debug.LogError($"{ie.Message}");
            Debug.LogError("No UserPIN " + searchingPINNum.text);
        }
    }
}
