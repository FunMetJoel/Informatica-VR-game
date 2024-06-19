using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMonsterFactory : MonsterFactory
{
    public GhostMonster ghostPrefab;
    public override IMonster CreateMonster()
    {
        GhostMonster ghost = Instantiate(ghostPrefab, position, rotation);
        ghost.Health = 100 * (1f + 0.1f * CurrentLevel);
        ghost.Speed = 5 * (1f + 0.1f * CurrentLevel);
        ghost.Damage = 10 * (1f + 0.1f * CurrentLevel);
        ghost.AttackSpeed = 1f * (1f + 0.1f * CurrentLevel);
        ghost.AttackRange = 1f * (1f + 0.1f * CurrentLevel);
        return ghost;
    }
}