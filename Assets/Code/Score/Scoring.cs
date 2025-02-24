using TMPro;
using UnityEngine;

public class Scoring : MonoBehaviour
{
    public TMP_Text scoreText;
    public Transform player;

    private int score = 0;
    private float lastRecordedPosition = 0f; // Memorizza l'ultima posizione valida
    private float minDistance = 0f;
    private float distanceThreshold = 1f; // Distanza minima percorsa per aggiornare il punteggio

    void Start()
    {
        if (player != null)
        {
            minDistance = player.position.x;
            lastRecordedPosition = player.position.x; // Memorizza la posizione iniziale
        }
        UpdateScoreText();
    }

    void Update()
    {
        UpdateScore();
    }

    public void UpdateScore()
    {
        if (player != null)
        {
            float currentDistance = player.position.x;

            // Verifica se il personaggio ha percorso una distanza maggiore della soglia
            if (Mathf.Abs(currentDistance - lastRecordedPosition) >= distanceThreshold)
            {
                // Aggiorna solo quando è stata percorsa almeno una distanza di "distanceThreshold"
                lastRecordedPosition = currentDistance;
                score++;
                UpdateScoreText();
            }
        }
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}
