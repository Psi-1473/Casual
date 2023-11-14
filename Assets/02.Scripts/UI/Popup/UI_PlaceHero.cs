using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlaceHero : UI_Popup
{
    int chapter = 1;
    int stage = 1;
    List<GameObject> heroList = new List<GameObject>();
    enum GameObjects
    {
        Content,
        Formation1,
        Formation2,
        Formation3,
        Formation4,
        Formation5,
    }

    enum Buttons
    {
        Btn_Form1,
        Btn_Form2,
        Btn_Form3,
        Btn_Form4,
        Btn_Form5,
        Btn_Play,
    }

    void Awake()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        heroList.Add(null);
        heroList.Add(null);
        heroList.Add(null);
        heroList.Add(null);
        heroList.Add(null);
        heroList.Add(null);
        Managers.GetPlayer.HeroComp.Sort();
        Bind<GameObject>(typeof(GameObjects));
        Bind<Button>(typeof(Buttons));
        HeroComponent comp = Managers.GetPlayer.HeroComp;
        LoadHeros(comp.Heros);

        BindEvent(GetButton((int)Buttons.Btn_Form1).gameObject, (data) => { PlaceHero(null, 1); });
        BindEvent(GetButton((int)Buttons.Btn_Form2).gameObject, (data) => { PlaceHero(null, 2); });
        BindEvent(GetButton((int)Buttons.Btn_Form3).gameObject, (data) => { PlaceHero(null, 3); });
        BindEvent(GetButton((int)Buttons.Btn_Form4).gameObject, (data) => { PlaceHero(null, 4); });
        BindEvent(GetButton((int)Buttons.Btn_Form5).gameObject, (data) => { PlaceHero(null, 5); });
        BindEvent(GetButton((int)Buttons.Btn_Play).gameObject, (data) => { StartGame(); });

        PlaceHeroByPlayerInfo();
    }

    void StartGame()
    {
        // 1. æ¿ ¿Ãµø
        bool canStart = false;

        for (int i = 0; i < 6; i++)
        {
            if (heroList[i] != null)
            {
                canStart = true;
                break;
            }
        }

        if (canStart)
        {
            Managers.Battle.NowChapter = chapter;
            Managers.Battle.NowStage = stage;
            Managers.SceneEx.LoadScene(Define.Scene.InGame);
        }
    }

    void LoadHeros(List<Hero> _heros)
    {
        Managers.GetPlayer.HeroComp.Sort();
        for (int i = 0; i < _heros.Count; i++)
        {
            UI_HeroInfoPH _info = Managers.UI.MakeSubItem<UI_HeroInfoPH>(Get<GameObject>((int)GameObjects.Content).transform);
            _info.Init();
            _info.SetHeroInfo(_heros[i], this);
        }
    }

    void PlaceHeroByPlayerInfo()
    {
        for(int i = 1; i < 6; i++)
        {
            Hero hero = Managers.GetPlayer.HeroComp.HeroFormation[i];
            if (hero != null)
            {
                PlaceHero(hero, i);
            }
        }
    }

    public void PlaceHero(Hero _hero, int _place)
    {
        if(_hero == null)
        {
            Destroy(heroList[_place]);
            heroList[_place] = null;
            Managers.GetPlayer.HeroComp.SetOffHeroFormation(_place);
            return;
        }


        if (heroList[_place] != null)
            Destroy(heroList[_place]);

        GameObject obj = Managers.Resource.Instantiate($"Heros/{_hero.Id}");
        GameObjects enumObj = (GameObjects)_place;
        obj.transform.position = Get<GameObject>((int)enumObj).gameObject.transform.position;
        obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y - 0.5f, obj.transform.position.z);
        obj.transform.localScale = new Vector3(-1f, 1f, 1f);
        heroList[_place] = obj;
    }

    public void SetStage(int _chapter, int _stage)
    {
        chapter = _chapter;
        stage = _stage;
        Debug.Log($"Chapter : {chapter}, stage  {stage}");
    }

    public override void OnExit()
    {
        base.OnExit();

        for(int i = 0; i < 6; i++)
        {
            if (heroList[i] != null)
                Destroy(heroList[i]);
        }
    }
}
