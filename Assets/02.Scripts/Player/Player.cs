using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    public HeroComponent HeroComp { get; private set; }

    [SerializeField]
    public StageComponent StageComp { get; private set; }

    public Inventory Inven { get; private set; }


    void Start()
    {
        HeroComp = GetComponent<HeroComponent>();
        StageComp = GetComponent<StageComponent>();
        Inven = GetComponent<Inventory>();

        Inven.GainItem(Managers.Data.MiscDict[0], 1000);
        Inven.GainItem(Managers.Data.EquipDict[0]);
        Inven.GainItem(Managers.Data.EquipDict[1]);
        Inven.GainItem(Managers.Data.EquipDict[2]);
        Inven.GainItem(Managers.Data.EquipDict[3]);
        Inven.GainItem(Managers.Data.EquipDict[4]);
        Inven.GainItem(Managers.Data.EquipDict[5]);
        Inven.GainItem(Managers.Data.EquipDict[6]);
        Inven.GainItem(Managers.Data.EquipDict[7]);
        Inven.GainItem(Managers.Data.EquipDict[8]);
        Inven.GainItem(Managers.Data.EquipDict[9]);
        Inven.GainItem(Managers.Data.EquipDict[10]);
        Inven.GainItem(Managers.Data.EquipDict[11]);
        Debug.Log($"Player - Item 0 : {Managers.Data.MiscDict[0].itemType} ");
        Managers.SetPlayer(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

