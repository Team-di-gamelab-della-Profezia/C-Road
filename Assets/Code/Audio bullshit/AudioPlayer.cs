using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public AudioSource audioSource;

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        // Assicura che il Box Collider sia un Trigger
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        if (boxCollider == null)
        {
            Debug.LogError("Errore: Nessun Box Collider trovato! Aggiungilo e attiva 'Is Trigger'.");
        }
        else
        {
            boxCollider.isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Qualcosa è entrato nel trigger: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Il player ha raccolto la moneta!");

            if (audioSource != null && audioSource.clip != null)
            {
                Debug.Log("Tentativo di riproduzione suono...");
                audioSource.PlayOneShot(audioSource.clip);
                Debug.Log("Suono riprodotto!");
            }
            else
            {
                Debug.LogError("Errore: AudioSource o Audio Clip non assegnato!");
            }

            // Distrugge la moneta dopo aver riprodotto il suono
            Destroy(gameObject);
        }
    }
}
