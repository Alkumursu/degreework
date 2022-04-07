using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    [Range(1,10)]
    public float smoothFactor;

    private void LateUpdate()
    {
        Follow();
    }

    void Follow()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.Slerp(transform.position, targetPosition, Time.deltaTime * smoothFactor);
    }
}
