using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Creature
{
    protected override void Awake()
    {
        base.Awake();
        SetId();
        stat.SetEnemyInfo(Id);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    void SetId()
    {
        string sId = gameObject.name;
        int idx = sId.IndexOf('(');
        sId = sId.Substring(0, idx);
        Id = int.Parse(sId);
        Debug.Log($"Enemy id : {Id}");
    }

    public void AttackEnd()
    {
        state = State.Return;
    }
}
