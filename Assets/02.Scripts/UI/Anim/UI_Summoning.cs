using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Summoning : UI_Anim
{
    UI_Spawn parentUI;
    int spawnedHeroId;

    public void OnExit()
    {
        UI_SummonComplete _ui = Managers.UI.ShowPopupUI<UI_SummonComplete>();
        _ui.SetParentUI(parentUI, spawnedHeroId);
        Destroy(gameObject);
    }

    public void SetParentUI(UI_Spawn _ui, int _heroId)
    {
        parentUI = _ui;
        spawnedHeroId = _heroId;
    }
}
