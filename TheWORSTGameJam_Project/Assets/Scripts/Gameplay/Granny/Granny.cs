using System.Net;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameObejct = UnityEngine.GameObject;
public class Granny : MonoBehaviour
{
    public AudioClip shreksophone = null;
    private enum GrannyState
    {
        TOMB,
        GHOST,
        ALIVE
    }
    [SerializeField] GameObejct ballGranny;
    public GameObject cookie;
    [SerializeField] private GameObject m_coinsZonePrefab = null;
    [SerializeField] private GameObject m_ghost = null;
    [SerializeField] private GameObject m_body = null;
    [SerializeField] private GameObject m_payRespectsButton = null;
    [SerializeField] private Transform m_groundTransform = null;

    [SerializeField]private GrannyState m_state = GrannyState.TOMB;
    private Transform m_playerTransform = null;
    private Vector3 m_distanceToPlayer = Vector3.zero;
    private float m_startingYLevel = 0f;
    private bool m_isPlayerClose = false;

    private void Awake()
    {
        m_startingYLevel = transform.position.y;
        if (m_payRespectsButton)
        m_payRespectsButton.SetActive(false);
    }

    private void Start()
    {
        m_playerTransform = FindAnyObjectByType<PlayerController>().transform;
        if (SceneManager.GetActiveScene().name.EndsWith('3') && FindAnyObjectByType<Player>())
        {
            Destroy(FindAnyObjectByType<Player>());
            _ = SpawnBallGranny(20);
        }
    }
    async Task SpawnBallGranny(int seconds)
    {
        await Task.Delay(1000 * seconds);

        Instantiate(ballGranny, transform);
        AudioManager.s_Instance.Play2D(shreksophone, AudioManager.AudioParams.s_SoundParams);
        FindAnyObjectByType<TextBox>().SayThisNow("GRANNY BIG BALL NOW, GRANNY DESTROY EVERYTHING\nso be careful my dear");
        _ = FindAnyObjectByType<TextBox>().SayThisLater("OMG it's Shrek !", 7f);
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
                    if (!isAlive)
                    {
                        m_body.SetActive(true);
                        isAlive = true;
                    }

                    if (Random.Range(0, 101) <= 75)
                    ThrowCookies();
                    GameCookie.CookieNumber++;

                    if (m_body.transform.position.y <= 1.5f)
                    {
                        m_body.GetComponent<Rigidbody>().AddForce(Random.Range(3f, 12f) * Vector3.up, ForceMode.Impulse);
                    }
                }
                break;
            default:
                {
                }
                break;
        }
    }
    void ThrowCookies()
    {
        GameCookie newcookie = Instantiate(cookie).GetComponent<GameCookie>();
        newcookie.direction = new Vector3().Random() * 2f +
            (Vector3.up + transform.position - FindAnyObjectByType<Player>().transform.position);
    }
    bool isAlive;
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
