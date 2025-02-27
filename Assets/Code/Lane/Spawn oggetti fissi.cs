using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn; // Array di oggetti assegnabili dall'Inspector
    public Transform spawnPoint; // Punto di spawn, se lasciato nullo userà la posizione dell'oggetto a cui è assegnato lo script

    void Start()
    {
        SpawnObject();
    }

    public void SpawnObject()
    {
        if (objectsToSpawn == null || objectsToSpawn.Length == 0)
        {
            return;
        }

        // Rimuove eventuali oggetti nulli dall'array
        objectsToSpawn = System.Array.FindAll(objectsToSpawn, obj => obj != null);

        if (objectsToSpawn.Length == 0)
        {
            return;
        }

        int randomIndex = Random.Range(0, objectsToSpawn.Length);
        GameObject selectedPrefab = objectsToSpawn[randomIndex];

        if (selectedPrefab == null)
        {
            return;
        }

        Vector3 spawnPosition = spawnPoint ? spawnPoint.position : transform.position;

        GameObject spawnedObject = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
    }
}

