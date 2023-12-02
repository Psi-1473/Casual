using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAnimEvent : MonoBehaviour
{
    public GameObject Target { get; set; }
    public AIController Owner { get; set; }
    public int Damage { get; set; }

    public void AttackEnd()
    {
        Owner.CreatureState = State.Return;
        //Debug.Log("Attack End");
        Destroy(gameObject);
    }

    public void AttackTarget()
    {
        Target.GetComponent<AIController>().OnDamaged(Owner.Stat.GetSkillDamage());
    }

    public void PlaySound()
    {
        Managers.Sound.Play($"Effects/Skill{Owner.Stat.Id}");
    }
}
