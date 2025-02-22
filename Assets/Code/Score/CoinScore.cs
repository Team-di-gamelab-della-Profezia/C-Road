using UnityEngine;
using TMPro;

public class CoinScore : MonoBehaviour
{
    public TMP_Text coinText; // Riferimento al testo TMP
    private int score = 0;

    private void Start()
    {
        LoadScore();
        UpdateScore();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            score += 5; // Aggiunge 5 punti quando raccoglie una moneta
            Debug.Log("Score:" + score);
            UpdateScore();
            SaveScore();
            Destroy(other.gameObject); // Rimuove la moneta
        }
    }

    void UpdateScore()
    {
        if (coinText != null)
        {
            coinText.text = "Coin/s: " + score;
        }
    }

    void SaveScore()
    {
        PlayerPrefs.SetInt("PlayerScore", score);
        PlayerPrefs.Save();
    }

    void LoadScore()
    {
        score = PlayerPrefs.GetInt("PlayerScore", 0);
    }
}
