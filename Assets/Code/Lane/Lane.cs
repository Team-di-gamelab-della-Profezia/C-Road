using UnityEngine;
using System.Collections;
using TMPro;

public class Lane : MonoBehaviour
{

    [Header("References")]
    public GameObject[] spawnPoints;

    [Space(20)]
    [Header("Variables")]
    public LaneType myLaneType = LaneType.grass;
    GameObject[] myObjectsToSpawn;
    bool spawnToLeft = true;
    float mySpawnInterval = 1.5f;
    float myLaneSpeed = 5f;

    [Space(20)]
    [Header("Materials")]
    public MeshRenderer laneRenderer;
    public Material[] laneMaterials;





    // Init all parameters of a lane
    public void InitLane(LaneType newLaneType, GameObject[] objectsToSpawn, bool moveToLeft, float laneSpeed, float laneInterval)
    {
        // set lane type and material
        myLaneType = newLaneType;
        laneRenderer.material = laneMaterials[(int)myLaneType];

        // Set objacles meshes to spawn
        myObjectsToSpawn = objectsToSpawn;

        // Set direction
        spawnToLeft = moveToLeft;

        // Set speed
        myLaneSpeed = laneSpeed;

        // Set interval
        mySpawnInterval = laneInterval;

        StartCoroutine("SpawnWithInterval");
    }


    // Wait some time and the spawn an obstacle
    public IEnumerator SpawnWithInterval()
    {
        // Aspetta per spawn interval
        yield return new WaitForSeconds(mySpawnInterval);
        SpawnObstacle();
    }

    // Spawn an obstacle
    public void SpawnObstacle()
    {
        if (myLaneType == LaneType.grass) { return; }

        Vector3 spawnLocation;
        Vector3 deathLocation;

        if (spawnToLeft) {
            // Get Left position to spawn
            spawnLocation = spawnPoints[0].transform.position;
            // Get Right position to die
            deathLocation = spawnPoints[1].transform.position;
        }
        else {
            // Get Right position
            spawnLocation = spawnPoints[1].transform.position;
            // Get Left position to die
            deathLocation = spawnPoints[0].transform.position;
        }

        // Spawna un oggetto
        GameObject spawnedObject = null;
        Quaternion tmpRot = spawnToLeft == true ? new Quaternion(0, 0, 0, 0) : new Quaternion (0, 90, 0, 0);
        spawnedObject = Instantiate(myObjectsToSpawn[0], spawnLocation, tmpRot);

        if (spawnedObject != null)
        {
            // Imposta la direzione di movimento
            spawnedObject.GetComponent<ObjectMovement>().SetMovementDirection(spawnToLeft, deathLocation);
            // Imposta la velocità di movimento
            spawnedObject.GetComponent<ObjectMovement>().SetMovementSpeed(myLaneSpeed);
        }

        // richiama la coroutine
        StartCoroutine("SpawnWithInterval");
    }
}
