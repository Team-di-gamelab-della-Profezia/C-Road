using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveDistance = 1f;  // Distanza di movimento per ogni pressione del tasto
    public float moveSpeed = 5f;  // Velocità di movimento per simulare un salto veloce
    public float jumpHeight = 2f;  // Altezza del salto
    private Vector3 startPosition;

    private Scoring scoreScript;
    public LaneManager laneManager;

    private bool isJumping = false;
    private float jumpTime = 0f;
    private Vector3 targetPosition;

    private bool isBlocked = false;  // Variabile per bloccare il movimento su collisione

    void Start()
    {
        laneManager = FindAnyObjectByType<LaneManager>();
        scoreScript = GetComponent<Scoring>();

        startPosition = transform.position;
    }

    void Update()
    {
        // Se il giocatore è nel mezzo di un salto o è bloccato, non deve potersi muovere
        if (isJumping || isBlocked) return;

        // Movimento in avanti (asse X negativo)
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            StartJump(Vector3.left); // Movimento in avanti (direzione -X)
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartJump(Vector3.right); // Movimento indietro (direzione +X)
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StartJump(Vector3.back); // Movimento a sinistra (asse Z negativo)
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartJump(Vector3.forward); // Movimento a destra (asse Z positivo)
        }
    }

    void StartJump(Vector3 direction)
    {
        if (isJumping) return;

        // Verifica se ci sono ostacoli nella zona di atterraggio
        RaycastHit hit;
        if (Physics.Raycast(transform.position + direction, Vector3.down, out hit, jumpHeight))
        {
            if (hit.collider.CompareTag("Ostacolo"))
            {
                // Se c'è un ostacolo nella zona di atterraggio, non avviare il salto
                return;
            }
        }

        // Se non ci sono ostacoli, avvia il salto
        isJumping = true;
        isBlocked = false;  // Reset blocco
        targetPosition = transform.position + direction * moveDistance;
        jumpTime = 0f;
    }

    void FixedUpdate()
    {
        if (isJumping)
        {
            PerformJump();
        }
        else
        {
            // Movimento orizzontale quando non si sta saltando
            MovePlayer();
        }
    }

    void PerformJump()
    {
        jumpTime += Time.fixedDeltaTime * moveSpeed;

        // Calcolo della parabola del salto
        float height = Mathf.Sin(jumpTime * Mathf.PI) * jumpHeight;
        Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, jumpTime);
        newPosition.y += height;

        transform.position = newPosition;

        if (jumpTime >= 1f)
        {
            isJumping = false;

            // Allineamento finale della posizione in base al terreno
            RaycastHit hit;
            if (Physics.Raycast(targetPosition + Vector3.up, Vector3.down, out hit, 2f))
            {
                targetPosition.y = hit.point.y;
            }

            transform.position = new Vector3(targetPosition.x, targetPosition.y, targetPosition.z);
        }
    }

    void MovePlayer()
    {
        // Movimento orizzontale
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.back * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Se il player collide con un oggetto Ostacolo
        if (collision.collider.CompareTag("Ostacolo"))
        {
            isJumping = false;
            isBlocked = true;  // Blocca il movimento se il player collide con un ostacolo
        }

        // Se il player atterra su un oggetto Saltabile
        if (collision.collider.CompareTag("Saltabile"))
        {
            isJumping = false;
            isBlocked = false;  // Rimuovi il blocco per il movimento
        }
    }

    void OnCollisionStay(Collision collision)
    {
        // Gestisce la collisione con oggetti con tag "Ostacolo"
        if (collision.collider.CompareTag("Ostacolo"))
        {
            isBlocked = true;  // Blocca il movimento finché il player è a contatto con l'ostacolo
        }
    }

    // Se il player viene imparentato con una piattaforma
    void OnTransformParentChanged()
    {
        if (transform.parent != null)
        {
            // Se il player è imparentato, i movimenti della piattaforma influenzeranno il player
            // Rimuoviamo la fisica (Rigidbody) in modo che il movimento sia gestito tramite la posizione
            this.GetComponent<Rigidbody>().isKinematic = true;
        }
        else
        {
            // Se il player non è imparentato, attiviamo la fisica per il movimento normale
            this.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
