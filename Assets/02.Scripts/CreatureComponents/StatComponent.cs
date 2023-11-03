using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatComponent : MonoBehaviour
{
    int maxHp;
    int maxMp;
    int hp;
    int mp;
    int attack;
    int defense;
    int speed;


    public int Hp { get { return hp; } set { hp = value; } }
    public int Mp { get { return mp; } set { mp = value; } }
    public int Attack { get { return attack; } set { attack = value; } }
    public int Defense { get { return defense; } set { defense = value; } }
    public int Speed { get { return speed; } set { speed = value; } }


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetHeroInfo(int _heroId)
    {
        HeroInfo _info = Managers.Data.HeroDict[_heroId];
        SetStat(_info.hp, _info.mp, _info.attack, _info.defense, 0);
    }

    public void SetEnemyInfo(int _enemyId)
    {
        EnemyInfo _info = Managers.Data.EnemyDict[_enemyId];
        SetStat(_info.hp, _info.mp, _info.attack, _info.defense, 0);
    }

    void SetStat(int _hp, int _mp, int _attack, int _defense, int _speed)
    {
        maxHp = _hp;
        maxMp = _mp;
        hp = _hp;
        mp = _mp;
        attack = _attack;
        defense = _defense;
        speed = _speed;
    }
}
