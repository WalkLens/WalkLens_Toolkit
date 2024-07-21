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
    [SerializeField] List<GameObject> buttons = new List<GameObject>();
    String[] projects = {"비전메타몽", "판타리움", "Band Lab", "BareHands", "Computer Graphics", "DevArchive", "Jellysis", "LUDENS", "Perrito", "ReasonMaker", "SeeTheMusic", "Spatial Computing Lab", "UXR Lab", "WalkLens", "운영 지원팀"};
    int[] size = {32, 32, 32, 32, 22, 32, 32, 32, 32, 26, 26, 24, 32, 32, 32};

    void Awake()
    {
        foreach(Transform child in transform)
        {
            if(child.gameObject.GetComponent<Toggle>() != null)
            {
                buttons.Add(child.gameObject);
            }
        }
        for(int i=0; i<projects.Length; i++)
        {
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = projects[i];
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().fontSize = size[i];
        }
    }
}
