using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstScene : BaseScene
{
    public override void Clear()
    {
        Debug.Log("FirstClear");
    }

    void Start()
    {
        base.Init();
        SceneType = Define.Scene.First;

        if (Managers.Save.LoadPlayerData() == true)
            Managers.SceneEx.LoadScene(Define.Scene.Lobby);
        else
        {
            Managers.SceneEx.LoadScene(Define.Scene.Login);
            Managers.GetPlayer.StartFirst();
        }
    }

}
