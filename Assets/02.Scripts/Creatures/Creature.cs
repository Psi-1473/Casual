using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    Animator anim;
    public Transform Destination { get; set; }
    public bool CanMove { get; set; } = false;

    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        Debug.Log("AWAKE CREATURE");
    }

    protected virtual void FixedUpdate()
    {
        Move();
    }

    public void MoveTo(Transform _trans)
    {
        Destination = _trans;
        CanMove = true;
    }

    void Move()
    {
        if (CanMove == false) return;

        if (Vector3.Distance(Destination.position, transform.position) >= 0.7f)
        {
            Vector3 dir = Destination.position - transform.position;
            transform.position += dir * Time.deltaTime;
            anim.SetBool("Move", true);
        }
        else if (Vector3.Distance(Destination.position, transform.position) <= 0.7f)
        {
            CanMove = false;
            anim.SetBool("Move", false);
        }
    }
}
