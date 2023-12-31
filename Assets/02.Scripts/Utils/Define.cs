﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Define
{
    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }
    public enum UIEvent
    {
        Click,
        Drag,
        Enter,
        Exit,
    }

    public enum MouseEvent
    {
        Press,
        Click,
    }

    public enum CameraMode
    {
        QuarterView,
    }

    public enum Scene
    {
        None,
        First,
        Login,
        Lobby,
        InGame,
    }

    public enum HeroGrade
    {
        NORMAL,
        RARE,
        UNIQUE,
    }

    public enum EBuff
    {
        None = 0 << 0,
        Freeze = 1 << 0,
        Burn = 1 << 1,
        Bleed = 1 << 2,
        Stun = 1 << 3,
        DefenseUp = 1 << 4,
    }

    public enum State
    {
        Setting,
        Idle,
        TurnStart,
        MoveToAttack,
        Attack,
        Skill,
        Return,
    }
    public enum CreatureType
    {
        CREATURE_HERO,
        CREATURE_ENEMY
    }

    public enum Role
    {
        Tank = 0,
        Melee = 1,
        Range = 2,
        Sup = 3,
    }
}
