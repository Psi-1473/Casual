using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Settings : UI_Popup
{
    enum Images
    {
        Img_Sound,
    }

    enum GameObjects
    {
        Slider_Sound,
    }
    void Awake()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Image>(typeof(Images));
        Bind<GameObject>(typeof(GameObjects));
    }

    void GameEnd()
    {
        
    }

    
}
