using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Stage : UI_Popup
{
    int chapter = 1;
    int clickedStage = 0; // 나중에 스크롤 뷰에 추가할 버튼을 클릭하면 1. clickedStage 갱신, 2. 클릭된 버튼과 clickedStage를 비교하여 같으면 스테이지 시작 다르면 다시 갱신
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
        // 1. 현재 챕터의 스테이지가 몇 개인지 받아오기
        // 2. for문 돌면서 스테이지 수만큼 UI_StageBtn 생성
        // 3. UI_StageBtn 생성하면서 chpater, stage(int, int) 세팅하기
        // 4. UI_StageBtn 누르면 해당 UI 들고 있던 chapter, stage 수에 따라 몬스터 배치 미리보기

        int chapterCount = Managers.Data.StageDicts[_chpater].Count;
        // Chapter 텍스트 이름 변경하기

        for(int i = 0; i < chapterCount; i++)
        {
            UI_StageBtn _ui = Managers.UI.MakeSubItem<UI_StageBtn>(Get<GameObject>((int)GameObjects.Content).transform);
            _ui.SetInfo(chapter, i + 1);

            Debug.Log($"팝업 띄우기 {i + 1}");
        }

        


        
    }
}
