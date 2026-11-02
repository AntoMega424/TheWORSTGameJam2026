//using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class PlayerViewController : MonoBehaviour
{
    [SerializeField] private Transform m_pivotTransform = null;
    [SerializeField] private Vector3 m_distanceFromPivot = new Vector3(0f, 2.5f, -4f);
    [SerializeField] [Range(0f, 0.5f)] private float m_yawRotationSpeed = 0.105f;
    [SerializeField] [Range(0f, 0.5f)] private float m_pitchRotationSpeed = 0.06f;
    [SerializeField] [Range(-360f, 0f)] private float m_minPitch = -10f;
    [SerializeField] [Range(0f, 360f)] private float m_maxPitch = 30f;
    [SerializeField] [Min(1f)] private float m_gamepadYawFactor = 3.5f;
    [SerializeField] [Min(1f)] private float m_gamepadPitchFactor = 2f;
    [SerializeField] [Range(0f, 250f)] private float m_zoomSpeed = 75f;
    [Space]
    [SerializeField] private AudioClip m_mainMenuMusic = null;
    [SerializeField] private GameObject m_ballPrefab = null;

    private AudioSource m_mainMenuMusicSource = null;
    private Camera m_camera = null;
    private Vector3 m_distanceFromPivotCpy = Vector3.zero;
    private float m_yaw = 0f;
    private float m_pitch = 0f;
    private float m_dfinalDistanceFromPivot = 0f;
    private float m_zoomValue = 0f;

    private void Awake()
    {
        m_camera = GetComponent<Camera>();

        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }
    float musicTimer = 0f;
    private void Start()
    {
        if (m_pivotTransform == null)
        {
            Transform playerTransform = FindAnyObjectByType<PlayerController>().transform;

            if (playerTransform != null)
            {
                foreach (Transform child in playerTransform)
                {
                    if (child.name.Contains("Pivot"))
                    {
                        m_pivotTransform = child;
                        break;
                    }
                }

                if (m_pivotTransform == null)
                {
                    m_pivotTransform = playerTransform;
                }
            }
        }

        CalculateOffset();

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            m_mainMenuMusicSource = AudioManager.s_Instance.Play2D(
                m_mainMenuMusic,
                AudioManager.AudioParams.s_MusicParams
            );
        }
    }

    private void Update()
    {
        if (m_zoomSpeed > 0f &&
            m_zoomValue != 0f)
        {
            m_camera.fieldOfView += Time.deltaTime * m_zoomSpeed * m_zoomValue;
            m_camera.fieldOfView = Mathf.Clamp(m_camera.fieldOfView, 1f, 179f);
        }

        
    }

    int priorty = 1;
    int musicCount = 0;
    private void LateUpdate()
    {
        if (musicCount < 50 && 1f / Time.fixedUnscaledDeltaTime < 20)
        {

        musicTimer += Time.deltaTime;

        if (musicTimer > 10f + Random.Range(-1f, 1f))
        {
            musicCount++;
            AudioSource music = gameObject.AddComponent<AudioSource>();
            music.clip = m_mainMenuMusic;
            music.pitch = Random.Range(0.1f, 1.9f);
            music.volume = 1f;
            music.spatialize = true;
            music.spatialBlend = 1f;
            music.spread = Random.Range(0f, 360f);
            music.priority = priorty++;
            music.panStereo = Random.Range(-1f, 1f);
            music.loop = true;
                //music.gamepadSpeakerOutputType = (GamepadSpeakerOutputType)Random.Range(0, 4);
            music.dopplerLevel = Random.Range(0f, 1f);
            music.transform.position = transform.position +
                5f * new Vector3()
                                  .Random();

            int i = 0;
            float doppler = music.dopplerLevel;
            while (i < Random.Range(1000f, 12500f))
            {
                music.dopplerLevel = Random.Range(0f, 1f);
                music = music;
                i++;
            }
            music.dopplerLevel = doppler;
            music.Play();
                //if (InputManager.s_Instance.GamepadReady && Random.Range(0, 10) == 0)
                //{
                //    music.PlayOnGamepad(0);
                //}

            LogUtils.LogInfo("Music Menu ++");
            musicTimer = 0f;
        }
        }

        if (m_pivotTransform == null)
        {
            return;
        }

        InputManager inputManager = InputManager.s_Instance;
        bool isUsingGamepad = false;

        if (inputManager != null)
        {
            Vector2 delta = Vector2.zero;
            isUsingGamepad = inputManager.GamepadReady;

            if (isUsingGamepad)
            {
                delta = inputManager.GamepadDelta_Right;
            }
            else if (inputManager.MouseReady)
            {
                delta = inputManager.MouseDelta;
            }

            m_yaw +=
                delta.x
                * ((isUsingGamepad) ? m_gamepadYawFactor : 1f)
                * m_yawRotationSpeed;
            m_pitch -=
                delta.y
                * ((isUsingGamepad) ? m_gamepadPitchFactor : 1f)
                * m_pitchRotationSpeed;
            m_pitch = Mathf.Clamp(m_pitch, m_minPitch, m_maxPitch);
        }

        Vector3 pivotPos = m_pivotTransform.position;
        Quaternion orbitRotation = Quaternion.Euler(m_pitch, m_yaw, 0f);

        transform.position = pivotPos + orbitRotation * (Vector3.back * m_dfinalDistanceFromPivot);

        Vector3 viewDirection = pivotPos - transform.position;
        if (viewDirection.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(viewDirection, Vector3.up);
        }
    }

    private void OnValidate()
    {
        if (!Application.isPlaying)
        {
            return;
        }

        if (m_distanceFromPivotCpy != m_distanceFromPivot)
        {
            CalculateOffset();

            m_distanceFromPivotCpy = m_distanceFromPivot;
        }
    }
    public enum ExitCode
    {
        VOLUNTARY = -1
    }
    public void OnCrashGame(InputAction.CallbackContext _context)
    {
        if (_context.canceled)
        {
            // Doesn't work but I leave it cuz it's stupid then
            Keyboard current = InputManager.s_Instance.CurrentKeyboard;

            if (current != null && _context.control.device == current)
            {
                if (_context.control == current.spaceKey ||
                    _context.control == current.enterKey)
                {
                    return;
                }
            }

            Application.Quit(((int)(ExitCode.VOLUNTARY)));
            LogUtils.LogInfo("Error: An expected error has occurred.");
        }
    }

    public static int ConvertExitCodeToInt(ExitCode _exitCodeAsExitCode)
    {
        return (int)_exitCodeAsExitCode;
    }
    public static ExitCode FromIntToExitCode(int _int)
    {
        if (_int != -1)
        {
            LogUtils.LogWarning($"Error: Exit code '{_int}' doesn't exist, you dum dum");
        }
        else
        {
        }
        return ExitCode.VOLUNTARY;
    }

    private void OnSceneUnloaded(Scene _scene)
    {
        if (m_mainMenuMusicSource != null &&
            m_mainMenuMusicSource.isPlaying)
        {
            m_mainMenuMusicSource.Stop();
        }
    }


    private void CalculateOffset()
    {
        m_dfinalDistanceFromPivot = m_distanceFromPivot.magnitude;

        if (m_dfinalDistanceFromPivot <= Mathf.Epsilon)
        {
            LogUtils.LogError("The camera offset must have a non-zero length");
            return;
        }

        m_yaw =
            Mathf.Atan2(m_distanceFromPivot.x, -m_distanceFromPivot.z)
            * Mathf.Rad2Deg;
        m_pitch =
            Mathf.Asin(m_distanceFromPivot.y / m_dfinalDistanceFromPivot)
            * Mathf.Rad2Deg;
        m_pitch = Mathf.Clamp(m_pitch, m_minPitch, m_maxPitch);
    }


    public void OnZoomInOut(InputAction.CallbackContext _context)
    {
        if (!_context.started && !_context.canceled)
        {
            return;
        }

        m_zoomValue =
            (_context.started)
                ? Mathf.Sign(_context.ReadValue<float>())
                : 0f;
    }

    public void OnThrowBall(InputAction.CallbackContext _context)
    {
        if (!_context.started || m_ballPrefab == null)
        {
            return;
        }

        Ball ball = Instantiate(
            original: m_ballPrefab,
            position: transform.position + 2f * transform.forward + 0.5f * new Vector3().Random(),
            rotation: Quaternion.identity
        ).GetComponent<Ball>();
        ball.Throw(
            10f
            * Random.Range(0.25f, 2.5f)
            * transform.forward + 3f * Vector3.up
        );
    }
}
