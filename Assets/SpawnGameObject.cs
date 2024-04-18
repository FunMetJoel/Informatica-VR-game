using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGameObject : MonoBehaviour
{ 
    
    private string prefabName = "Frikandellen";
    private GameObject SpawnPrefab;
    // Start is called before the first frame update
    void Start()
    {
        SpawnPrefab = Resources.Load<GameObject>(prefabName);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SpawnObject(Vector3 spawnPosition)
    {
        Debug.Log("Spawn");

        // GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject spawnedObject = Instantiate(SpawnPrefab);
        //Set the position of the cube to match this GameObject's position
        spawnedObject.transform.position = spawnPosition;
        spawnedObject.name = prefabName;


       
    }
}
