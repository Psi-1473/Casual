using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Settings : UI_Popup
{
    SoundManager soundManager = null;
    Slider slider = null;
    enum Images
    {
        Img_Sound,
    }

    enum GameObjects
    {
        Slider_Sound,
        Btn_EndGame,
    }
    void Awake()
    {
        Init();
    }

    private void Start()
    {
        soundManager = Managers.Sound;
        slider = Get<GameObject>((int)GameObjects.Slider_Sound).GetComponent<Slider>();
        slider.value = soundManager.Volume;
    }

    private void Update()
    {
        float prevVolume = soundManager.Volume;

        if (prevVolume != slider.value)
        {
            soundManager.Volume = slider.value;
            Debug.Log($"{soundManager.Volume}");
        }
    }

    public override void Init()
    {
        base.Init();

        Bind<Image>(typeof(Images));
        Bind<GameObject>(typeof(GameObjects));

        BindEvent(Get<GameObject>((int)GameObjects.Btn_EndGame).gameObject, GameEnd);
    }

    void GameEnd(PointerEventData data)
    {
        Managers.Save.SavePlayerData(Managers.GetPlayer);
        Application.Quit();
    }

    
}
