using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginScene : BaseScene
{
    public override void Clear()
    {
        Debug.Log("LoginClear");
    }

    void Start()
    {
        base.Init();
        SceneType = Define.Scene.Login;
        Managers.UI.ShowSceneUI<UI_Login>();
        Managers.Sound.Play("Bgms/Bgm_Lobby", Define.Sound.Bgm);

        if (Managers.Save.LoadPlayerData() == true)
            Managers.SceneEx.LoadScene(Define.Scene.Lobby);
    }
}
