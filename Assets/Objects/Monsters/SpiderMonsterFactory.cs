using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMonsterFactory : MonsterFactory
{
    public SpiderMonster spiderPrefab;
    public override IMonster CreateMonster()
    {
        SpiderMonster spider = Instantiate(spiderPrefab, position, rotation);
        spider.Health = Mathf.RoundToInt(6f * (1f + 0.1f * CurrentLevel));
        spider.Speed = 1.3f * (1f + 0.1f * CurrentLevel);
        spider.Damage = 5 * (1f + 0.1f * CurrentLevel);
        spider.AttackSpeed = 1 * (1f + 0.1f * CurrentLevel);
        spider.AttackRange = 0.8f * (1f + 0.1f * CurrentLevel);
        return spider;
    }
}
