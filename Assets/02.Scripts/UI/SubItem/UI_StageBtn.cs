using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_StageBtn : UI_Base
{
    int chapter = 0;
    int stage = 0;
    bool opened = false;
    
    enum Texts
    {
        Text_Number,
    }

    enum Images
    {
        Img_Lock,
    }
    void Awake()
    {
        Init();
    }

    public override void Init()
    {
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));

        BindEvent(gameObject,  OpenFormationUI);
    }

    public void SetInfo(int _chapter, int _stage)
    {
        chapter = _chapter;
        stage = _stage;

        Get<TextMeshProUGUI>((int)Texts.Text_Number).text = $"{stage}";

        if (Managers.GetPlayer.StageComp.OpenedChapter > chapter)
            LockStage(false, out opened);
        else if (Managers.GetPlayer.StageComp.OpenedChapter < chapter)
            LockStage(true, out opened);
        else if (Managers.GetPlayer.StageComp.OpenedChapter == chapter && Managers.GetPlayer.StageComp.OpenedStage >= stage)
            LockStage(false, out opened);
        else
            LockStage(true, out opened);
    }

    public void LockStage(bool _value, out bool _opened)
    {
        _opened = !_value;
        Get<Image>((int)Images.Img_Lock).gameObject.SetActive(_value);
    }

    public void OpenFormationUI(PointerEventData data)
    {
        if (opened == false)
            return;

        UI_PlaceHero _ui = Managers.UI.ShowPopupUI<UI_PlaceHero>();
        _ui.SetStage(chapter, stage);
        // ui ¼¼ÆÃ

    }



}
