using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveDistance = 1f; // Distanza di movimento per ogni pressione del tasto
    public float moveSpeed = 5f; // Velocità di movimento per simulare un salto veloce
    private float maxDistanceReached = 0f; // Distanza massima raggiunta in avanti
    private Vector3 startPosition;

    private Scoring scoreScript;
    LaneManager laneManager;



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
            Move(Vector3.left, true);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(Vector3.right, false);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(Vector3.back, false);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(Vector3.forward, false);
        }
        #endregion
    }

    void Move(Vector3 direction, bool checkScore)
    {
        transform.position += direction * moveDistance;
        transform.position = Vector3.Lerp(transform.position, transform.position + direction * moveDistance, moveSpeed * Time.deltaTime);

        if (checkScore && transform.position.x < maxDistanceReached)
        {
            maxDistanceReached = transform.position.z;
            scoreScript.UpdateScore();


            laneManager.CreateLane(gameObject.transform.position);
        }
    }
}
