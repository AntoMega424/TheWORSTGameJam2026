using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    private enum PlayerState
    {
        TALKING,
        PLAYABLE,
        DEAD
    }

    [SerializeField] private GameObject m_playerTextUI = null;
    [SerializeField] [Min(0f)] float m_health = 100f;
    [SerializeField] [Min(0f)] float m_secondsUntilDeathWhenUnderTheFloor = 10f;

    private PlayerState m_state = PlayerState.TALKING;
    private TextMeshProUGUI m_uiText = null;
    private float m_timeUntilRespawn = 0f;

    private void Awake()
    {
        m_uiText = m_playerTextUI.GetComponent<TextMeshProUGUI>();
        m_timeUntilRespawn = m_secondsUntilDeathWhenUnderTheFloor;
    }

    private void Update()
    {
        switch (m_state)
        {
            case PlayerState.TALKING:
                {

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
