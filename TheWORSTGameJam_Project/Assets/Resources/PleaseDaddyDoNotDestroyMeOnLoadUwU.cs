using UnityEngine;

public class PleaseDaddyDoNotDestroyMeOnLoadUwU : MonoBehaviour
{
    private void Awake()
    {
        if (Random.Range(0, 1001) != 0)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
