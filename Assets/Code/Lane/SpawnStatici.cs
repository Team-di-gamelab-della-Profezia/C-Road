using UnityEngine;
using System.Collections.Generic;

public class StaticObjectSpawner : MonoBehaviour
{
    [Header("Oggetti statici disponibili")]
    public List<GameObject> staticObjects = new List<GameObject>();

    [Header("Numero minimo e massimo di oggetti")]
    public Vector2 minMaxObjects = new Vector2(1, 5);

    [Header("Transform della corsia")]
    public Transform laneTransform;

    private List<Vector3> occupiedPositions = new List<Vector3>();
    private int[] validSpawnPositionsX = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 }; // 12 quadrati centrali
    private float minZ = -7f;
    private float maxZ = 7f;
    private float objectSize = 2f;

    private void Start()
    {
        SpawnStaticObjects();
    }

    public void SpawnStaticObjects()
    {
        if (staticObjects.Count == 0 || laneTransform == null)
        {
            Debug.LogError("Oggetti statici non assegnati o Transform della corsia nullo!");
            return;
        }

        int objectCount = Random.Range((int)minMaxObjects.x, (int)minMaxObjects.y + 1);

        for (int i = 0; i < objectCount; i++)
        {
            GameObject selectedObject = staticObjects[Random.Range(0, staticObjects.Count)];
            int attempts = 10;
            bool placed = false;

            while (attempts > 0 && !placed)
            {
                int randomIndex = Random.Range(0, validSpawnPositionsX.Length);
                float spawnX = validSpawnPositionsX[randomIndex];
                float spawnZ = Random.Range(minZ, maxZ);

                Vector3 spawnPosition = laneTransform.position + new Vector3(-spawnX, 0, spawnZ);

                if (!IsOverlapping(spawnPosition))
                {
                    Instantiate(selectedObject, spawnPosition, Quaternion.identity, laneTransform);
                    occupiedPositions.Add(spawnPosition);
                    placed = true;
                }
                attempts--;
            }
        }
    }

    private bool IsOverlapping(Vector3 position)
    {
        foreach (Vector3 occupied in occupiedPositions)
        {
            if (Vector3.Distance(position, occupied) < objectSize)
            {
                return true;
            }
        }
        return false;
    }
}
