using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkillUse : UI_Popup
{
    enum Texts
    {
        Text_SkillName,
    }

    enum Images
    {
        Img_Hero,
    }

    enum GameObjects
    {
        SkillUse,
    }



    private void Awake()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(GameObjects));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));

        GameObject obj = Get<GameObject>((int)GameObjects.SkillUse);

        SetStartPos();

    }

    public void SetInfo(int _heroId)
    {
        Sprite sprite = Managers.Resource.Load<Sprite>($"Images/Heros/{_heroId}");
        Get<TextMeshProUGUI>((int)Texts.Text_SkillName).text = Managers.Data.SkillDict[_heroId].name;
        Get<Image>((int)Images.Img_Hero).sprite = sprite;
    }

    public override void OnExit()
    {
        base.OnExit();
        ClosePopupUI();
    }

    void SetStartPos()
    {
        int width = Screen.width;
        int height = Screen.height;
        RectTransform rectTrans = Get<GameObject>((int)GameObjects.SkillUse).GetComponent<RectTransform>();

        float left = rectTrans.offsetMin.x;
        float bottom = rectTrans.offsetMin.y;
        float right = rectTrans.offsetMax.x;
        float top = rectTrans.offsetMax.y;

        Debug.Log(rectTrans.rect.width);
        float widthPos = (width / 2) + (rectTrans.rect.width / 2);
        rectTrans.offsetMin = new Vector2(-widthPos, bottom);
        rectTrans.offsetMax = new Vector2(-widthPos, top);

        // 둘 다 음수 - 왼쪽
        // 둘 다 양수 - 오른쪽

    }
}
