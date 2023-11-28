using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

enum SpawnType
{
    Spawn_Normal,
    Spawn_Rare,
}
public class UI_Spawn : UI_Popup
{
    SpawnType spawnType;
    Item normalTicket;
    Item rareTicket;


    enum Buttons
    {
        Btn_NormalSpawn,
        Btn_RareSpawn,
        Btn_Spawn,
    }

    enum Texts
    {
        Text_SpawnTitle,
        Text_SpawnEx,
        Text_SpawnPercent,
        Text_SpawnStone,
    }

    enum Images
    {
        Img_SpawnStone,
    }

    enum GameObjects
    {
        ClickerUI,
    }

    void Awake()
    {
        Init();

    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<GameObject>(typeof(GameObjects));

        BindEvent(GetButton((int)Buttons.Btn_NormalSpawn).gameObject, (data) => { SetSpawnType(SpawnType.Spawn_Normal); });
        BindEvent(GetButton((int)Buttons.Btn_RareSpawn).gameObject, (data) => { SetSpawnType(SpawnType.Spawn_Rare); });
        BindEvent(GetButton((int)Buttons.Btn_Spawn).gameObject, OnClickedSpawn);

        SetTickets();
        SetSpawnType(SpawnType.Spawn_Normal);
        
    }

    public void StartSpawningEffect(int _heroId)
    {
        if (_heroId == -1) return;

        Get<GameObject>((int)GameObjects.ClickerUI).SetActive(false);
        UI_Summoning _ui = Managers.UI.MakeAnimUI<UI_Summoning>(transform);
        _ui.SetParentUI(this, _heroId);

    }

    public void EndSpawningEffect()
    {
        Get<GameObject>((int)GameObjects.ClickerUI).SetActive(true);
    }

    void OnClickedSpawn(PointerEventData data)
    {
        int _heroId = 0;
        if(spawnType == SpawnType.Spawn_Normal)
            _heroId = Managers.Sunmmon.NormalSummon(ref normalTicket);
        else if(spawnType == SpawnType.Spawn_Rare)
            _heroId = Managers.Sunmmon.RareSummon(ref rareTicket);

        SetTickets();
        StartSpawningEffect(_heroId);

    }

    void SetSpawnType(SpawnType _type)
    {
        spawnType = _type;
        if (_type == SpawnType.Spawn_Normal)
            SetTextNormal();

        if (_type == SpawnType.Spawn_Rare)
            SetTextRare();

        SetTicketInfo(spawnType);
    }

    void SetTextRare()
    {
        Get<TextMeshProUGUI>((int)Texts.Text_SpawnTitle).text = "레어 뽑기";
        Get<TextMeshProUGUI>((int)Texts.Text_SpawnEx).text = "레어 등급, 유니크 등급의 영웅 중 랜덤으로 하나가 나옵니다.";
        Get<TextMeshProUGUI>((int)Texts.Text_SpawnPercent).text = "레어 등급 : 80%" + "\n" + "유니크 등급 : 20%";
    }
    void SetTextNormal()
    {
        Get<TextMeshProUGUI>((int)Texts.Text_SpawnTitle).text = "일반 뽑기";
        Get<TextMeshProUGUI>((int)Texts.Text_SpawnEx).text = "노말 등급, 레어 등급의 영웅 중 랜덤으로 하나가 나옵니다.";
        Get<TextMeshProUGUI>((int)Texts.Text_SpawnPercent).text = "노말 등급 : 75%" + "\n" + "유니크 등급 : 25%";
    }

    void SetTicketInfo(SpawnType _type)
    {
        Sprite sprite = null;
        switch(_type)
        {
            case SpawnType.Spawn_Normal:
                sprite = Managers.Resource.Load<Sprite>("Images/Items/1/0");
                Get<TextMeshProUGUI>((int)Texts.Text_SpawnStone).text = (normalTicket == null) ? "0" : $"{normalTicket.Number}";
                break;
            case SpawnType.Spawn_Rare:
                sprite = Managers.Resource.Load<Sprite>("Images/Items/1/1");
                Get<TextMeshProUGUI>((int)Texts.Text_SpawnStone).text = (rareTicket == null) ? "0" :$"{rareTicket.Number}";
                break;
        }

        Get<Image>((int)Images.Img_SpawnStone).sprite = sprite;
    }

    void SetTickets()
    {
        normalTicket = Managers.GetPlayer.Inven.ItemById(ItemType.Misc, 0);
        rareTicket = Managers.GetPlayer.Inven.ItemById(ItemType.Misc, 1);

        SetTicketInfo(spawnType);
    }

    

}
