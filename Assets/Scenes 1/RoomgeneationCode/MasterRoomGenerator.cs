using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MasterRoomGenerator : MonoBehaviour
{
    public List<GameObject> possiblePrefabs = new List<GameObject>();

    [SerializeField] private List<Transform> RoomEndPoints = new List<Transform>();

    public bool NextIteration = false;


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
            for (int i = 0; i < RoomEndPoints.Count; i++)
            {
                spawnPrefab(possiblePrefabs[0], RoomEndPoints[i]);
                RoomEndPoints.Remove(RoomEndPoints[i]);
            }
        }
    }

    void spawnPrefab(GameObject prefab, Transform transform)
    {
        GameObject newRoom = Instantiate(prefab, transform);
        for (int i = 0; i < newRoom.GetComponent<Room>().RoomEndPoints.Count; i++)
        {
            RoomEndPoints.Add(newRoom.GetComponent<Room>().RoomEndPoints[i]);
        }
    }
}
