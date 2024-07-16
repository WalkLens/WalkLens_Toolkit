using MixedReality.Toolkit.SpatialManipulation;
using MRTK.Tutorials.MultiUserCapabilities;
using RealityCollective.Extensions;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SearchingUIManager : MonoBehaviour
{
    [SerializeField] private DirectionalIndicator indicator;
    [SerializeField] private Text searchingPINNum;

    public void OnSearchButtonClicked()
    {
        indicator.SetActive(true);
        Transform userTransform = GameObject.Find(searchingPINNum.text).GetComponent<Transform>();
        indicator.DirectionalTarget = userTransform;
        if (indicator.DirectionalTarget == null)
        {
            Debug.LogError("No UserPIN" + searchingPINNum.text);
        }
    }
}
