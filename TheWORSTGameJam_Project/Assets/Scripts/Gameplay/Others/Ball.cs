using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    [SerializeField] [Range(0.1f, 120f)]
     private float m_timeToLive = 10f;

    private MeshRenderer m_mr = null;
    private float m_ttlTimer = 0f;

    private void Awake()
    {
        m_mr = GetComponent<MeshRenderer>();
        Material mat = m_mr.material;
        mat.color = ColourUtils.RandomRGB01();
        m_mr.material = mat;
    }

    private void Update()
    {
        m_ttlTimer += Time.deltaTime;

        if (m_ttlTimer >= m_timeToLive)
        {
            Destroy(gameObject);
        }
    }

    public void Throw(Vector3 _throwForce)
    {
        GetComponent<Rigidbody>().AddForce(_throwForce, ForceMode.Impulse);
    }
}
