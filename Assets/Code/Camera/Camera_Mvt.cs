using UnityEngine;

public class AutoFollowCamera : MonoBehaviour
{
    public Transform player;
    public float baseSpeed = 5f;
    public float speedIncrease = 3f;
    public float upperThreshold = 0.7f;
    public float lowerThreshold = 0.1f;

    private float currentSpeed;
    private Camera cam;

    void Start()
    {
        currentSpeed = baseSpeed;
        cam = Camera.main;
    }

    void Update()
    {
        if (player == null || cam == null) return;

        Vector3 viewportPos = cam.WorldToViewportPoint(player.position);

        if (viewportPos.y > upperThreshold)
        {
            currentSpeed = baseSpeed + speedIncrease;
        }
        else
        {
            currentSpeed = baseSpeed;
        }

        transform.position += Vector3.left * currentSpeed * Time.deltaTime;

        if (viewportPos.y < lowerThreshold)
        {
            Debug.Log("Game Over");
        }
    }
}

