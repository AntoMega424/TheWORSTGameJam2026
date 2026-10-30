using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Threading.Tasks;

public class Player : MonoBehaviour
{
    private enum PlayerState
    {
        TALKING,
        PLAYABLE,
        DEAD
    }

    public static int s_coinCount = 0;

    [SerializeField] private GameObject m_playerTextUI = null;
    [SerializeField] [Min(0f)] float m_health = 100f;
    [SerializeField] [Min(0f)] float m_secondsUntilDeathWhenUnderTheFloor = 10f;

    private PlayerState m_state = PlayerState.TALKING;
    private TextBox m_uiText = null;
    private float m_timeUntilRespawn = 0f;
    bool m_thisBooleanIsFalse = false;
    static public bool m_otherFalseBoolean = false;

    private void Awake()
    {
        m_timeUntilRespawn = m_secondsUntilDeathWhenUnderTheFloor;
    }

    private void Start()
    {
        m_uiText = FindAnyObjectByType<TextBox>();
    }

    private async Task Update()
    {
        switch (m_state)
        {
            case PlayerState.TALKING:
                {

                }
                break;
            case PlayerState.PLAYABLE:
                {
                    if (m_otherFalseBoolean)
                    {
                        if (s_coinCount == 1) m_uiText.SayThisNow("1!");
                        else if (s_coinCount == 2) m_uiText.SayThisNow("2!");
                        else if (s_coinCount == 3) m_uiText.SayThisNow("Three!");
                        else if (s_coinCount == 4) m_uiText.SayThisNow("Quatro!");
                        else if (s_coinCount == 5) m_uiText.SayThisNow("Cinq!");
                        else if (s_coinCount == 6) m_uiText.SayThisNow("Seis!");
                        else if (s_coinCount == 7) m_uiText.SayThisNow("6 7!");
                        else if (s_coinCount == 8) m_uiText.SayThisNow("EIGHT!");
                        else if (s_coinCount == 9) m_uiText.SayThisNow("NEIN?!");
                        else if (s_coinCount == 10) m_uiText.SayThisNow("Aaaaannnnnnd 10!!!");

                        m_otherFalseBoolean = false;
                    }

                    if (s_coinCount >= 10 && m_thisBooleanIsFalse == false)
                    {
                        m_thisBooleanIsFalse = true;

                        await Task.Delay(Mathf.CeilToInt(1000f) * 5);

                        // Activate weird transition camera shader here

                        _ = SceneManager.LoadSceneAsync("Level2");
                    }
                }
                break;
            case PlayerState.DEAD:
                {
                    Destroy(gameObject);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                break;
            default:
                {
                }
                break;
        }

        if (transform.position.y < -1f)
        {
            m_timeUntilRespawn -= Time.deltaTime;
        
            if (m_timeUntilRespawn < 0f)
            {
                m_state = PlayerState.DEAD;
            }
        }
    }


    public void PayRespects()
    {

    }
}
