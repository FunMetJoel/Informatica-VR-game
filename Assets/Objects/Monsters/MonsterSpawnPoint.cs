using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnPoint : MonoBehaviour
{
    public List<MonsterFactory> monsterFactories;
    void Start()
    {
        Debug.Log("Spawning random monster");

        MonsterFactory monsterFactory = monsterFactories[0];
        monsterFactory.CurrentLevel = NewRoomGenerator.Instance.iterations;
        monsterFactory.position = transform.position;
        monsterFactory.rotation = transform.rotation;
        monsterFactory.CreateMonster();

        Debug.Log("Monster spawned");
    }

    private MonsterFactory GetRandomMonsterFactory()
    {
        return monsterFactories[Random.Range(0, monsterFactories.Count)];
    }
}
