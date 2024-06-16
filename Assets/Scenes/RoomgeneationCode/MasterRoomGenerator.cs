using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class DungeonRoomGeneration
{
    public GameObject prefab;
    public float RandomWeight = 1;
}

public class MasterRoomGenerator : MonoBehaviour
{
    public List<DungeonRoomGeneration> possibleRooms = new List<DungeonRoomGeneration>();

    [SerializeField] private List<Transform> RoomEndPoints = new List<Transform>();
    [SerializeField] private List<Transform> OldRoomEndPoints = new List<Transform>();

    public bool NextIteration = false;

    public Vector2 Max2DDungeonSize = new Vector2(10, 10);
    public float DungeonMargin = 0.5f;

    public int MaxFloors = 5;
    public float FloorHeight = 3;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        for (int i = 0; i < MaxFloors; i++)
        {
            Gizmos.DrawWireCube(new Vector3(0, (i + 0.5f) * FloorHeight, 0), new Vector3(Max2DDungeonSize.x, FloorHeight, Max2DDungeonSize.y));
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(0, 0.5f * MaxFloors * FloorHeight, 0), new Vector3(Max2DDungeonSize.x, MaxFloors * FloorHeight, Max2DDungeonSize.y));
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3(0, 0.5f * MaxFloors * FloorHeight, 0), new Vector3(Max2DDungeonSize.x - DungeonMargin, MaxFloors * FloorHeight, Max2DDungeonSize.y - DungeonMargin));
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (NextIteration)
        {
            NextIteration = false;
            OldRoomEndPoints = new List<Transform>(RoomEndPoints);
            RoomEndPoints = new List<Transform>();
            for (int i = 0; i < OldRoomEndPoints.Count; i++)
            {
                spawnRandomRoom(OldRoomEndPoints[i]);
            }
        }
    }

    void spawnRandomRoom(Transform endpoint)
    {
        float totalWeight = 0;
        foreach (DungeonRoomGeneration room in possibleRooms)
        {
            totalWeight += room.RandomWeight;
        }

        float randomValue = Random.Range(0, totalWeight);
        float weightSum = 0;
        foreach (DungeonRoomGeneration room in possibleRooms)
        {
            weightSum += room.RandomWeight;
            if (randomValue <= weightSum)
            {
                spawnPrefab(room.prefab, endpoint);
                break;
            }
        }
    }

    void spawnPrefab(GameObject prefab, Transform transform)
    {
        GameObject newRoom = Instantiate(prefab, transform.position, transform.rotation);
        for (int i = 0; i < newRoom.GetComponent<Room>().RoomEndPoints.Count; i++)
        {
            RoomEndPoints.Add(newRoom.GetComponent<Room>().RoomEndPoints[i]);
        }
    }
}
