using UnityEngine;
using UnityEngine.SceneManagement; // Necessario per il cambio di scena

public class AutoFollowCamera : MonoBehaviour
{
    public Transform player;
    public float baseSpeed = 5f;
    public float speedIncrease = 3f;
    public float PtFugaNord = 0.7f;
    public float PtFugaSud = 0.1f;
    public float distance;
    public string sceneToLoad = "GameOverScene"; // Nome della scena da caricare

    private float currentSpeed;
    public GameObject cam;
    private bool hasStarted = false;

    void Start()
    {
        currentSpeed = baseSpeed;
    }

    void Update()
    {
        // Controlla se il giocatore ha premuto un tasto di movimento per la prima volta
        if (!hasStarted && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space) ||
                            Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) ||
                            Input.GetKeyDown(KeyCode.RightArrow)))
        {
            hasStarted = true;
        }

        CheckIfOutOfView(); // Controlla se il giocatore è uscito dallo schermo
    }

    void FixedUpdate()
    {
        if (!hasStarted) return;

        distance = Mathf.Abs(player.position.x) - Mathf.Abs(cam.transform.position.x);

        if (distance > PtFugaNord)
        {
            currentSpeed = baseSpeed + speedIncrease;
        }
        else
        {
            currentSpeed = baseSpeed;
        }

        transform.Translate(Vector3.left * Time.deltaTime * currentSpeed);
    }

    void CheckIfOutOfView()
    {
        // Verifica se il giocatore è fuori dallo schermo
        Vector3 screenPosition = Camera.main.WorldToViewportPoint(player.position);

        if (screenPosition.x < 0 || screenPosition.x > 1 || screenPosition.y < 0) // Fuori a sinistra, destra o in basso
        {
            Debug.Log("Game Over - Uscito dalla visuale!");
            SceneManager.LoadScene(sceneToLoad); // Carica la scena selezionata
        }
    }
}


