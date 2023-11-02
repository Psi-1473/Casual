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

        // 1. �ο� ����, ���� ���� ���
        // 2. �ش� ���ֵ� ��ȯ�ϰ� �ɾ���� �ִϸ��̼�
        // 3. ������ Battle Start �߸鼭 ��Ʋ ����
        // 4. ��Ʋ �˰��� ¥��
        // 5. ������ ����Ǹ� �¸� or �й迡 ���� UI ���� ó���� �͵� ó��
        // 6. Lobby Scene����.
    }
}
