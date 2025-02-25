using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public AudioSource footstepSource; // AudioSource per i passi
    public AudioClip footstepClip; // Suono dei passi
    public float stepInterval = 0.5f; // Tempo tra i suoni dei passi

    private Rigidbody rb;
    private float stepTimer;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (footstepSource == null)
        {
            footstepSource = gameObject.AddComponent<AudioSource>(); // Crea un AudioSource se non è assegnato
            Debug.Log("AudioSource creato!");
        }

        if (footstepClip == null)
        {
            Debug.LogError("Errore: Nessun suono assegnato a footstepClip!");
        }

        footstepSource.clip = footstepClip;
        footstepSource.loop = false; // Evita loop continuo
        footstepSource.playOnAwake = false;

        Debug.Log("Script avviato!");
    }

    private void Update()
    {
        // Controlla se il player preme le frecce direzionali o la barra spaziatrice
        bool isMoving = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) ||
                        Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow);

        bool isJumping = Input.GetKeyDown(KeyCode.Space);

        // Debug per vedere se i tasti di movimento sono rilevati
        if (isMoving)
        {
            Debug.Log("Movimento rilevato!");
        }
        else
        {
            Debug.Log("Nessun movimento rilevato.");
        }

        // Controlla se il player sta toccando il terreno
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);
        Debug.Log("IsGrounded: " + isGrounded);

        if (isMoving && isGrounded)
        {
            stepTimer += Time.deltaTime;

            if (stepTimer >= stepInterval)
            {
                Debug.Log("Movimento rilevato! Suono in riproduzione...");
                footstepSource.PlayOneShot(footstepClip);  // Prova a riprodurre il suono
                Debug.Log("Suono riprodotto!");
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = 0f; // Reset quando il personaggio è fermo
        }

        // Se il player salta, riproduci il suono immediatamente
        if (isJumping && isGrounded)
        {
            Debug.Log("Salto rilevato! Suono in riproduzione...");
            footstepSource.PlayOneShot(footstepClip);  // Suono per il salto
            Debug.Log("Suono riprodotto per salto!");
        }
    }
}
