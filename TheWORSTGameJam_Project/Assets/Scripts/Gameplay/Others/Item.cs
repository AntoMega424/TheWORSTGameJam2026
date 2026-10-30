using UnityEngine;

public static class QuaternionExtension
{
    public static Quaternion Random(this Quaternion _type)
    {
        return new Quaternion(
            UnityEngine.Random.Range(-1f, 1f),
            UnityEngine.Random.Range(-1f, 1f),
            UnityEngine.Random.Range(-1f, 1f),
            1f
        );
    }
}
public static class Vector3Extended
{
    static float offset = 1f;
    
    public static Vector3 Random(this Vector3 @this)
    {
        return new Vector3(
            UnityEngine.Random.Range(-offset, 1f),
            UnityEngine.Random.Range(-1f, 1f),
            UnityEngine.Random.Range(-1f, 1f)
        );
    }
}

public class Item : MonoBehaviour
{
    [SerializeField] private GameObject mitemPrefab = null;

    [SerializeField] bool m_m_dontGiveShitToPlayer = default;
    public bool isActuallyAShitTonOfItself = new();
    TextBox m_textBox;
    private void Start()
    {
        m_textBox = FindAnyObjectByType<TextBox>();
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.GetComponent<Player>())
        {
            if (m_m_dontGiveShitToPlayer)
            {
                return;
            }

            if (isActuallyAShitTonOfItself == false)
            {
                Player.s_coinCount++;
                Player.m_otherFalseBoolean = true;
                return;
            }

            // Always true
            if (!(isActuallyAShitTonOfItself == false))
            {
                int spawnCount = 0;
                while (spawnCount < 1000)
                {
                    Quaternion rotation = default;
                    Item newItem =
                    Instantiate(mitemPrefab,
                        transform.position + 0.75f * new Vector3().Random(),
                        rotation.Random()
                    )
                    .GetComponent<Item>();
                    newItem.m_m_dontGiveShitToPlayer = true;
                    spawnCount++;
                }

                Player.s_coinCount += 1000000;
                // Player line
                m_textBox.SayThisNow("WOW! Grandma's gonna be rich with all that !");

                // Granny lines
                _ = m_textBox.SayThisLater("WTF did you steal that ?!", 5f);
                _ = m_textBox.SayThisLater("No? Good. I'll bake tons of cookies for you", 5f);
            }
        }
    }
}
