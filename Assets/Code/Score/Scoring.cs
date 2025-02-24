using TMPro;
using UnityEngine;

public class Scoring : MonoBehaviour
{
    public TMP_Text scoreText; // UI per il punteggio

    int score = 0;

    public void UpdateScore()
    {
        score++;

        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}