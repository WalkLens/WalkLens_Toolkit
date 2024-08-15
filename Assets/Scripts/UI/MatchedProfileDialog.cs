using System.Collections;
using System.Collections.Generic;
using MixedReality.Toolkit.SpatialManipulation;
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
    public DirectionalIndicator directionalIndicator;
    public ARUIManager aRUIManager;

    void Awake()
    {
        aRUIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<ARUIManager>();
    }

    public void OnClicked()
    {
        Debug.Log("MatchProfile Clicked!!!");
        DirectionalIndicator indicatorClone = Instantiate(directionalIndicator);
        Transform userTransform = GameObject.Find(pinNum).GetComponent<Transform>();
        indicatorClone.DirectionalTarget = userTransform;
        aRUIManager.matchUI.SetActive(false);
    }

}
