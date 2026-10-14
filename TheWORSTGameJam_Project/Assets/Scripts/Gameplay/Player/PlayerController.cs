using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_moveSpeed = 10f;
    [SerializeField] private float m_jumpStrength = 6f;

    private Vector3 m_velocity = Vector3.zero;

    private void Update()
    {
        transform.Translate(Time.deltaTime * m_moveSpeed * m_velocity);
    }
}
