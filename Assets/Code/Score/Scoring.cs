using TMPro;
using UnityEngine;

public class Scoring : MonoBehaviour
{
    public TMP_Text scoreText;
    public Transform player;

    private int score = 0;
    private float minDistance = 0f;

    void Start()
    {
        if (player != null)
        {
            minDistance = player.position.x;
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

            if (currentDistance < minDistance)
            {
                minDistance = currentDistance;
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

