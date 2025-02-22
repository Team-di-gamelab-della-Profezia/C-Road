using UnityEngine;
using System.Collections;
using TMPro;

public class Lane : MonoBehaviour
{
    [HideInInspector] public enum laneType {grass, dirt, lava, water};

    [InspectorName("References")]
    public GameObject[] spawnPoints;

    [Space(20)]
    [InspectorName("Variables")]
    public laneType myLaneType = laneType.grass;
    public GameObject[] objectsToSpawn;
    public bool spawnToLeft = true;
    public float spawnInterval = 1.5f;
    public float laneSpeed = 5f;

    [Space(20)]
    [InspectorName("Materials")]
    public MeshRenderer laneRenderer;
    public Material[] laneMaterials;


    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        laneRenderer.material = laneMaterials[(int)myLaneType];
        StartCoroutine("SpawnWithInterval");
    }

    
    public IEnumerator SpawnWithInterval()
    {
        // Aspetta per spawn interval
        yield return new WaitForSeconds(spawnInterval);
        SpawnObstacle();
    }

    public void SpawnObstacle()
    {
        Vector3 spawnLocation;
        Vector3 deathLocation;

        if (spawnToLeft) {
            // Get Left position to spawn
            spawnLocation = spawnPoints[0].transform.position;
            // Get Right position to die
            deathLocation = spawnPoints[1].transform.position;
        } else {
            // Get Right position
            spawnLocation = spawnPoints[1].transform.position;
            // Get Left position to die
            deathLocation = spawnPoints[0].transform.position;
        }

        // Spawna un oggetto
        GameObject spawnedObject = null;
        spawnedObject = Instantiate(objectsToSpawn[0], spawnLocation, transform.rotation);

        // Imposta la direzione di movimento
        spawnedObject.GetComponent<Obtacles_Movement>().SetMovementDirection(spawnToLeft, deathLocation);
        // Imposta la velocità di movimento
        spawnedObject.GetComponent<Obtacles_Movement>().SetMovementSpeed(laneSpeed);


        // richiama la coroutine
        StartCoroutine("SpawnWithInterval");
    }
}
