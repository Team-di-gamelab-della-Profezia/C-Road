using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class Obtacles_Movement : MonoBehaviour
{
    public float _speed;
    public float _maxDistance;
    Vector3 movementDirection;
    public Vector3 spawnPoint;
    bool isLeft;


    private void FixedUpdate()
    {
        transform.Translate(movementDirection * _speed * Time.deltaTime);

        // Distruggi l'oggetto se supera una certa distanza
        if (isLeft)
        {
            if (transform.position.z > spawnPoint.z)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (transform.position.z < spawnPoint.z)
            {
                Destroy(gameObject);
            }
        }
    }



    public void SetMovementDirection(bool moveToLeft, Vector3 init)
    {
        spawnPoint = init;
        isLeft = moveToLeft;

        if (moveToLeft) {
            movementDirection = new Vector3(0, 0 , 1);
        }
        else {
            movementDirection = new Vector3(0, 0, -1);
        }
    }

    public void SetMovementSpeed(float movSpeed)
    {
        _speed = movSpeed;
    }
}
