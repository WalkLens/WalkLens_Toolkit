using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CheckGuest : MonoBehaviour
{
    [SerializeField] private Toggle Member;
    // [SerializeField] private Toggle Guest;
    [SerializeField] private GameObject MemberPage;
    [SerializeField] private GameObject GuestPage;
    public void OnClick()
    {
        if(Member.isOn)
        {
            MemberPage.SetActive(true);
        }
        else
        {
            GuestPage.SetActive(true);
        }
    }
}
