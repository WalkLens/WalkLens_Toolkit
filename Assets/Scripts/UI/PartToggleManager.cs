using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartToggleManager : MonoBehaviour // TODO 아무것도 안 누르고 다음 버튼을 누르면??
{
    [SerializeField] private Toggle guestToggle;
    [SerializeField] private Toggle[] memberToggles;

    void Awake()
    {
        guestToggle.onValueChanged.AddListener(delegate{OnGuestToggled();});
        foreach(var toggle in memberToggles)
        {
            toggle.onValueChanged.AddListener(delegate{OnMemberToggled(toggle);});
        }
    }

    void OnGuestToggled()
    {
        if(guestToggle.isOn)
        {
            foreach(var toggle in memberToggles)
            {
                toggle.isOn = false;
            }
        }
    }

    void OnMemberToggled(Toggle toggle)
    {
        if(toggle.isOn && guestToggle.isOn)
        {
            guestToggle.isOn = false;
        }
    }
}
