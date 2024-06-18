using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEditor.VersionControl;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<Transform> RoomEndPoints = new List<Transform>();

    private BoxCollider RoomHitbox;

    private void Awake()
    {
        RoomHitbox = GetComponent<BoxCollider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector3 center = RoomHitbox.center;

        // Check if there are other rooms in the way
        Collider[] hitColliders = Physics.OverlapBox(center, RoomHitbox.size, Quaternion.identity);
        Debug.Log(hitColliders.Length);

        // Check if collider is not the current room
        foreach (var hitCollider in hitColliders)
        {
            // Check if collider is the current room
            if (hitCollider == RoomHitbox)
            {
                continue;
            }

            // Check if collider is a room
            if (!hitCollider.GetComponent<Room>())
            {
                continue;
            }

            Debug.Log("Room intersects with another room");
            ResetRoom();
            return;
        }   

        foreach (var endPoint in RoomEndPoints)
        {
            NewRoomGenerator.Instance.roomSpawnPoints.Add(endPoint.gameObject);
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetRoom()
    {
        NewRoomGenerator.Instance.GenerateRoom(this.gameObject);
    }

}
