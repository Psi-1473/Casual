using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureAnimEvent : MonoBehaviour
{
    public void AttackEnd()
    {
        Creature creature = GetComponentInParent<Creature>();
        creature.CreatureState = State.Return;
    }
}
