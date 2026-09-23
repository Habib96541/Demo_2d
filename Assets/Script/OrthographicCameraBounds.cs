using UnityEngine;

[RequireComponent(typeof(Camera))]
public class OrthographicCameraBounds : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector2 minWorld, maxWorld;
    [SerializeField] float smoothTime = 0.18f;
    private Camera cam;
    private Vector3 velocity;

    private void Awake() => cam = GetComponent<Camera>();

    private void LateUpdate()
    {
        if (target == null || !cam.orthographic) return;
        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;
        float x = Mathf.Clamp(target.position.x,
            minWorld.x + halfWidth, maxWorld.x - halfWidth);
        float y = Mathf.Clamp(target.position.y,
            minWorld.y + halfHeight, maxWorld.y - halfHeight);
        Vector3 desired = new(x, y, transform.position.z);
        transform.position = Vector3.SmoothDamp(
            transform.position, desired, ref velocity, smoothTime);
    }
}
