using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseMenu;         // Riferimento al menu di pausa
    private bool isPaused = false;       // Flag per verificare se il gioco è in pausa
    public Button resumeButton;          // Riferimento al pulsante di ripresa
    public Button pauseButton;           // Riferimento al pulsante di pausa (se vuoi gestirlo anche tramite un altro pulsante)

    void Start()
    {
        // Inizializza il menu di pausa come disabilitato
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }

        // Aggiungi il listener per il pulsante di ripresa
        if (resumeButton != null)
        {
            resumeButton.onClick.AddListener(ResumeGame);
        }

        // Aggiungi il listener per il pulsante di pausa
        if (pauseButton != null)
        {
            pauseButton.onClick.AddListener(TogglePause);
        }
    }

    void Update()
    {
        // Se viene premuto il tasto P, alterna la pausa
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    // Funzione per alternare la pausa
    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1; // Imposta la velocità del tempo su 0 (pausa) o 1 (normale)

        // Mostra o nasconde il menu di pausa
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(isPaused);
        }

        // Disabilita o abilita il movimento del player
        Movement player = UnityEngine.Object.FindFirstObjectByType<Movement>();
        if (player != null)
        {
            player.enabled = !isPaused;
        }

        // Disabilita o abilita il movimento di tutti i Rigidbody
        Rigidbody[] rigidbodies = UnityEngine.Object.FindObjectsByType<Rigidbody>(UnityEngine.FindObjectsSortMode.None);
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = isPaused; // Rende il Rigidbody "cinematico" (non influenzato dalla fisica) in pausa
        }
    }

    // Funzione per riprendere il gioco
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;  // Ripristina la velocità normale del gioco

        // Nascondi il menu di pausa
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }

        // Riabilita il movimento del player
        Movement player = UnityEngine.Object.FindFirstObjectByType<Movement>();
        if (player != null)
        {
            player.enabled = true;
        }

        // Riabilita il movimento di tutti i Rigidbody
        Rigidbody[] rigidbodies = UnityEngine.Object.FindObjectsByType<Rigidbody>(UnityEngine.FindObjectsSortMode.None);
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = false;  // Rimuovi la modalità cinematica
        }
    }
}

