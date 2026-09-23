using UnityEngine;

public class BasicCameraFollow2D : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField]
    private Vector3 offset =
        new Vector3(0f, 1f, -10f);

    private void LateUpdate()
    {
        if (target == null) return;
        transform.position = target.position + offset;
    }
}
