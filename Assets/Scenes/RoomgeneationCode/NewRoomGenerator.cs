using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeightedRoom
{
    public GameObject prefab;
    public float RandomWeight = 1;
}

public class NewRoomGenerator : MonoBehaviour
{
    public static NewRoomGenerator Instance { get; private set; }

    public float iterations = 0;
    public float maxIterations = 5;

    public List<GameObject> roomPrefabs = new List<GameObject>();
    public List<GameObject> roomSpawnPoints = new List<GameObject>();
    public GameObject bossRoomprefab;
    public GameObject endPointprefab;


    public bool NextIteration = false;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
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
            GenerateRoom(roomSpawnPoints[0]);
        }
    }

    object GetRandomRoomPrefab()
    {
        return roomPrefabs[Random.Range(0, roomPrefabs.Count)];
    }


    public void GenerateRoom(GameObject spawnPoint)
    {
        Debug.Log("Generating room...");

        // Get a random room prefab
        GameObject roomPrefab = (GameObject)GetRandomRoomPrefab();
        GameObject room = Instantiate(roomPrefab, spawnPoint.transform.position, Quaternion.identity);

        // Destroy the spawn point
        Destroy(spawnPoint);

        Debug.Log("Room generated");
    }
}


