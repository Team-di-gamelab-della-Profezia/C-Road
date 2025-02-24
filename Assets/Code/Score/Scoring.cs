using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    public TMP_Text scoreText; 

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