using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGameObject : MonoBehaviour
{ 
    
    private GameObject SpawnPrefab;
    // Start is called before the first frame update
    // void Start()
    // {
    // }

    // Update is called once per frame
    void Update()
    {
    }

    public void SpawnObject(Vector3 spawnPosition, string prefabName)
    {
        SpawnPrefab = Resources.Load<GameObject>(prefabName);
        

        // GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject spawnedObject = Instantiate(SpawnPrefab);
        //Set the position of the cube to match this GameObject's position
        spawnedObject.transform.position = spawnPosition;
        spawnedObject.name = prefabName;

       
    }
}
