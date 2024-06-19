using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnPoint : MonoBehaviour
{
    public List<MonsterFactory> monsterFactories;
    void Start()
    {
        Debug.Log("Spawning random monster");

        MonsterFactory monsterFactory = GetRandomMonsterFactory();
        monsterFactory.CurrentLevel = NewRoomGenerator.Instance.iterations;
        monsterFactory.position = transform.position;
        Debug.Log("Monster position: " + monsterFactory.position);
        monsterFactory.rotation = transform.rotation;
        IMonster monster = monsterFactory.CreateMonster();
        monster.Setup();

        Debug.Log("Monster spawned");
    }

    private MonsterFactory GetRandomMonsterFactory()
    {
        return monsterFactories[Random.Range(0, monsterFactories.Count)];
    }
}
