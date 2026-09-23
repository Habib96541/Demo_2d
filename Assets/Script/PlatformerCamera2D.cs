using UnityEngine;

public class PlatformerCamera2D : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Rigidbody2D targetBody;
    [SerializeField] Vector3 baseOffset = new(0f, 1f, -10f);
    [SerializeField] float lookAheadDistance = 2.5f;
    [SerializeField] float smoothTime = 0.18f;
    private Vector3 velocity;

    private void LateUpdate()
    {
        if (target == null || targetBody == null) return;
        float direction = Mathf.Sign(targetBody.linearVelocity.x);
        float moving = Mathf.Clamp01(Mathf.Abs(targetBody.linearVelocity.x));
        Vector3 lookAhead = Vector3.right * direction *
                            lookAheadDistance * moving;
        Vector3 desired = target.position + baseOffset + lookAhead;
        transform.position = Vector3.SmoothDamp(
            transform.position, desired, ref velocity, smoothTime);
    }
}
