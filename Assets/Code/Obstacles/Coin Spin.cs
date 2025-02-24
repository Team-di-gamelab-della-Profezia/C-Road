using UnityEngine;

public class CoinRotation : MonoBehaviour
{
    public float rotationSpeed = 100f; // Velocità di rotazione regolabile

    void Update()
    {
        // Ruota la moneta lungo l'asse Y
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
