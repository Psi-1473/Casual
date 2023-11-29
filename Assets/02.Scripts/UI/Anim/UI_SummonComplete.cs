using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SummonComplete : UI_Popup
{
    UI_Spawn parentUI;
    int spawnedHeroId;

    bool canClose = false;
    
    enum Images
    {
        Img_Hero,
    }

    enum Texts
    {
        Text_Name,
        Text_Grade,
    }

    enum GameObjects
    {
        ExitPanel,
    }

    private void Awake()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<GameObject>(typeof(GameObjects));
        
        BindEvent(Get<GameObject>((int)GameObjects.ExitPanel).gameObject, (data) => { OnExit(); });
    }

    public override void OnExit()
    {
        if (!canClose)
            return;

        parentUI.EndSpawningEffect();
        ClosePopupUI();
    }

    public void SetParentUI(UI_Spawn _ui, int _heroId)
    {
        parentUI = _ui;
        spawnedHeroId = _heroId;

        SetHeroInfo(_heroId);
    }

    public void CanClose()
    {
        canClose = true;
    }

    void SetHeroInfo(int _heroId)
    {
        HeroInfo info = Managers.Data.HeroDict[_heroId];
        Sprite sprite = Managers.Resource.Load<Sprite>($"Images/Heros/{_heroId}");
        string grade = "";

        if (info.grade == 0) grade = "노 말";
        if (info.grade == 3) grade = "레 어";
        if (info.grade == 6) grade = "유니크";

        Get<TextMeshProUGUI>((int)Texts.Text_Name).text = info.name;
        Get<TextMeshProUGUI>((int)Texts.Text_Grade).text = grade;
        Get<Image>((int)Images.Img_Hero).sprite = sprite;
    }
}
