using UnityEngine;
using TMPro;            

public class ScoreManager : MonoBehaviour
{
    public TMP_Text tmpScoreText; 

    private void Start()
    {
        UpdateScoreDisplay();
    }

    void UpdateScoreDisplay()
    {
        
        int finalScore = PlayerPrefs.GetInt("Score", 0);  

        

        if (tmpScoreText != null)
            tmpScoreText.text = "Score: " + finalScore;
    }

  
    public static void SaveScore(int newScore)
    {
        int highScore = PlayerPrefs.GetInt("Score", 0);  

        
        if (newScore > highScore)
        {
            PlayerPrefs.SetInt("Score", newScore);  
            PlayerPrefs.Save(); 
            Debug.Log("Nuovo punteggio massimo salvato: " + newScore);
        }

        
        PlayerPrefs.SetInt("Score", newScore);  
        PlayerPrefs.Save();  
        Debug.Log("Punteggio finale salvato: " + newScore);
    }
}

