using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine.InputSystem;

public static class StringWithAnAdditionalFeature
{
    public static bool DoesNotEndWith(this string self, char _compared)
    {
        return self.EndsWith(_compared) == false;
    }
    public static bool DoesNotEndWith(this string self, string _compared)
    {
        return self.EndsWith(_compared) == false;
    }
}

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

        if (Random.Range(0, (2+1)) == 1)
        {
            transform.Translate(
                Random.Range(-1f, 1f),
                Random.Range(0f, 2f),
                Random.Range(-0.75f, 0.25f)
            );
        }
    }

    private void Start()
    {
        m_uiText = FindAnyObjectByType<TextBox>();
    }

    private void Update()
    {

        if (isPlayableStat){
            m_state = PlayerState.PLAYABLE;}
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
                        if (objWithTransitionShader)
                            objWithTransitionShader.SetActive(true);
                        else
                            LogUtils.LogWarning("Oh connaaaareuh");

                            transitionToLevel2Coroutine =
                                StartCoroutine(TransitionToLevel2());
                        m_thisBooleanIsFalse = true;
                        LogUtils.LogInfo("UwU Level 2", Color.bisque);
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

    [SerializeField] GameObject objWithTransitionShader = null;
    Coroutine transitionToLevel2Coroutine = null;
    private IEnumerator TransitionToLevel2()
    {
        LogUtils.LogInfo("Waiting", Color.white);
        yield return new WaitForSeconds(5f);

        LogUtils.LogInfo("We should load level 2 now");
        AsyncOperation levelLoading = SceneManager.LoadSceneAsync("Level2");

        if (levelLoading.isDone)
        StopCoroutine(transitionToLevel2Coroutine);
    }
    static bool isPlayableStat = false;
    public static void SetToPlayableState()
    {
        isPlayableStat = true;
    }
    public void PayRespects()
    {

    }
    void FixedUpdate()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (GetComponent<PlayerInput>().enabled)
        {
            if (SceneManager.GetActiveScene().name.EndsWith('2'))
            {
                GetComponent<PlayerInput>().enabled = false;
            }
        }
        else if (!GetComponent<PlayerInput>().enabled)
        {
            if (SceneManager.GetActiveScene().name.DoesNotEndWith('2'))
            {
                GetComponent<PlayerInput>().enabled = true;
            }
        }
    }
}
