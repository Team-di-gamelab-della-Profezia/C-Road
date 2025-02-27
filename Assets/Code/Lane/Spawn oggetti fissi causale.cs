using UnityEngine;

public class Spawnoggettifissicausale : MonoBehaviour

{
    public GameObject[] objectsToSpawn; // Array di oggetti assegnabili dall'Inspector
    public Transform spawnPoint; // Punto di spawn, se lasciato nullo userà la posizione dell'oggetto a cui è assegnato lo script
    [Range(0f, 1f)] public float spawnProbability = 0.2f; // Probabilità di spawn dell'oggetto

    void Start()
    {
        // Questo è l'unico metodo Start presente nel codice.
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

        // Controlla se l'oggetto deve essere spawnato in base alla probabilità
        if (Random.value > spawnProbability)
        {
            return; // Non spawna l'oggetto, in base alla probabilità
        }

        // Se la probabilità è passata, seleziona un oggetto casuale
        int randomIndex = Random.Range(0, objectsToSpawn.Length);
        GameObject selectedPrefab = objectsToSpawn[randomIndex];

        if (selectedPrefab == null)
        {
            return;
        }

        Vector3 spawnPosition = spawnPoint ? spawnPoint.position : transform.position;

        // Instanzia l'oggetto
        GameObject spawnedObject = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
    }
}
