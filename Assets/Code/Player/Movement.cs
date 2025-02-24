using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveDistance = 1f; // Distanza di movimento per ogni pressione del tasto
    public float moveSpeed = 5f; // Velocità di movimento per simulare un salto veloce
    public float jumpHeight = 2f; // Altezza del salto
    private float maxDistanceReached = 0f; // Distanza massima raggiunta in avanti
    private Vector3 startPosition;

    private Scoring scoreScript;
    public LaneManager laneManager;

    private bool isJumping = false;
    private float jumpTime = 0f;
    private Vector3 targetPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        laneManager = FindAnyObjectByType<LaneManager>();
        scoreScript = gameObject.GetComponent<Scoring>();

        startPosition = transform.position;
        maxDistanceReached = startPosition.z;
    }

    // Update is called once per frame
    void Update()
    {
        #region Inputs
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            StartJump(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartJump(Vector3.right);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StartJump(Vector3.back);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartJump(Vector3.forward);
        }
        #endregion

        if (isJumping)
        {
            PerformJump();
        }

    }

    void StartJump(Vector3 direction)
    {
        if (isJumping) return; // Non far partire un altro salto se il personaggio sta già saltando
        isJumping = true;
        targetPosition = transform.position + direction * moveDistance;
        jumpTime = 0f; // Reset del tempo del salto
    }

    void PerformJump()
    {
        jumpTime += Time.deltaTime * moveSpeed;

        // Calcolo della parabola del salto
        float height = Mathf.Sin(jumpTime * Mathf.PI) * jumpHeight; // Calcola l'altezza del salto
        Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, jumpTime);

        // Aggiungi l'altezza alla posizione
        newPosition.y = height;

        transform.position = newPosition;

        // Controlla se il salto è completo
        if (jumpTime >= 1f)
        {
            isJumping = false;
            transform.position = new Vector3(targetPosition.x, startPosition.y, targetPosition.z); // Reset dell'altezza a quella di partenza

            // Aggiungi logica per aggiornare il punteggio e le corsie
            if (newPosition.x < maxDistanceReached)
            {
                maxDistanceReached = transform.position.z;
                scoreScript.UpdateScore();

                laneManager.CreateLane(gameObject.transform.position);
            }
        }
    }
}
