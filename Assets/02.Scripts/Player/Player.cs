using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Player : MonoBehaviour
{
    string playerName = "Player";
    [SerializeField]
    public HeroComponent HeroComp { get; private set; }

    [SerializeField]
    public StageComponent StageComp { get; private set; }
    public Inventory Inven { get; private set; }
    public string PlayerName { get { return playerName; } }


    void Start()
    {
        HeroComp = GetComponent<HeroComponent>();
        StageComp = GetComponent<StageComponent>();
        Inven = GetComponent<Inventory>();

        Inven.GainItem(Managers.Data.MiscDict[0], 10);
        Inven.GainItem(Managers.Data.MiscDict[1], 10);
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


        string k = "00001011";
        int a = 0 << 8;

        for(int i = 0; i < k.Length; i++)
        {
            a = a << 1;
            int num = 0 << 7;
            num += k[i] - '0' << 0;

            a = a | num;
            Debug.Log($"num : {num}, a = {a}");

        }

        Debug.Log($"¿Ï : {a}");


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

