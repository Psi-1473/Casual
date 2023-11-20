using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public abstract class Creature
{
    protected string creatureName;

    protected int id;
    protected int level;
    protected int maxHp;
    protected int maxMp;
    protected int attack;
    protected int defense;
    protected int speed;
    protected int role;
    protected int grade;

    public string CreatureName { get { return creatureName; } set { creatureName = value; } }

    public int Id { get { return id; } set { id = value; } }
    public int Level { get { return level; } set { level = value; } }
    public int MaxHp { get { return maxHp; } set { maxHp = value; } }
    public int MaxMp { get { return maxMp; } set { maxMp = value; } }
    public int Attack { get { return attack; } set { attack = value; } }
    public int Defense { get { return defense; } set { defense = value; } }
    public int Speed { get { return speed; } set { speed = value; } }
    public int Role { get { return role; } set { role = value; } }
    public int Grade { get { return grade; } set { grade = value; } }

    public abstract void SetNewCreatureInfo(int Id);

    
}
