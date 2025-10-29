using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public float smoothSpeed = 0.125f;
    public float minZoom = 5f;
    public float maxZoom = 10f;
    public float zoomLimiter = 5f;
    public Vector3 offset;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (player1 == null || player2 == null)
            return;

        Vector3 centerPoint = GetCenterPoint();
        Vector3 desiredPosition = centerPoint + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        float distance = GetGreatestDistance();
        float zoom = Mathf.Lerp(maxZoom, minZoom, distance / zoomLimiter);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoom, Time.deltaTime);
    }

    Vector3 GetCenterPoint()
    {
        return (player1.position + player2.position) / 2f;
    }

    float GetGreatestDistance()
    {
        return Vector3.Distance(player1.position, player2.position);
    }
}
