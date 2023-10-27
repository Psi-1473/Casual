using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public HeroComponent HeroComp { get; private set; }

    void Start()
    {
        HeroComp = GetComponent<HeroComponent>();

        Managers.SetPlayer(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
