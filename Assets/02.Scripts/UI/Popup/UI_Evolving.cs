using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Evolving : UI_Popup
{
    UI_Evolution baseUI;

    enum Texts
    {
        Text_AttackBefore,
        Text_AttackAfter,
        Text_DefenseBefore,
        Text_DefenseAfter,
    }
    enum GameObjects
    {
        EvolutionSucceed,
        Particle,
        Btn_Confirm,
    }

    void Awake()
    {
        Init();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        Bind<TextMeshProUGUI>(typeof(Texts));

        Get<GameObject>((int)GameObjects.EvolutionSucceed).SetActive(false);

        BindEvent(Get<GameObject>((int)GameObjects.Btn_Confirm), OnClickedConfirm);
        
        
    }

    public void PlayParticle()
    {
        Get<GameObject>((int)GameObjects.EvolutionSucceed).SetActive(true);
        Get<GameObject>((int)GameObjects.Particle).SetActive(false);
    }

    public void SetInfo(UI_Evolution _baseUI, int _bAttack, int _aAttack, int _bDefense, int _aDefense)
    {
        baseUI = _baseUI;
        Get<TextMeshProUGUI>((int)Texts.Text_AttackBefore).text = $"{_bAttack}";
        Get<TextMeshProUGUI>((int)Texts.Text_AttackAfter).text = $"{_aAttack}";
        Get<TextMeshProUGUI>((int)Texts.Text_DefenseBefore).text = $"{_bDefense}";
        Get<TextMeshProUGUI>((int)Texts.Text_DefenseAfter).text = $"{_aDefense}";
    }

    public void OnClickedConfirm(PointerEventData data)
    {
        if (baseUI == null)
            return;

        baseUI.Renew();
        ClosePopupUI();
    }
}
