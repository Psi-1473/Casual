using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Define
{

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
        Lobby,
        InGame,
    }

    public enum HeroGrade
    {
        NORMAL,
        RARE,
        UNIQUE,
    }
}
