using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMonsterFactory : MonsterFactory
{
    public SkeletonMonster skeletonPrefab;
    public override IMonster CreateMonster()
    {
        SkeletonMonster skeleton = Instantiate(skeletonPrefab, position, rotation);
        skeleton.Health = Mathf.RoundToInt( 40 * (1f + 0.1f * CurrentLevel));
        skeleton.Speed = 1.2f * (1f + 0.1f * CurrentLevel);
        skeleton.Damage = 12 * (1f + 0.1f * CurrentLevel);
        skeleton.AttackSpeed = 0.9f * (1f + 0.1f * CurrentLevel);
        skeleton.AttackRange = 1.2f * (1f + 0.1f * CurrentLevel);
        return skeleton;
    }
}