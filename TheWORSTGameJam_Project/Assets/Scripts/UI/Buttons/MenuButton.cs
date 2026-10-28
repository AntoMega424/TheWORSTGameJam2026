using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class MenuButton : UIThing
{
    private enum ButtonType
    {
        PLAY,
        QUIT,

        RESPECT
    }

    [SerializeField] private ButtonType m_type = ButtonType.PLAY;
    [SerializeField] [Min(0f)]
     private float m_scaleChangeSpeed = 2f;

    private RectTransform m_rectTransform = null;
    private float m_scaleChangeTimer = 0f;
    private float m_scaleChangeOffset = 1f;

    private void Awake()
    {
        m_rectTransform = GetComponent<RectTransform>();

        GetComponent<Button>().onClick.AddListener(OnClick);

        float offsetValue = 0.25f;
        m_scaleChangeOffset += Random.Range(-offsetValue, offsetValue);

        string typeAsStr =
            m_type
            .ToString()
            .ToLower()
            .FirstCharacterToUpper();
        string pathStart = $"Buttons/{typeAsStr}Button";

        FetchSound(pathStart + "_Hover");
        FetchSound(pathStart + "_Interact");
    }

    private void Update()
    {
        m_scaleChangeTimer += Time.deltaTime * m_scaleChangeSpeed;

        float newScaleValue = m_scaleChangeOffset * m_scaleChangeTimer;
        float newScaleX = 1f + 0.5f * Mathf.Sin(newScaleValue);
        float newScaleY = 1f + 0.5f * Mathf.Cos(newScaleValue);
        Vector3 localScale = m_rectTransform.localScale;
        localScale.x = newScaleX;
        localScale.y = newScaleY;
        m_rectTransform.localScale = localScale;

        if (m_scaleChangeTimer > 1000000f)
        {
            m_scaleChangeTimer = 0f;
        }
    }


    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    private void QuitGame()
    {
        Application.Quit();
    }
    private void PayRespects()
    {
        Player player = FindAnyObjectByType<Player>();
        if (player == null)
        {
            LogUtils.LogWarning("Wtf");
            return;
        }

        player.PayRespects();
        
        Granny baba = FindAnyObjectByType<Granny>();
        if (baba == null)
        {
            LogUtils.LogWarning("Where is granny ?");
            return;
        }

        baba.AppearAsGhost();
        Destroy(gameObject);
    }

    private void OnClick()
    {
        switch (m_type)
        {
            case ButtonType.PLAY: StartGame(); break;
            case ButtonType.QUIT: QuitGame(); break;
            case ButtonType.RESPECT: PayRespects(); break;
            default: break;
        }
    }
}
