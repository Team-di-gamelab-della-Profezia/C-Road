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
        if (!gameOver && (player.position.z >= 9f || player.position.z <= -9f))
        {
            Debug.Log("Player fuori dai limiti. Game Over.");
            EndGame();
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
        if (gameOver) return;
        gameOver = true;

        if (audioSource != null && deathSound != null)
        {
            Debug.Log("Riproduzione audio...");
            audioSource.PlayOneShot(deathSound);
        }

        Debug.Log("Forzando il caricamento della scena tra 1 secondo...");
        Invoke(nameof(ForceLoadGameOverScene), 1f);
    }

    void ForceLoadGameOverScene()
    {
        Debug.Log("Tentativo di caricamento della scena: " + sceneToLoad);

        // Assicurati che il timeScale sia a 1 (se il gioco è in pausa)
        Time.timeScale = 1f;

        // Controlla se la scena esiste nei Build Settings
        if (SceneExists(sceneToLoad))
        {
            Debug.Log("Scena trovata! Caricamento in corso...");
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
        }
        else
        {
            Debug.LogError("ERRORE: La scena '" + sceneToLoad + "' non è nei Build Settings! Aggiungila e riprova.");
        }
    }

    bool SceneExists(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneFileName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            if (sceneFileName == sceneName)
            {
                return true;
            }
        }
        return false;
    }
}
