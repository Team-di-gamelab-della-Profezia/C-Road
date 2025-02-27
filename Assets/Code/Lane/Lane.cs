using UnityEngine;
using System.Collections;
using TMPro;

public class Lane : MonoBehaviour
{

    public GameObject[] spawnPoints;

    public GameObject[] laneTypes;

    LaneType myLaneType = LaneType.grass;
    GameObject[] myObjectsToSpawn;
    bool spawnToLeft = true;
    Vector2 mySpawnInterval;
    Vector2 myLaneSpeed;

    // public MeshRenderer laneRenderer;
    public Material[] laneMaterials;



    bool isFirstSpawn = true;



    // Init all parameters of a lane
    public void InitLane(LaneType newLaneType, GameObject[] objectsToSpawn, bool moveToLeft, Vector2 laneSpeed, Vector2 laneInterval)
    {
        // set lane type and material
        myLaneType = newLaneType;
        // laneRenderer.material = laneMaterials[(int)myLaneType];

        Instantiate(laneTypes[(int)myLaneType], transform.position, transform.rotation);

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
        float waitTime = 0f;


        if (isFirstSpawn)
        {
            if (myLaneType == LaneType.water || myLaneType == LaneType.lava)
            {
                waitTime = 0;
            }
        } else {
            waitTime = Random.Range(mySpawnInterval.x, mySpawnInterval.y);
        }

        isFirstSpawn = false;

        // print("SPAWN OBJ");
        // Aspetta per spawn interval
        yield return new WaitForSeconds(waitTime);
        SpawnObstacle();
    }

    // Spawn an obstacle
    public void SpawnObstacle()
    {
        if (myLaneType == LaneType.grass)
        {
            return;
        }
        int randomIndex = Random.Range(0, myObjectsToSpawn.Length);

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
        spawnedObject = Instantiate(myObjectsToSpawn[randomIndex], spawnLocation, tmpRot);

        if (spawnedObject != null)
        {
            // Imposta la direzione di movimento
            spawnedObject.GetComponent<ObjectMovement>().SetMovementDirection(spawnToLeft, deathLocation);
            // Imposta la velocità di movimento
            spawnedObject.GetComponent<ObjectMovement>().SetMovementSpeed(Random.Range(myLaneSpeed.x, myLaneSpeed.y));
        }

        // richiama la coroutine
        StartCoroutine("SpawnWithInterval");
    }
}
