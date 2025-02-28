using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [Header("Impostazioni Game Over")]
    public string sceneToLoad = "MainMenu";  // Nome della scena di game over
    public LayerMask selectableLayer;        // Layer degli oggetti selezionabili
    public int punteggioCorrente;            // Punteggio del giocatore

    public Transform player;                 // Riferimento al giocatore
    public AudioSource audioSource;          // Riferimento all'AudioSource
    public AudioClip deathSound;             // Suono di morte

    private void Update()
    {
        // Controlla se la posizione del giocatore lungo l'asse Z è fuori dai limiti
        if (player.position.z >= 9f || player.position.z <= -9f)
        {
            EndGame();  // Fine del gioco e cambio scena
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsSelectable(other.gameObject))
        {
            EndGame();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsSelectable(collision.gameObject))
        {
            EndGame();
        }
    }

    bool IsSelectable(GameObject obj)
    {
        return ((1 << obj.layer) & selectableLayer) != 0;
    }

    void EndGame()
    {
        // Riproduci il suono di morte prima di caricare la scena
        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);  // Riproduce il suono
        }

        // Attendi che il suono finisca prima di caricare la scena
        Invoke("LoadGameOverScene", deathSound.length);  // Aspetta la durata del suono
    }

    void LoadGameOverScene()
    {
        Debug.Log("Salvataggio punteggio finale: " + punteggioCorrente);
        ScoreManager.SaveScore(punteggioCorrente);
        SceneManager.LoadScene(sceneToLoad);  // Carica la scena di game over
    }
}
