using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathTrigger : MonoBehaviour
{
    [Header("Impostazioni")]
    public string sceneToLoad = "GameOverScene"; // Nome della scena da caricare

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Controlla se il trigger è attivato dal giocatore
        {
            Debug.Log("Giocatore entrato nel DeathTrigger. Caricamento scena: " + sceneToLoad);
            LoadScene();
        }
    }

    void LoadScene()
    {
        // Assicurati che il nome della scena sia corretto
        if (SceneExists(sceneToLoad))
        {
            Time.timeScale = 1f; // Ripristina il tempo in caso di pause
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogError("ERRORE: La scena '" + sceneToLoad + "' non esiste nei Build Settings! Aggiungila e riprova.");
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
