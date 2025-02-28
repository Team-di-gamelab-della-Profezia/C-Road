using UnityEngine;

public class CoinSound : MonoBehaviour
{
    public AudioClip coinSound;  // Suono della moneta

    // Funzione che viene chiamata quando c'è una collisione con la moneta
    void OnTriggerEnter(Collider other)
    {
        // Controlla se l'oggetto con cui si è in collisione è una moneta
        if (other.CompareTag("Coin"))
        {
            PlayCoinSound();  // Riproduce il suono della moneta

            // Distrugge la moneta dopo la collisione
            Destroy(other.gameObject);
        }
    }

    // Funzione per riprodurre il suono della moneta
    void PlayCoinSound()
    {
        if (coinSound != null && AudioManager.instance != null && AudioManager.instance.sfxAudioSource != null)
        {
            AudioManager.instance.sfxAudioSource.PlayOneShot(coinSound);  // Riproduce il suono della moneta
        }
    }
}
