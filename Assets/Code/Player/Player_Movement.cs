using UnityEngine;

public class Player_movement : MonoBehaviour
{
    public float _distance;
    public float _jumpheight;
    private Rigidbody _rb;
    private bool _isGrounded;

    [SerializeField] Camera_Movement Camera_Movement;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && _isGrounded)
        {
            _rb.AddForce(transform.up * _jumpheight);
            _rb.AddForce(Vector3.left * _distance);
            _isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.D) && _isGrounded)
        {
            _rb.AddForce(transform.up * _jumpheight);
            _rb.AddForce(Vector3.forward * _distance);
            _isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.A) && _isGrounded)
        {
            _rb.AddForce(transform.up * _jumpheight);
            _rb.AddForce(Vector3.back * _distance);
            _isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.S) && _isGrounded)
        {
            _rb.AddForce(transform.up * _jumpheight);
            _rb.AddForce(Vector3.right * _distance);
            _isGrounded = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("cam_accellerator"))
        {
            Camera_Movement._speed = 10f;
        }
        return;
    }
}