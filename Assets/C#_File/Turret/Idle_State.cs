using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Idle_State : Turret_State
{
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.tag == "Player")
        {
            parent.Target = other.transform;
            parent.ChangeState(new FindTargetState());
        }
    }
}
