using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckGuest : MonoBehaviour
{
    [SerializeField] private Toggle guestButton; // TODO Change to actual variable?
    [SerializeField] private GameObject XREALPage;
    [SerializeField] private GameObject guestPage;
    
    public void OnClick()
    {
        if(guestButton.isOn) // GOTO guestPage
        {
            guestPage.SetActive(true);
        }
        else // GOTO XREALPage
        {
            XREALPage.SetActive(true);
        }
    }
}
