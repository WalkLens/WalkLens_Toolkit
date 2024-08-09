using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProjectsBar : MonoBehaviour
{
    [SerializeField] private GameObject ToggleListPrefab;
    [SerializeField] private List<GameObject> buttons = new List<GameObject>();
    [SerializeField] private string[] projects = {"비전메타몽", "판타리움", "Band Lab", "BareHands", "Computer Graphics", "DevArchive", "Jellysis", "LUDENS", "Perrito", "ReasonMaker", "SeeTheMusic", "Spatial Computing Lab", "UXR Lab", "WalkLens", "운영 지원팀"};

    void Awake()
    {
        for(int i=0; i<projects.Length; i++)
        {
            buttons.Add(Instantiate(ToggleListPrefab, Vector3.zero, Quaternion.identity, this.gameObject.transform));
            buttons[i].transform.SetSiblingIndex(i);
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = projects[i];
        }
    }
}
