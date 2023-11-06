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
    int role;


    public int Hp { get { return hp; } set { hp = value; gameObject.GetComponent<Creature>().OnStatChanged.Invoke(); } }
    public int Mp { get { return mp; } set { mp = value; gameObject.GetComponent<Creature>().OnStatChanged.Invoke(); } }
    public int Attack { get { return attack; } set { attack = value; } }
    public int Defense { get { return defense; } set { defense = value; } }
    public int Speed { get { return speed; } set { speed = value; } }
    public int Role { get { return role; } set { speed = role; } }

    public float HpRatio => (float)hp / maxHp;
    public float MpRatio => (float)mp / maxMp;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetHeroInfo(int _heroId)
    {
        HeroInfo _info = Managers.Data.HeroDict[_heroId];
        SetStat(_info.hp, _info.mp, _info.attack, _info.defense, 0, _info.role);
    }

    public void SetEnemyInfo(int _enemyId)
    {
        EnemyInfo _info = Managers.Data.EnemyDict[_enemyId];
        SetStat(_info.hp, _info.mp, _info.attack, _info.defense, 0, _info.role);
    }

    void SetStat(int _hp, int _mp, int _attack, int _defense, int _speed, int _role)
    {
        maxHp = 50;
        maxMp = 50;
        hp = maxHp;
        mp = maxMp;
        attack = _attack;
        defense = _defense;
        speed = _speed;
        role = _role;
    }
}
