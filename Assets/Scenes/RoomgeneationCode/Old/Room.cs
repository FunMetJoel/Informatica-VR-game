using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<Transform> RoomEndPoints = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        
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
}
