using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatComponent : MonoBehaviour
{
    // AI Controller를 가진 객체가 게임 씬에 생성 되었을 때, Creature 클래스의 정보를 받아 초기화
    // InGame씬에서 객체끼리 전투를 할 때만 사용
    int id;
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
    int skillDamage;

    public int Id { get { return id; } set { id = value; } }
    public string Name { get { return heroName; } set { heroName = value; } }
    public int MaxHp { get { return maxHp; } }
    public int Hp { get { return hp; } set { hp = value; gameObject.GetComponent<AIController>().OnStatChanged.Invoke(); } }
    public int Mp { get { return mp; } set { mp = value; gameObject.GetComponent<AIController>().OnStatChanged.Invoke(); } }
    public int Attack { get { return attack; } set { attack = value; } }
    public int Defense { get { return defense; } set { defense = value; } }
    public int Speed { get { return speed; } set { speed = value; } }
    public int Role { get { return role; } set { speed = value; } }
    public int Grade { get { return grade; } set { speed = value; } }
    

    public float HpRatio => (float)hp / maxHp;
    public float MpRatio => (float)mp / maxMp;

    public int GetSkillDamage()
    {
        // 20의 10퍼 = 2
        // 20 * 0.1
        float percent = skillDamage * 0.01f;
        int addDamage = (int)(attack * percent);
        int damage = attack + addDamage;
        //Debug.Log($" Skill Damage ! : {damage} (Original Damage : {attack}, Increased : {addDamage}) (Percentage : {percent}, {skillDamage} * 0.01f)");
        return damage;
    }

    public float GetHealPercentage()
    {
        return (skillDamage * 0.01f);
    }


    public void SetStatByHeroInfo(Hero _hero)
    {
        id = _hero.Id;
        SetStat(_hero.CreatureName, _hero.MaxHp, _hero.MaxMp, _hero.Attack, _hero.Defense, 0, _hero.Role, _hero.Grade, _hero.SkillDamage);
    }
    public void SetStatByEnemyInfo(int _enemyId)
    {
        EnemyInfo _info = Managers.Data.EnemyDict[_enemyId];
        SetStat(_info.name, _info.hp, _info.mp, _info.attack, _info.defense, 0, _info.role);
    }

    void SetStat(string _name, int _hp, int _mp, int _attack, int _defense, int _speed, int _role, int _grade = 0, int _skillDamage = 1)
    {
        Debug.Log($"Set Stat : {_attack}");
        heroName = _name;
        maxHp = _hp;
        maxMp = 2;
        hp = maxHp;
        mp = 0;
        attack = _attack;
        defense = _defense;
        speed = _speed;
        role = _role;
        grade = _grade;
        skillDamage = _skillDamage;
    }
}
