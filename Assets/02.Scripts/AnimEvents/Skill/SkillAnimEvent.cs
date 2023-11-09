using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAnimEvent : MonoBehaviour
{
    public GameObject Target { get; set; }
    public AIController Owner { get; set; }
    public int Damage { get; set; } = 2;

    public void AttackEnd()
    {
        Owner.CreatureState = State.Return;
        Debug.Log("Attack End");
        Destroy(gameObject);
    }

    public void AttackTarget()
    {
        Target.GetComponent<AIController>().OnDamaged(Damage);
    }
}