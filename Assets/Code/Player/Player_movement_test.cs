using UnityEngine;

public class Player_movement_test : MonoBehaviour
{
   public float moveDistance = 1f; // Distanza di spostamento per ogni input
    public float moveSpeed = 10f;   // Velocità di movimento
    public float jumpHeight = 0.5f; // Altezza del salto
    private Vector3 targetPosition;
    private bool isJumping = false;

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        if (!isJumping && transform.position == targetPosition) // Controlla se ha raggiunto la posizione desiderata
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                Move(Vector3.forward);
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                Move(Vector3.back);
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                Move(Vector3.left);
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                Move(Vector3.right);
        }
    }

    void Move(Vector3 direction)
    {
        targetPosition += direction * moveDistance;
        StartCoroutine(Jump());
    }

    System.Collections.IEnumerator Jump()
    {
        isJumping = true;
        Vector3 startPosition = transform.position;
        Vector3 peakPosition = startPosition + Vector3.up * jumpHeight;
        float elapsedTime = 0f;
        float jumpDuration = moveDistance / moveSpeed;

        while (elapsedTime < jumpDuration / 2)
        {
            transform.position = Vector3.Lerp(startPosition, peakPosition, (elapsedTime / (jumpDuration / 2)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        elapsedTime = 0f;
        while (elapsedTime < jumpDuration / 2)
        {
            transform.position = Vector3.Lerp(peakPosition, targetPosition, (elapsedTime / (jumpDuration / 2)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        isJumping = false;
    }
}
