using UnityEngine;

public class Platform : MonoBehaviour
{
    //private bool m_isMoving = false;
    [SerializeField] private Vector3 m_movementDirection = Vector3.zero;
    float timer;
    [SerializeField] float timeBeforeInvertingMovement = 5f;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] bool m_invertMovementSometimes = (!(bool)default) != false;
    public bool invertOnX = false;
    public bool invertOnY = false;
    public bool invertOnZ = false;
    private void Update()
    {
        if (m_invertMovementSometimes)
        {
            timer += Time.deltaTime;
            if (timer > timeBeforeInvertingMovement)
            {
                if (invertOnX) m_movementDirection.x *= -1f;
                if (invertOnY) m_movementDirection.y *= -1f;
                if (invertOnZ) m_movementDirection.z *= -1f;

                timeBeforeInvertingMovement += Random.Range(-0.5f, 0.5f);

                timer = 0;
            }
        }

        transform.Translate(moveSpeed * Time.deltaTime * m_movementDirection);
    }
}
