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
        foreach (var userPIN in PhotonLobby.Lobby.userPINList)
        {
            if (userPIN == searchingPINNum.text)
            {
                
                indicator.SetActive(true);
                Transform userTransform = GameObject.Find(userPIN).GetComponent<Transform>();
                indicator.DirectionalTarget = userTransform;
            }
        }

        if (indicator.DirectionalTarget == null)
        {
            Debug.LogError("No UserPIN" + searchingPINNum.text);
        }
    }
}
