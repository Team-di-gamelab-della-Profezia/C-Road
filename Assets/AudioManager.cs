using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource; // Per la musica di sottofondo

    [Header("Musica")]
    public AudioClip background;

    [Header("Effetti Sonori")]
    public AudioClip topo;
    public AudioClip cane;
    public AudioClip gatto;
    public AudioClip toro;
    public AudioClip lava;
    public AudioClip acqua;
    public AudioClip morte;
    public AudioClip moneta;

    private void Start()
    {
        if (musicSource == null)
        {
            Debug.LogError("Errore: Nessun AudioSource assegnato per la musica!");
            return;
        }

        if (background != null)
        {
            Debug.Log("Avvio della musica di sottofondo...");
            musicSource.clip = background;
            musicSource.loop = true;  // La musica si ripete
            musicSource.volume = 1.0f; // Assicura che il volume sia alto
            musicSource.Play();
            Debug.Log("Musica in background avviata con successo!");
        }
        else
        {
            Debug.LogError("Errore: Nessun AudioClip assegnato alla musica di sottofondo!");
        }
    }
}
