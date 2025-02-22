using UnityEngine;

public class LaneManager : MonoBehaviour
{
    public GameObject lanePrefab;
    public float laneDistance = 1f;
    float initialOffset = 4;
    int initialLanes = 10;

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
