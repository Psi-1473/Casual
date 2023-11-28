using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SummonComplete : UI_Anim
{
    UI_Spawn parentUI;
    int spawnedHeroId;

    

    public void OnExit()
    {
        parentUI.EndSpawningEffect();
        Destroy(gameObject);
    }

    public void SetParentUI(UI_Spawn _ui, int _heroId)
    {
        parentUI = _ui;
        spawnedHeroId = _heroId;
    }
}
