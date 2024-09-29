using System.Collections;
using System.Collections.Generic;
using MRTK.Tutorials.AzureCloudServices.Scripts.Managers;
using UnityEngine;
using System;
using Random = System.Random;

public class UserEntityManager : MonoBehaviour
{
    public void SetPinNumberBasedDB(ref List<DataManagerCtrl.UserEntity> allUsersList)
    {
        // DB의 partition key를 기준으로 새로 등록하는 pin을 자동으로 할당합니다.
        if (allUsersList == null) return;

        HashSet<int> existingPins = new HashSet<int>();
        foreach (var user in allUsersList)
        {
            if (int.TryParse(user.PartitionKey, out int existingPin))
            {
                existingPins.Add(existingPin);
            }
        }

        int newPin = GenerateUniquePin(existingPins);
        UIManager.instance.pinNumber.text = newPin.ToString("D4");
    }

    private int GenerateUniquePin(HashSet<int> existingPins)
    {
        Random random = new Random();
        int potentialPin;
        do
        {
            potentialPin = random.Next(0, 10000); // Generates a number from 0 to 9999
        }
        while (existingPins.Contains(potentialPin));

        return potentialPin;
    }

    public static int GetIntValue(string value)
    {
        // 문자열의 앞뒤에 있는 작은 따옴표(')를 제거
        string cleanedInput = value.Trim('\'');

        // 문자열을 정수로 변환, 실패하면 기본값 0 반환
        if (int.TryParse(cleanedInput, out int result))
        {
            return result;  // 변환 성공 시 정수 반환
        }
        else
        {
            return -1;  // 변환 실패 시 기본값 -1 반환
        }
    }
}
