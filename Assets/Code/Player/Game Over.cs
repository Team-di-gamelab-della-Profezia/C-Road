using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    [Header("Impostazioni Game Over")]
    public string sceneToLoad = "MainMenu";  // Nome della scena di game over
    public LayerMask selectableLayer;        // Layer degli oggetti selezionabili
    public int punteggioCorrente;            // Punteggio del giocatore

    public Transform player;                 // Riferimento al giocatore
    public AudioSource audioSource;          // Riferimento all'AudioSource
    public AudioClip deathSound;             // Suono di morte

    private bool gameOver = false;           // Flag per evitare chiamate multiple

    private void Update()
    {
        // Controlla se la posizione del giocatore lungo l'asse Z è fuori dai limiti
        if (!gameOver && (player.position.z >= 9f || player.position.z <= -9f))
        {
            Debug.Log("Player fuori dai limiti. Game Over.");
            EndGame();  // Fine del gioco e cambio scena
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!gameOver && IsSelectable(other.gameObject))
        {
            Debug.Log("Oggetto selezionato. Game Over.");
            EndGame();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!gameOver && IsSelectable(collision.gameObject))
        {
            Debug.Log("Oggetto selezionato tramite collisione. Game Over.");
            EndGame();
        }
    }

    bool IsSelectable(GameObject obj)
    {
        return ((1 << obj.layer) & selectableLayer) != 0;
    }

    void EndGame()
    {
        if (gameOver) return;  // Evita che venga eseguito più volte
        gameOver = true;  // Imposta il flag per evitare chiamate multiple

        // Riproduci il suono di morte prima di caricare la scena
        if (audioSource != null && deathSound != null)
        {
            Debug.Log("Suono di morte riprodotto.");
            audioSource.PlayOneShot(deathSound);  // Riproduce il suono
        }

        // Avvia la Coroutine per aspettare il suono e poi caricare la scena
        StartCoroutine(WaitForSoundAndLoadScene(deathSound.length));
    }

    // Coroutine per aspettare il suono e caricare la scena
    IEnumerator WaitForSoundAndLoadScene(float soundDuration)
    {
        yield return new WaitForSeconds(soundDuration);  // Attende la durata del suono
        LoadGameOverScene();
    }

    void LoadGameOverScene()
    {
        // Debug per verificare che la scena venga caricata
        Debug.Log("Caricamento scena di Game Over...");
        ScoreManager.SaveScore(punteggioCorrente);
        SceneManager.LoadScene(sceneToLoad);  // Carica la scena di game over
    }
}
