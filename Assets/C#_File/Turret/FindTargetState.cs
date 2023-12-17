using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindTargetState : Turret_State
{
    public override void Update()
    {
        parent.GhostRotator.LookAt(parent.Target.position + parent.AinOffset);
        parent.Rotator.rotation = Quaternion.RotateTowards(parent.Rotator.rotation, parent.GhostRotator.rotation, Time.deltaTime * parent.RotationSpeed);
    }

    public override void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            parent.Target = null;
            parent.ChangeState(new Idle_State());
        }
    }
}
