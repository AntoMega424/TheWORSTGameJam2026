using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TitleText : MonoBehaviour, IPointerEnterHandler
{
    private TextMeshProUGUI m_tmpText = null;

    private void Awake()
    {
        m_tmpText = GetComponent<TextMeshProUGUI>();
    }


    public void OnPointerEnter(PointerEventData _eventData)
    {
        m_tmpText.color = ColourUtils.RandomRGB01();
    }
}
