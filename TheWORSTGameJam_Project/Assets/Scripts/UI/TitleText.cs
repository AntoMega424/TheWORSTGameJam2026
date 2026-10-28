using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TitleText : UIThing
{
    [SerializeField] [Range(0f, 10f)] private float m_secondsToQuitGame = 5f;

    private TextMeshProUGUI m_tmpText = null;
    private float m_quitTimer = 0f;
    private bool m_canChangeColourOnHover = true;

    private void Awake()
    {
        m_tmpText = GetComponent<TextMeshProUGUI>();

        FetchSound("Title_Hover");
        FetchSound("Title_Interact");
    }

    private void Update()
    {
        if (m_canChangeColourOnHover)
        {
            return;
        }

        m_quitTimer -= Time.deltaTime;

        if (m_secondsToQuitGame >= 2f &&
            m_quitTimer <= m_secondsToQuitGame)
        {
            m_tmpText.text = m_quitTimer.ToString("F0");
        }

        if (m_quitTimer <= 0f)
        {
            Application.Quit();
        }
    }

    public override void OnPointerEnter(PointerEventData _eventData)
    {
        base.OnPointerEnter(_eventData);

        if (!m_canChangeColourOnHover)
        {
            return;
        }

        m_tmpText.color = ColourUtils.RandomRGB01();
    }

    public override void OnPointerClick(PointerEventData _eventData)
    {
        base.OnPointerClick(_eventData);

        // Double-click to exit
        if (_eventData.clickCount < 2)
        {
            return;
        }

        m_tmpText.color = ColourUtils.HexToColor("F05353");
        m_tmpText.text = "Bye bye! 🖐️";
        m_tmpText.rectTransform.localScale = Vector3.one;

        m_quitTimer = m_secondsToQuitGame + 1f;
        m_canChangeColourOnHover = false;
    }
}
