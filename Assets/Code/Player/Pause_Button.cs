using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool isPaused = false;

    void Start()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;

        // Mostra o nasconde il menu di pausa
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(isPaused);
        }

        // Disabilita il movimento del player e di altri oggetti
        Movement player = FindObjectOfType<Movement>();
        if (player != null)
        {
            player.enabled = !isPaused;
        }

        // Disabilita il movimento di tutti i GameObject con Rigidbody
        Rigidbody[] rigidbodies = FindObjectsOfType<Rigidbody>();
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = isPaused;
        }
    }
}

