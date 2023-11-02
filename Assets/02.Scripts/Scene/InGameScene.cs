using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameScene : BaseScene
{
    public override void Clear()
    {
        Debug.Log("LobbySceneClear");
    }

    // Start is called before the first frame update
    void Start()
    {
        base.Init();
        SceneType = Define.Scene.InGame;
        Managers.UI.ShowSceneUI<UI_InGame>();

        // 1. 싸울 영웅, 몬스터 정보 등록
        // 2. 해당 유닛들 소환하고 걸어나오는 애니메이션
        // 3. 끝나면 Battle Start 뜨면서 배틀 시작
        // 4. 배틀 알고리즘 짜고
        // 5. 전투가 종료되면 승리 or 패배에 따라 UI 띄우고 처리할 것들 처리
        // 6. Lobby Scene으로.
    }
}
