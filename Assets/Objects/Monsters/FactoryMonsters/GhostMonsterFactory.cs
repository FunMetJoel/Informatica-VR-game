using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMonsterFactory : MonsterFactory
{
    public GhostMonster ghostPrefab;
    public override IMonster CreateMonster()
    {
        GhostMonster ghost = Instantiate(ghostPrefab, position, rotation);
        ghost.Health = Mathf.RoundToInt(12 * (1f + 0.1f * CurrentLevel));
        ghost.Speed = 0.8f * (1f + 0.1f * CurrentLevel);
        ghost.Damage = 10 * (1f + 0.1f * CurrentLevel);
        ghost.AttackSpeed = 0.7f * (1f + 0.1f * CurrentLevel);
        ghost.AttackRange = 0.6f * (1f + 0.1f * CurrentLevel);
        return ghost;
    }
}