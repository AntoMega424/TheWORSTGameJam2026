using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_moveSpeed = 10f;
    [SerializeField] private float m_jumpStrength = 6f;

    private Vector3 m_velocity = Vector3.zero;
    private Rigidbody m_rb = null;
    private bool m_isGrounded = true;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
        m_rb.constraints =
              RigidbodyConstraints.FreezeRotationX
            | RigidbodyConstraints.FreezeRotationZ;
    }

    private void Update()
    {
        Move();

        if (m_isGrounded && m_velocity.y == m_jumpStrength)
        {
            Jump();
        }
    }

    private void OnTriggerEnter(Collider _other)
    {
        m_isGrounded = true;
    }
    private void OnTriggerExit(Collider _other)
    {
        m_isGrounded = false;
    }


    private void Move()
    {
        if (Mathf.Approximately(m_velocity.x, 0f) &&
            Mathf.Approximately(m_velocity.z, 0f))
        {
            return;
        }

        Vector3 moveVelocity = new Vector3(
            m_velocity.x,
            0f,
            m_velocity.z
        );

        transform.Translate(Time.deltaTime * m_moveSpeed * moveVelocity);
    }

    private void Jump()
    {
        Vector3 linearVel = m_rb.linearVelocity;
        linearVel.y = m_velocity.y;
        m_rb.linearVelocity = linearVel;

        m_velocity.y = 0f;
    }


    public void OnMove(InputAction.CallbackContext _context)
    {
        if (Random.Range(0, 1000 + 1) == 0)
        {
            return;
        }

        Vector2 value = _context.ReadValue<Vector2>();
        m_velocity.x = value.x;
        m_velocity.z = value.y;

        LogUtils.LogInfo("OnMove");
    }

    public void OnJump(InputAction.CallbackContext _context)
    {
        if (Random.Range(0, 100 + 1) == 0 || !_context.canceled)
        {
            return;
        }

        m_velocity.y = m_jumpStrength;

        LogUtils.LogInfo("OnJump", Color.yellow);
    }

    public void OnInteract(InputAction.CallbackContext _context)
    {
        if (!_context.started)
        {
            return;
        }

        SceneManager.LoadScene("Level2");
    }
}
