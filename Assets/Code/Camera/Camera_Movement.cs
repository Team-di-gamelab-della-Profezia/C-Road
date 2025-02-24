using UnityEngine;

public class Camera_Movement : MonoBehaviour
{
    public float _speed;
    private bool hasStarted = false; // Controlla se la telecamera deve iniziare a muoversi

    private void Update()
    {
        // Controlla se il giocatore ha premuto un tasto di movimento per la prima volta
        if (!hasStarted && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space) ||
                            Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) ||
                            Input.GetKeyDown(KeyCode.RightArrow)))
        {
            hasStarted = true;
        }
    }

    private void FixedUpdate()
    {
        if (hasStarted)
        {
            CameraMovement();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("cam_accelerator"))
        {
            _speed *= 1.2f; // Aumento della velocità del 20% invece di ridurla
        }
    }

    private void CameraMovement()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime);
    }
}
