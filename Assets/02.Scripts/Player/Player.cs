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


    void Start()
    {
        HeroComp = GetComponent<HeroComponent>();
        StageComp = GetComponent<StageComponent>();

        Managers.SetPlayer(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

