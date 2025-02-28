using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource sfxAudioSource;  // Riferimento all'AudioSource per gli effetti sonori

    // Singleton per accedere all'AudioManager globalmente
    public static AudioManager instance;

    private void Awake()
    {
        // Se non esiste già un'istanza, imposta questa come l'istanza
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Assicurati che il GameObject non venga distrutto tra le scene
        DontDestroyOnLoad(gameObject);
    }
}

