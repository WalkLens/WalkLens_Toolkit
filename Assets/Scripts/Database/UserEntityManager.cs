using System.Collections;
using System.Collections.Generic;
using MRTK.Tutorials.AzureCloudServices.Scripts.Managers;
using UnityEngine;

public class UserEntityManager : MonoBehaviour
{
    
    public void SetPinNumberBasedDB(ref List<DataManagerCtrl.UserEntity> allUsersList)
    {
        // DB의 partition key를 기준으로 새로 등록하는 pin을 자동으로 할당합니다.
        if (allUsersList == null) return;

        int maxPinNumber = 0;
        int value;
        foreach (var user in allUsersList)
        {
            value = int.Parse(user.PartitionKey);
            if (maxPinNumber < value)
                maxPinNumber = value;
        }

        UIManager.instance.pinNumber.text = (maxPinNumber + 1).ToString("D4");
    }
}
