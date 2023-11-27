using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_BuffBar : UI_Base
{
    const float horseHeight = 0.6f;

    Dictionary<Define.EBuff, UI_BuffImg> buffUi = new Dictionary<Define.EBuff, UI_BuffImg>();

    public GameObject Owner { get; set; }

    enum GameObjects
    {
        Content,
    }

    private void Awake()
    {
        Init();
    }
    private void Update()
    {
        
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
    }

    public void SetPos(bool isHorse = false)
    {
        if (Owner == null)
            return;
        float value = 1.9f;
        if (isHorse) value += horseHeight;

        gameObject.transform.position = new Vector3(Owner.transform.position.x, Owner.transform.position.y + value, Owner.transform.position.z);
    }

    public void SetBuffAction(BuffComponent _comp)
    {
        _comp.OnBuffAdded += AddOrRenewBuff;
        _comp.OnBuffRemoved += RemoveBuff;
    }

    public void AddOrRenewBuff(Define.EBuff _buffType)
    {
        // 딕셔너리에 이미 키값이 있을 경우 = 숫자만 갱신
        if(buffUi.ContainsKey(_buffType))
        {
            // 숫자 갱신
            return;
        }
        
        UI_BuffImg _ui = Managers.UI.MakeSubItem<UI_BuffImg>(Get<GameObject>((int)GameObjects.Content).transform);
        _ui.SetImg(_buffType);
        buffUi.Add(_buffType, _ui);
    }

    public void RemoveBuff(Define.EBuff _buffType)
    {
        UI_BuffImg _ui = buffUi[_buffType];
        buffUi.Remove(_buffType);
        Destroy(_ui.gameObject);
    }

}
