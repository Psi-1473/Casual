using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Lobby : UI_Scene
{
    Player _player;
    enum Buttons
    {
        Btn_Evolution,
        Btn_Hero,
        Btn_Inventory,
        Btn_Spawn,
        Btn_Options,
        Btn_Play,
        Btn_Shop
    }

    enum Texts
    {
        Text_Gold,
        Text_Name,
        Text_SpawnGold,
        Text_Level
    }

    void Awake()
    {
        Init();
    }

    void Start()
    {
        SetInfo();
        SetGoldAndExpStone();
        _player.Inven.OnGoldChanged = null;
        _player.Inven.OnGoldChanged += SetGoldAndExpStone;
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));

        BindEvent(GetButton((int)Buttons.Btn_Hero).gameObject, OnHeroClicked);
        BindEvent(GetButton((int)Buttons.Btn_Evolution).gameObject, OnEvolutionClicked);
        BindEvent(GetButton((int)Buttons.Btn_Inventory).gameObject, OnInventoryClicked);
        BindEvent(GetButton((int)Buttons.Btn_Spawn).gameObject, OnSpawnClicked);
        BindEvent(GetButton((int)Buttons.Btn_Options).gameObject, OnOptionsClicked);
        BindEvent(GetButton((int)Buttons.Btn_Play).gameObject, OnPlayClicked);
        BindEvent(GetButton((int)Buttons.Btn_Shop).gameObject, OnShopClicked);
    }


    public void OnHeroClicked(PointerEventData data)
    {
        Managers.UI.ShowPopupUI<UI_Hero>();
    }
    public void OnEvolutionClicked(PointerEventData data)
    {
        UI_Evolution _ui = Managers.UI.ShowPopupUI<UI_Evolution>();
    }
    public void OnInventoryClicked(PointerEventData data)
    {
        UI_Inventory _ui = Managers.UI.ShowPopupUI<UI_Inventory>();
        _ui.SetItems(ItemType.Equip);
    }
    public void OnSpawnClicked(PointerEventData data)
    {
        Managers.UI.ShowPopupUI<UI_Spawn>();
    }
    public void OnOptionsClicked(PointerEventData data)
    {
        Managers.UI.ShowPopupUI<UI_Settings>();
        Debug.Log("Option");
    }
    public void OnPlayClicked(PointerEventData data)
    {
        UI_Stage _ui = Managers.UI.ShowPopupUI<UI_Stage>();
        _ui.SetByChpater(1);

    }
    public void OnShopClicked(PointerEventData data)
    {
        Managers.UI.ShowPopupUI<UI_Shop>();
        Managers.Save.SavePlayerData(Managers.GetPlayer);
        //Managers.Save.SaveHeroData();
    }

    void SetInfo()
    {
        Get<TextMeshProUGUI>((int)Texts.Text_Name).text = $"{Managers.GetPlayer.PlayerName}";
    }

    void SetGoldAndExpStone()
    {
        if (_player == null)
            _player = Managers.GetPlayer;

        Get<TextMeshProUGUI>((int)Texts.Text_Gold).text = $"{_player.Inven.Gold}";
        Get<TextMeshProUGUI>((int)Texts.Text_SpawnGold).text = $"{_player.Inven.ExpStone}";
    }
}
