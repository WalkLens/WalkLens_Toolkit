using System.Collections;
using System.Collections.Generic;
using MixedReality.Toolkit.SpatialManipulation;
using RealityCollective.Extensions;
using TMPro;
using UnityEngine;

public class MatchedProfileDialog : MonoBehaviour
{
    [Header("Left Info")]
    public TextMeshProUGUI name;
    public TextMeshProUGUI generationAndGroup;
    public TextMeshProUGUI introduction;
    public TextMeshProUGUI univAndMajor;
    public TextMeshProUGUI jobAndPosition;
    public Transform team_Horizontal;
    [Header("Right Info")]
    public GameObject rightProfile;
    [Header("Matched Info")]
    public string pinNum;
    public DirectionalIndicator indicator;
    public ARUIManager aRUIManager;
    public Material blueMaterial;

    void Awake()
    {
        aRUIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<ARUIManager>();
        indicator = GameObject.FindGameObjectWithTag("Indicator").GetComponent<DirectionalIndicator>();
    }

    public void OnClicked()
    {
        Debug.Log("MatchProfile Clicked!!!");
        Transform userTransform = GameObject.Find(pinNum).GetComponent<Transform>();
        aRUIManager.searchUI.SetActive(false);
        aRUIManager.searchUI.transform.GetChild(0).gameObject.SetActive(true);
        aRUIManager.searchUI.transform.GetChild(1).gameObject.SetActive(false);
        // indicator.SetActive(true);
        indicator.DirectionalTarget = userTransform;
        indicator.gameObject.GetComponent<MeshRenderer>().SetActive(true);
        userTransform.gameObject.GetComponentInChildren<MeshRenderer>().material = blueMaterial;
        indicator.gameObject.GetComponent<DistanceUpdater>().matchedUser = userTransform;
    }

}
