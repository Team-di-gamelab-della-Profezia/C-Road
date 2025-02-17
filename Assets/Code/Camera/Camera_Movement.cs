using UnityEngine;
using UnityEngine.UIElements;

public class Camera_Movement : MonoBehaviour
{
    public float _speed;
    Vector3 _position;
    private void FixedUpdate()
    {
        CameraMovement();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("cam_accelerator"))
        {
           _speed =  _speed * 0.2f;
        }
    }
    private void CameraMovement()
    {
        transform.Translate(Vector3.left * _speed);
    }
}