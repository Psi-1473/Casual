using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatComponent : MonoBehaviour
{
    // AI Controller를 가진 객체가 게임 씬에 생성 되었을 때, Creature 클래스의 정보를 받아 초기화
    // InGame씬에서 객체끼리 전투를 할 때만 사용

    string heroName;
    int maxHp;
    int hp;
    int maxMp = 2;
    int mp = 0;
    int attack;
    int defense;
    int speed;
    int role;
    int grade;

    public string Name { get { return heroName; } set { heroName = value; } }
    public int Hp { get { return hp; } set { hp = value; gameObject.GetComponent<AIController>().OnStatChanged.Invoke(); } }
    public int Mp { get { return mp; } set { mp = value; gameObject.GetComponent<AIController>().OnStatChanged.Invoke(); } }
    public int Attack { get { return attack; } set { attack = value; } }
    public int Defense { get { return defense; } set { defense = value; } }
    public int Speed { get { return speed; } set { speed = value; } }
    public int Role { get { return role; } set { speed = role; } }
    public int Grade { get { return grade; } set { speed = grade; } }
    

    public float HpRatio => (float)hp / maxHp;
    public float MpRatio => (float)mp / maxMp;

    public void SetStatByHeroInfo(Hero _hero)
    {
        SetStat(_hero.CreatureName, _hero.MaxHp, _hero.MaxMp, _hero.Attack, _hero.Defense, 0, _hero.Role, _hero.Grade);
    }
    public void SetStatByEnemyInfo(int _enemyId)
    {
        EnemyInfo _info = Managers.Data.EnemyDict[_enemyId];
        SetStat(_info.name, _info.hp, _info.mp, _info.attack, _info.defense, 0, _info.role);
    }

    void SetStat(string _name, int _hp, int _mp, int _attack, int _defense, int _speed, int _role, int _grade = 0)
    {
        heroName = _name;
        maxHp = 50;
        maxMp = 2;
        hp = maxHp;
        mp = 0;
        attack = _attack;
        defense = _defense;
        speed = _speed;
        role = _role;
        grade = _grade;
    }
}
