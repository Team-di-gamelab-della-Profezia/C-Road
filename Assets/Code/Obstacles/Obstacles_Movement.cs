using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class Obtacles_Movement : MonoBehaviour
{
    public float _speed;
    public float _maxDistance;
    private void FixedUpdate()
    {
        transform.Translate(Vector3.back * _speed * Time.deltaTime);
    }

    private void Update()
    {
        if (transform.position.z < -_maxDistance)
        {
            Destroy(gameObject);
        }
    }
}
