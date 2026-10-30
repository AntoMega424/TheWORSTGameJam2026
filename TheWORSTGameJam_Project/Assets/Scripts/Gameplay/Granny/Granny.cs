using UnityEngine;

public class Granny : MonoBehaviour
{
    private enum GrannyState
    {
        TOMB,
        GHOST,
        ALIVE
    }

    [SerializeField] private GameObject m_coinsZonePrefab = null;
    [SerializeField] private GameObject m_ghost = null;
    [SerializeField] private GameObject m_body = null;
    [SerializeField] private GameObject m_payRespectsButton = null;
    [SerializeField] private Transform m_groundTransform = null;

    private GrannyState m_state = GrannyState.TOMB;
    private Transform m_playerTransform = null;
    private Vector3 m_distanceToPlayer = Vector3.zero;
    private float m_startingYLevel = 0f;
    private bool m_isPlayerClose = false;

    private void Awake()
    {
        m_startingYLevel = transform.position.y;
        m_payRespectsButton.SetActive(false);
    }

    private void Start()
    {
        m_playerTransform = FindAnyObjectByType<PlayerController>().transform;
    }

    private void Update()
    {
        switch (m_state)
        {
            case GrannyState.TOMB:
                {
                    MoveBackAnnoyingly((m_isPlayerClose) ? 1.5f : 0.5f);
                }
                break;
            case GrannyState.GHOST:
                {

                }
                break;
            case GrannyState.ALIVE:
                {

                }
                break;
            default:
                {
                }
                break;
        }
    }

    private void OnTriggerEnter(Collider _other)
    {
        m_isPlayerClose = true;

        if (m_payRespectsButton != null &&
            !m_payRespectsButton.activeSelf)
        {
            m_payRespectsButton.SetActive(true);
        }

        float offset = 250f;

        if (m_payRespectsButton != null)
        {
            m_payRespectsButton.transform.position += new Vector3(
                Random.Range(-offset, offset),
                Random.Range(-offset, offset),
                0f
            );
        }

        InputManager.s_Instance.UnlockAndShowCursor();
    }
    private void OnTriggerExit(Collider _other)
    {
        m_isPlayerClose = false;
    }


    private void MoveBackAnnoyingly(float _moveSpeed)
    {
        Vector3 position = transform.position;
        m_distanceToPlayer = position - m_playerTransform.position;
        position += Time.deltaTime * _moveSpeed * m_distanceToPlayer;
        position.y = m_startingYLevel;
        transform.position = position;
        m_groundTransform.position = position;
    }


    public void AppearAsGhost()
    {
        m_state = GrannyState.GHOST;
        m_ghost.SetActive(true);

        Instantiate(m_coinsZonePrefab);
    }
}
