using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMonster
{
    void Attack();
    void Move();
    void Die();

    float Health { get; set; }
    float Speed { get; set; }
    float Damage { get; set; }
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
    public abstract IMonster CreateMonster();
}

public class GhostMonsterFactory : MonsterFactory
{
    public GhostMonster ghostPrefab;
    public override IMonster CreateMonster()
    {
        GhostMonster ghost = Instantiate(ghostPrefab);
        ghost.Health = 100 * (1f + 0.1f * CurrentLevel);
        ghost.Speed = 5 * (1f + 0.1f * CurrentLevel);
        ghost.Damage = 10 * (1f + 0.1f * CurrentLevel);
        ghost.AttackRange = 2 * (1f + 0.1f * CurrentLevel);
        return ghost;
    }
}


