using UnityEngine;

public class CameraBGColourChanger : MonoBehaviour
{
    [SerializeField] [Range(0f, 60f)] float m_colourChangeInterval = 1.5f;

    private Camera m_camera = null;
    private float m_colourChangeTimer = 0f;

    private void Awake()
    {
        m_camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (m_camera == null)
        {
            return;
        }

        m_colourChangeTimer += Time.deltaTime;

        if (m_colourChangeTimer >= m_colourChangeInterval)
        {
            m_camera.backgroundColor = ColourUtils.RandomRGB01();
            m_colourChangeTimer = 0f;
        }
    }
}
