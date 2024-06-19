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
        ghost.AttackSpeed = 1f * (1f + 0.1f * CurrentLevel);
        ghost.AttackRange = 1f * (1f + 0.1f * CurrentLevel);
        return ghost;
    }
}

public class SpiderMonsterFactory : MonsterFactory
{
    public SpiderMonster spiderPrefab;
    public override IMonster CreateMonster()
    {
        SpiderMonster spider = Instantiate(spiderPrefab);
        spider.Health = 100 * (1f + 0.1f * CurrentLevel);
        spider.Speed = 5 * (1f + 0.1f * CurrentLevel);
        spider.Damage = 10 * (1f + 0.1f * CurrentLevel);
        spider.AttackSpeed = 1.5f * (1f + 0.1f * CurrentLevel);
        spider.AttackRange = 2 * (1f + 0.1f * CurrentLevel);
        return spider;
    }
}

public class SkeletonMonsterFactory : MonsterFactory
{
    public SkeletonMonster skeletonPrefab;
    public override IMonster CreateMonster()
    {
        SkeletonMonster skeleton = Instantiate(skeletonPrefab);
        skeleton.Health = 100 * (1f + 0.1f * CurrentLevel);
        skeleton.Speed = 5 * (1f + 0.1f * CurrentLevel);
        skeleton.Damage = 10 * (1f + 0.1f * CurrentLevel);
        skeleton.AttackSpeed = 1f * (1f + 0.1f * CurrentLevel);
        skeleton.AttackRange = 1.5f * (1f + 0.1f * CurrentLevel);
        return skeleton;
    }
}
