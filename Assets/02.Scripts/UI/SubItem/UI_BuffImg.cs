using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BuffImg : UI_Base
{
    public override void Init()
    {
        throw new System.NotImplementedException();
    }

    public void SetImg(Define.EBuff _buffType)
    {
        string imgName = System.Enum.GetName(typeof(Define.EBuff), _buffType);
        GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>($"Images/BuffImages/{imgName}");

    }
}
