using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<Transform> RoomEndPoints = new List<Transform>();

    private Collider RoomHitbox;

    private void Awake()
    {
        RoomHitbox = GetComponent<Collider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector2 size = new Vector2(RoomHitbox.bounds.size.x, RoomHitbox.bounds.size.z);
        Vector2 center = new Vector2(RoomHitbox.bounds.center.x, RoomHitbox.bounds.center.z);

        // Check if there are other rooms in the way
        Collider[] hitColliders = Physics.OverlapBox(center, size / 2, Quaternion.identity);
        Debug.Log(hitColliders.Length);

        // Check if collider is not the current room
        foreach (var hitCollider in hitColliders)
        {
            Debug.Log(hitCollider.name);
            // Check if collider is the current room
            if (hitCollider == RoomHitbox)
            {
                continue;
            }
            Debug.Log("Room intersects with another room");

            // Check if collider is a room
            if (!hitCollider.GetComponent<Room>())
            {
                continue;
            }
            Debug.Log($"{hitCollider.name}");

            Debug.Log("Room intersects with another room");
            ResetRoom();
        }   

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        foreach (var endPoint in RoomEndPoints)
        {
            Gizmos.DrawSphere(endPoint.position, 0.05f);
            Gizmos.DrawLine(endPoint.position, endPoint.position + Quaternion.AngleAxis(90, Vector3.up) * endPoint.forward * 0.2f);
        }

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 0.05f);
        Gizmos.DrawLine(transform.position, transform.position + Quaternion.AngleAxis(-90, Vector3.up) * transform.forward * 0.2f);
    }

    public void ResetRoom()
    {
        NewRoomGenerator.Instance.GenerateRoom(this.gameObject);
    }

}
