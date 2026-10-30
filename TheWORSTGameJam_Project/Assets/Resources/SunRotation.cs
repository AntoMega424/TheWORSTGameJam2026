using UnityEngine;

public class SunRotation : MonoBehaviour
{
    [Header("Speed at which the rotation rotates the rotatee, in units")] public float rotationVitesse = 1.5f;
    [HideInInspector]
    protected Vector3 mm_rotationAxes;

    private void Awake()
    {
        mm_rotationAxes = new Vector3().Random();
    }

    void Update()
    {
        transform
            .
             Rotate
             (
                 rotationVitesse
                       *
                 Time.fixedUnscaledDeltaTime
                       *
                 mm_rotationAxes
             )
        ;
    }
}
