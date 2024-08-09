using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class DynamicMinWidth : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    private string previousText;
    public RectTransform textComponent;
    public LayoutElement layoutElement;

    void Awake()
    {
        if (textMeshPro == null)
        {
            Debug.LogError("TextMeshProUGUI component not found on this GameObject.");
            return;
        }
    }

    void Update() 
    {
        if(textMeshPro.text != previousText)
        {
            previousText = textMeshPro.text;
            UpdateMinWidth(textComponent);
        }
    }

    private void UpdateMinWidth(RectTransform textRect)
    {
        Vector3[] corners = new Vector3[4];
        textRect.GetWorldCorners(corners);
        double preferredWidth = Vector3.Distance(corners[0], corners[3]) * 200 * 6.024 * 1.5;

        // 부모 오브젝트의 Layout Element의 min width 설정
        layoutElement.minWidth = (float)preferredWidth;
    }
}