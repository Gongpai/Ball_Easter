using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Turret_State
{
    protected Turret parent;

    public virtual void Enter(Turret parent)
    {
        this.parent = parent;
    }

    public virtual void Update()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual void OnTriggerEnter(Collider other)
    {

    }

    public virtual void OnTriggerExit(Collider other)
    {

    }
}
