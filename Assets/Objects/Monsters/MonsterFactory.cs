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





