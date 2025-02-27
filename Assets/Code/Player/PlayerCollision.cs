using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Ottiene il Rigidbody del player
    }

    void OnCollisionEnter(Collision collision)
    {
        // Controlla se l'oggetto ha il tag "Ostacolo"
        if (collision.gameObject.CompareTag("Ostacolo"))
        {
            // Ferma il movimento del player con la nuova API di Unity
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}

