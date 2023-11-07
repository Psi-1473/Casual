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
}
