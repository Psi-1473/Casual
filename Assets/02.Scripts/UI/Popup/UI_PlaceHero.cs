using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlaceHero : UI_Popup
{
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
        Bind<GameObject>(typeof(GameObjects));
        Bind<Button>(typeof(Buttons));
        HeroComponent comp = Managers.GetPlayer.HeroComp;
        LoadHeros(comp.UniqueHero, comp.RareHero, comp.NormalHero);

        BindEvent(GetButton((int)Buttons.Btn_Form1).gameObject, (data) => { PlaceHero(-1, 1); });
        BindEvent(GetButton((int)Buttons.Btn_Form2).gameObject, (data) => { PlaceHero(-1, 2); });
        BindEvent(GetButton((int)Buttons.Btn_Form3).gameObject, (data) => { PlaceHero(-1, 3); });
        BindEvent(GetButton((int)Buttons.Btn_Form4).gameObject, (data) => { PlaceHero(-1, 4); });
        BindEvent(GetButton((int)Buttons.Btn_Form5).gameObject, (data) => { PlaceHero(-1, 5); });

        PlaceHeroByPlayerInfo();
    }

    void LoadHeros(List<int> unique, List<int> rare, List<int> normal)
    {
        for (int i = 0; i < unique.Count; i++)
        {
            for (int j = 0; j < Managers.GetPlayer.HeroComp.HeroCount[unique[i]]; j++)
            {
                UI_HeroInfoPH _info = Managers.UI.MakeSubItem<UI_HeroInfoPH>(Get<GameObject>((int)GameObjects.Content).transform);
                _info.Init();
                _info.SetHeroInfo(unique[i], this);
            }
        }

        for (int i = 0; i < rare.Count; i++)
        {
            for (int j = 0; j < Managers.GetPlayer.HeroComp.HeroCount[rare[i]]; j++)
            {
                UI_HeroInfoPH _info = Managers.UI.MakeSubItem<UI_HeroInfoPH>(Get<GameObject>((int)GameObjects.Content).transform);
                _info.Init();
                _info.SetHeroInfo(rare[i], this);
            }
        }

        for (int i = 0; i < normal.Count; i++)
        {
            for (int j = 0; j < Managers.GetPlayer.HeroComp.HeroCount[normal[i]]; j++)
            {
                UI_HeroInfoPH _info = Managers.UI.MakeSubItem<UI_HeroInfoPH>(Get<GameObject>((int)GameObjects.Content).transform);
                _info.Init();
                _info.SetHeroInfo(normal[i], this);
            }
        }
    }


    void PlaceHeroByPlayerInfo()
    {
        for(int i = 1; i < 6; i++)
        {
            int hId = Managers.GetPlayer.HeroComp.HeroFormation[i];
            if (hId != -1)
                PlaceHero(hId, i);
        }
    }

    public void PlaceHero(int _heroId, int _place)
    {
        if(_heroId == -1)
        {
            Destroy(heroList[_place]);
            heroList[_place] = null;
            Managers.GetPlayer.HeroComp.SetOffHeroFormation(_place);
            return;
        }

        if (heroList[_place] != null)
            Destroy(heroList[_place]);

        GameObject obj = Managers.Resource.Instantiate($"Heros/{_heroId}");
        GameObjects enumObj = (GameObjects)_place;
        obj.transform.position = Get<GameObject>((int)enumObj).gameObject.transform.position;
        obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y - 0.5f, obj.transform.position.z);
        obj.transform.localScale = new Vector3(-1f, 1f, 1f);
        heroList[_place] = obj;
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
