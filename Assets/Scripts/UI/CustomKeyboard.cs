using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CustomKeyboard : MonoBehaviour
{
    public TextMeshProUGUI inputField;

    public void OnKeyPress(string character)
    {
        inputField.text += character;
    }

    public void OnBackspace()
    {
        if (inputField.text.Length > 0)
        {
            inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
        }
    }

    public void OnEnter()
    {
        // Enter key functionality
    }
}
