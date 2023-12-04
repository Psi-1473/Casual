using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Stage : UI_Popup
{
    int chapter = 1;
    int clickedStage = 0; // 나중에 스크롤 뷰에 추가할 버튼을 클릭하면 1. clickedStage 갱신, 2. 클릭된 버튼과 clickedStage를 비교하여 같으면 스테이지 시작 다르면 다시 갱신
    int gold = 0;

    List<UI_StageBtn> stages = new List<UI_StageBtn>();

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

        BindEvent(GetButton((int)Buttons.Btn_NextChapter).gameObject, (data) => { SetByChpater(chapter + 1); });
        BindEvent(GetButton((int)Buttons.Btn_PrevChapter).gameObject, (data) => { SetByChpater(chapter - 1); });

    }

    public void SetByChpater(int _chapter)
    {
        if (_chapter < 1 || _chapter > 3)
            return;

        if (Managers.GetPlayer.StageComp.OpenedChapter < _chapter)
            return;

        Debug.Log($"Set Chapter : {_chapter}");
        chapter = _chapter;

        SetChapterImg(chapter);
        SetChapterText(chapter);
        SetSlot(chapter);
    }

    void SetChapterImg(int _chapter)
    {
        Sprite sprite = Managers.Resource.Load<Sprite>($"Images/ChapterImage/Chapter{_chapter}");
        GetImage((int)Images.Img_BackGround).sprite = sprite;
    }
    void SetChapterText(int _chapter)
    {
        string _text = "";
        switch(_chapter)
        {
            case 1:
                _text = "01. 마을 밖 산책로";
                break;
            case 2:
                _text = "02. 동굴 밖";
                break;
            case 3:
                _text = "03. 동굴 안";
                break;
        }

        Get<TextMeshProUGUI>((int)Texts.Text_Chapter).text = _text;
    }
    void SetSlot(int _chapter)
    {
        int chapterCount = Managers.Data.StageDicts[_chapter].Count;
        CreateAndSetSlot(chapterCount, _chapter);

        if (chapterCount < stages.Count)
            RemoveExcessSlots(chapterCount);
    }

    void CreateAndSetSlot(int _chapterCount, int _chapter)
    {
        for (int i = 0; i < _chapterCount; i++)
        {
            if ((stages.Count - 1) <= i)
            {
                UI_StageBtn _ui = Managers.UI.MakeSubItem<UI_StageBtn>(Get<GameObject>((int)GameObjects.Content).transform);
                stages.Add(_ui);
            }

            stages[i].SetInfo(_chapter, i + 1);
        }
    }
    void RemoveExcessSlots(int _chapterCount)
    {
        int number = stages.Count - _chapterCount;
        for (int i = 0; i < number; i++)
        {
            int idx = stages.Count - 1;
            Debug.Log($"Remove {idx}");
            UI_StageBtn _ui = stages[idx];
            Destroy(_ui.gameObject);
            stages.RemoveAt(idx);
        }
    }

}
