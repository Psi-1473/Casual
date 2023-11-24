using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_BuffBar : UI_Base
{
    const float horseHeight = 0.6f;

    public GameObject Owner { get; set; }


    private void Update()
    {
        
    }

    public override void Init()
    {
       

    }

    public void SetPos(bool isHorse = false)
    {
        if (Owner == null)
            return;
        float value = 1.9f;
        if (isHorse) value += horseHeight;

        gameObject.transform.position = new Vector3(Owner.transform.position.x, Owner.transform.position.y + value, Owner.transform.position.z);

    }

}
