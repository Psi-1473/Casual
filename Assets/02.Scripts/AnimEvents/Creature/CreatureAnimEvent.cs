using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureAnimEvent : MonoBehaviour
{
    public void AttackEnd()
    {
        AIController controller = GetComponentInParent<AIController>();
        controller.CreatureState = State.Return;
    }

    public void SkillExecute()
    {
        AIController controller = GetComponentInParent<AIController>();
        Skill skill= controller.gameObject.GetComponent<Skill>();
        skill.Execute(controller.Stat.Id);
    }
}
