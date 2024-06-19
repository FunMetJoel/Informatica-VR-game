using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMonster
{
    void Attack();
    void Move();
    void Die();

    void Setup();

    int Health { get; set; }
    float Speed { get; set; }
    float Damage { get; set; }
    float AttackSpeed { get; set; }
    float AttackRange { get; set; }
}

public enum MonsterType
{
    Ghost,
    Spider,
    Skeleton
}

public abstract class MonsterFactory : MonoBehaviour
{
    public float CurrentLevel = 1;
    public Vector3 position;
    public Quaternion rotation;

    public abstract IMonster CreateMonster();
}



public class SpiderMonsterFactory : MonsterFactory
{
    public SpiderMonster spiderPrefab;
    public override IMonster CreateMonster()
    {
        SpiderMonster spider = Instantiate(spiderPrefab);
        spider.Health = Mathf.RoundToInt( 100 * (1f + 0.1f * CurrentLevel));
        spider.Speed = 5 * (1f + 0.1f * CurrentLevel);
        spider.Damage = 10 * (1f + 0.1f * CurrentLevel);
        spider.AttackSpeed = 1.5f * (1f + 0.1f * CurrentLevel);
        spider.AttackRange = 2 * (1f + 0.1f * CurrentLevel);
        return spider;
    }
}


