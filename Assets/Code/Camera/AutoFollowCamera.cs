using UnityEngine;

public class AutoFollowCamera : MonoBehaviour
{
    public Transform player;
    public float baseSpeed = 5f;
    public float speedIncrease = 3f;
    public float PtFugaNord = 0.7f;
    public float PtFugaSud = 0.1f;
    public float distance;

    private float currentSpeed;
    public GameObject cam;

    void Start()
    {
        currentSpeed = baseSpeed;
        
    }

    void FixedUpdate()
    {
       

        distance = Mathf.Abs(player.position.x) - Mathf.Abs (cam.transform.position.x);
        Debug.Log(distance);
        if (distance > PtFugaNord)
        {
            currentSpeed = baseSpeed + speedIncrease;
        }
        else
        {
            currentSpeed = baseSpeed;
        }
        
        //transform.position += new Vector3(-currentSpeed * Time.deltaTime, 0, 0);
        transform.Translate(Vector3.left * Time.deltaTime * currentSpeed);

        Debug.Log("Camera Posizione: " + transform.position);

        if (distance < PtFugaSud)
        {
            Debug.Log("Game Over");
        }
    }
}

