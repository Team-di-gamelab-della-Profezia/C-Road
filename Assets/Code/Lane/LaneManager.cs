using UnityEngine;

public class LaneManager : MonoBehaviour
{
    public GameObject lanePrefab;
    public float laneDistance = 1f;
    Vector2 laneSpeedRange = new Vector2(2, 5);
    Vector2 spawnIntervalRange = new Vector2(3, 6);

    int initialLanes = 10;

    [Header("Lane Dirt")]
    public GameObject[] dirtObstacles;
    public Vector2 dirtLaneSpeedRange = new Vector2(2, 5);
    public Vector2 dirtSpawnIntervalRange = new Vector2(3, 6);

    [Header("Lane Water")]
    public GameObject[] waterObstacles;
    public Vector2 waterLaneSpeedRange = new Vector2(2, 5);
    public Vector2 waterSpawnIntervalRange = new Vector2(3, 6);

    [Header("Lane Lava")]
    public GameObject[] lavaObstacles;
    public Vector2 lavaLaneSpeedRange = new Vector2(2, 5);
    public Vector2 lavaSpawnIntervalRange = new Vector2(3, 6);

    int curLaneIndex = 0;
    float lastPlayerPositionX = 0f; // Memorizza l'ultima posizione X del giocatore

    public Transform player; // Riferimento al giocatore

    private void Start()
    {
        // Create some initial lanes
        for (int i = 0; i < initialLanes - 1; i++)
        {
            CreateLane();
        }

        lastPlayerPositionX = player.position.x; // Inizializza la posizione del giocatore
    }

    private void Update()
    {
        // Verifica se il giocatore si � spostato oltre una certa distanza
        if (player.position.x < lastPlayerPositionX - laneDistance)
        {
            CreateLane();
            lastPlayerPositionX = player.position.x; // Aggiorna la posizione
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
            case LaneType.grass: // grass
                obstaclesToSpawn = null;
                break;
            case LaneType.dirt: // dirt
                obstaclesToSpawn = dirtObstacles;
                laneSpeedRange = dirtLaneSpeedRange;
                spawnIntervalRange = dirtSpawnIntervalRange;
                break;
            case LaneType.water: // water
                obstaclesToSpawn = waterObstacles;
                laneSpeedRange = waterLaneSpeedRange;
                spawnIntervalRange = waterSpawnIntervalRange;
                break;
            case LaneType.lava: // lava
                obstaclesToSpawn = lavaObstacles;
                laneSpeedRange = lavaLaneSpeedRange;
                spawnIntervalRange = lavaSpawnIntervalRange;
                break;
            default:
                obstaclesToSpawn = null;
                print("Incorrect lane");
                break;
        }

        // Set random dir
        int tmpInt = Random.Range(0, 2);
        bool tmpDir = tmpInt < 0.5f ? false : true;

        // Init lane
        curLane.GetComponent<Lane>().InitLane(newLaneType, obstaclesToSpawn, tmpDir, spawnIntervalRange, spawnIntervalRange);
    }
}
