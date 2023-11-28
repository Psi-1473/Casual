using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Anim : UI_Base
{
    private void Start()
    {
        
    }

    public override void Init()
    {
        
    }

    public void SetAnchoredPos(Vector2 _vec)
    {
        GetComponent<RectTransform>().anchoredPosition = _vec;
    }
    public void DestroyThis()
    {
        Destroy(gameObject);
    }
    
    


}
