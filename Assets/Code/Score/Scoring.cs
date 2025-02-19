using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    public float moveDistance = 1f; // Distanza di movimento per ogni pressione del tasto
    public float moveSpeed = 5f; // Velocità di movimento per simulare un salto veloce
    public TMP_Text scoreText; // UI per il punteggio
    private int score = 0;
    private float maxDistanceReached = 0f; // Distanza massima raggiunta in avanti
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        maxDistanceReached = startPosition.z;
        UpdateScore();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            Move(Vector3.forward, true);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(Vector3.back, false);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(Vector3.left, false);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(Vector3.right, false);
        }
    }

    void Move(Vector3 direction, bool checkScore)
    {
        transform.position += direction * moveDistance;
        transform.position = Vector3.Lerp(transform.position, transform.position + direction * moveDistance, moveSpeed * Time.deltaTime);

        if (checkScore && transform.position.z > maxDistanceReached)
        {
            maxDistanceReached = transform.position.z;
            score++;
            UpdateScore();
        }
    }

    void UpdateScore()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}

