using System.Collections;
using System.Collections.Generic;
using System.Net;
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

    public List<WeightedRoom> roomPrefabs = new List<WeightedRoom>();
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
            IterateGen();
        }
    }

    void IterateGen()
    {
        if (iterations < maxIterations)
        {
            iterations++;
            
            List<GameObject> OldRoomEndPoints = new List<GameObject>(roomSpawnPoints);
            roomSpawnPoints = new List<GameObject>();
            foreach (GameObject spawnPoint in OldRoomEndPoints)
            {
                GenerateRoom(spawnPoint);
            }
        }
        else
        {
            //GenerateBossRoom();
        }
    }

    GameObject GetRandomRoomPrefab()
    {
        float totalWeight = 0;
        foreach (WeightedRoom room in roomPrefabs)
        {
            totalWeight += room.RandomWeight;
        }

        float randomValue = Random.Range(0, totalWeight);
        float weightSum = 0;
        foreach (WeightedRoom room in roomPrefabs)
        {
            weightSum += room.RandomWeight;
            if (randomValue <= weightSum)
            {
                return room.prefab;
            }
        }
        return null;
    }


    public void GenerateRoom(GameObject spawnPoint)
    {
        Debug.Log("Generating room...");

        // Get a random room prefab
        GameObject roomPrefab = GetRandomRoomPrefab();
        GameObject room = Instantiate(roomPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);

        // Destroy the spawn point
        Destroy(spawnPoint);

        Debug.Log("Room generated");
    }
}


