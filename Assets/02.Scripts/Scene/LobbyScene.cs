using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : BaseScene
{
    public override void Clear()
    {
        Debug.Log("LobbySceneClear");
    }

    // Start is called before the first frame update
    void Start()
    {
        base.Init();
        SceneType = Define.Scene.Lobby;
        Managers.UI.ShowSceneUI<UI_Lobby>();
        Managers.Sound.Play("Bgms/Bgm_Lobby", Define.Sound.Bgm);

    }
}
