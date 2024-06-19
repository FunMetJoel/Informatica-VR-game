using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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