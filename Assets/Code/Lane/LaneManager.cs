using UnityEngine;

public class LaneManager : MonoBehaviour
{
    public GameObject lanePrefab;
    public float laneDistance = 1f;
    public Vector2 laneSpeedRange = new Vector2 (2, 5);
    public Vector2 spawnIntervalRange = new Vector2 (3, 6);

    //float initialOffset = 4;
    int initialLanes = 10;

    [Header("Lane Dirt")]
    public GameObject[] dirtObstacles;
    public Vector2 minMaxDirtLanes = new Vector2 (1,6);

    [Header("Lane Water")]
    public GameObject[] waterObstacles;
    public Vector2 minMaxWaterLanes = new Vector2(1, 3);

    [Header("Lane Lava")]
    public GameObject[] lavaObstacles;
    public Vector2 minMaxLavaLanes = new Vector2(1, 3);


    int curLaneIndex = 0;


    private void Start()
    {
        // Create some initial lanes
        for (int i = 0; i < initialLanes -1; i++) {
            CreateLane();
        }
    }

    public void CreateLane()
    {
        curLaneIndex++;

        GameObject curLane = Instantiate(lanePrefab, transform.position - new Vector3(curLaneIndex * laneDistance, 0, 0), transform.rotation);

        // Create new lane type
        LaneType newLaneType = (LaneType)Random.Range(0, 4);
        GameObject[] obstaclesToSpawn;

        // Do stuff relative to lane type
        switch (newLaneType)
        {
            case LaneType.grass: obstaclesToSpawn = null; // grass
                break;
            case LaneType.dirt: obstaclesToSpawn = dirtObstacles; // dirt
                break;
            case LaneType.water: obstaclesToSpawn = waterObstacles; // water
                break;
            case LaneType.lava: obstaclesToSpawn = lavaObstacles; // lava
                break;
            default: obstaclesToSpawn = null; print("Incorrect lane");
                break;
        }

        // Set random dir
        int tmpInt = Random.Range(0, 2);
        bool tmpDir = tmpInt < 0.5f ? false : true;

        // Set random speed
        float tmpSpeed = Random.Range(laneSpeedRange.x, laneSpeedRange.y);

        // Set random interval
        float tmpInterval = Random.Range(spawnIntervalRange.x, spawnIntervalRange.y);

        // Init lane
        curLane.GetComponent<Lane>().InitLane(newLaneType, obstaclesToSpawn, tmpDir, tmpSpeed, tmpInterval);

    }
}
