using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveDistance = 1f;
    public float moveSpeed = 5f;
    public float jumpHeight = 2f;
    private Vector3 targetPosition;
    private bool isJumping = false;
    private float jumpTime = 0f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isJumping) return;

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            AttemptMove(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            AttemptMove(Vector3.right);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            AttemptMove(Vector3.back);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            AttemptMove(Vector3.forward);
        }
    }

    void AttemptMove(Vector3 direction)
    {
        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, moveDistance))
        {
            if (hit.collider.CompareTag("Ostacolo"))
            {
                return;
            }
        }
        StartJump(direction);
    }

    void StartJump(Vector3 direction)
    {
        isJumping = true;
        targetPosition = transform.position + direction * moveDistance;
        jumpTime = 0f;
    }

    void FixedUpdate()
    {
        if (isJumping)
        {
            PerformJump();
        }
    }

    void PerformJump()
    {
        jumpTime += Time.fixedDeltaTime * moveSpeed;
        float height = Mathf.Sin(jumpTime * Mathf.PI) * jumpHeight;
        Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, jumpTime);
        newPosition.y += height;

        rb.linearVelocity = (newPosition - transform.position) / Time.fixedDeltaTime;

        if (jumpTime >= 1f)
        {
            isJumping = false;
            if (Physics.Raycast(targetPosition + Vector3.up, Vector3.down, out RaycastHit hit, 2f))
            {
                targetPosition.y = hit.point.y;
            }
            rb.position = new Vector3(targetPosition.x, targetPosition.y, targetPosition.z);
            rb.linearVelocity = Vector3.zero;
        }
    }
}