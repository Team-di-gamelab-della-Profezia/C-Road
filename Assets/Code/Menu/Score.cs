using UnityEngine;
using UnityEngine.UI;  
using TMPro;            

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;   
    public TMP_Text tmpScoreText; 

    private void Start()
    {
        
        int highScore = PlayerPrefs.GetInt("Score", 0);

        // Mostra il punteggio
        if (scoreText != null)
            scoreText.text = "Score: " + highScore;

        if (tmpScoreText != null)
            tmpScoreText.text = "Score: " + highScore;
    }

    
    public static void SaveScore(int newScore)
    {
        int highScore = PlayerPrefs.GetInt("Score", 0);

        if (newScore > highScore)
        {
            PlayerPrefs.SetInt("Score", newScore);
            PlayerPrefs.Save(); 
        }
    }
}
