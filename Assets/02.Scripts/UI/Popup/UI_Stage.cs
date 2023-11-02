using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Stage : UI_Popup
{
    int chapter = 1;
    int clickedStage = 0; // ���߿� ��ũ�� �信 �߰��� ��ư�� Ŭ���ϸ� 1. clickedStage ����, 2. Ŭ���� ��ư�� clickedStage�� ���Ͽ� ������ �������� ���� �ٸ��� �ٽ� ����
    enum GameObjects
    {
        Content,
    }
    enum Buttons
    {
        Btn_NextChapter,
        Btn_PrevChapter,
    }

    enum Texts
    {
        Text_Chapter
    }

    enum Images
    {
        Img_BackGround,
    }

    void Awake()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<GameObject>(typeof(GameObjects));

        BindEvent(GetButton((int)Buttons.Btn_NextChapter).gameObject, (data) => { Debug.Log("Next Button"); });
        BindEvent(GetButton((int)Buttons.Btn_PrevChapter).gameObject, (data) => { Debug.Log("Prev Button"); });

    }

    public void SetByChpater(int _chpater)
    {
        chapter = _chpater;
        // 1. ���� é���� ���������� �� ������ �޾ƿ���
        // 2. for�� ���鼭 �������� ����ŭ UI_StageBtn ����
        // 3. UI_StageBtn �����ϸ鼭 chpater, stage(int, int) �����ϱ�
        // 4. UI_StageBtn ������ �ش� UI ��� �ִ� chapter, stage ���� ���� ���� ��ġ �̸�����

        int chapterCount = Managers.Data.StageDicts[_chpater].Count;
        // Chapter �ؽ�Ʈ �̸� �����ϱ�

        for(int i = 0; i < chapterCount; i++)
        {
            UI_StageBtn _ui = Managers.UI.MakeSubItem<UI_StageBtn>(Get<GameObject>((int)GameObjects.Content).transform);
            _ui.SetInfo(chapter, i + 1);

            Debug.Log($"�˾� ���� {i + 1}");
        }

        


        
    }
}
