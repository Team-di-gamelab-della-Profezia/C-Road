using UnityEngine;

public class LaneManager : MonoBehaviour
{
    public GameObject lanePrefab;
    public float laneDistance = 1f;
    float initialOffset = 4;
    int initialLanes = 10;

    [Header("Lane Dirt")]
    public Vector2 minMaxDirtLanes = new Vector2 (1,6);


    [Header("Lane Water")]
    public Vector2 minMaxWaterLanes = new Vector2(1, 3);

    
    private void Start()
    {
        for (int i = 0; i < initialLanes; i++)
        {
            CreateLane(new Vector3(i, 0, 0));
        }
    }


    public void CreateLane(Vector3 playerPosition)
    {
        Instantiate(lanePrefab, new Vector3(playerPosition.x - laneDistance - initialOffset - initialLanes, 0, 0), transform.rotation);
    }
}
