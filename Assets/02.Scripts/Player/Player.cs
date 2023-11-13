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
        Debug.Log($"Player - Item 0 : {Managers.Data.MiscDict[0].itemType} ");
        Managers.SetPlayer(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

