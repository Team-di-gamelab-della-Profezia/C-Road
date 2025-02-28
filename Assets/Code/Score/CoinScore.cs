using UnityEngine;
using TMPro;

public class CoinScore : MonoBehaviour
{
    public TMP_Text coinText; // Riferimento al testo che mostra il punteggio
    private int score = 0;    // Punteggio del player

    // Layer per oggetti selezionabili
    public LayerMask selectableLayer;

    void Start()
    {
        LoadScore();
        UpdateScore();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Controlla se l'oggetto con cui il player entra in collisione è nell layer selezionabile
        if (((1 << other.gameObject.layer) & selectableLayer) != 0)
        {
            // Aggiungi punti al punteggio
            score += 5;

            // Aggiorna e salva il punteggio
            UpdateScore();
            SaveScore();

            // Disattiva il collider per evitare collisioni multiple
            other.GetComponent<Collider>().enabled = false;

            // Distruggi l'oggetto selezionabile
            Destroy(other.gameObject);
        }
    }

    void UpdateScore()
    {
        if (coinText != null)
        {
            coinText.text = "Coin/s: " + score; // Aggiorna il testo con il punteggio
        }
        else
        {
            Debug.LogWarning("coinText non è assegnato correttamente nel CoinScore.");
        }
    }

    void SaveScore()
    {
        // Salva il punteggio con PlayerPrefs
        PlayerPrefs.SetInt("PlayerScore", score);
        PlayerPrefs.Save();
    }

    void LoadScore()
    {
        // Carica il punteggio salvato
        score = PlayerPrefs.GetInt("PlayerScore", 0);
    }
}
