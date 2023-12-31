using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureAnimEvent : MonoBehaviour
{
    public void AttackEnd()
    {
        AIController controller = GetComponentInParent<AIController>();
        controller.CreatureState = Define.State.Return;
    }

    public void SkillExecute()
    {
        AIController controller = GetComponentInParent<AIController>();
        Skill skill= controller.gameObject.GetComponent<Skill>();
        skill.Execute(controller.Stat.Id);
    }

    public void PlayAttackSound()
    {
        AIController controller = GetComponentInParent<AIController>();
        if (controller.Stat.Role == 0 || controller.Stat.Role == 1)
            Managers.Sound.Play("Effects/Attack_Melee");
        else
            Managers.Sound.Play("Effects/Attack_Range");
    }
}
