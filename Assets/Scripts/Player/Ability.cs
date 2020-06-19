using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Derive to add abilities.  Attach to any game object with a PlayerController which will call the Process methods.
public class Ability : MonoBehaviour
{
    protected PlayerController _playerController;

    protected virtual void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }

    // Called every frame.  This of it as an EarlyUpdate() if one existed
    public virtual void EarlyProcessAbility()
    {

    }

    // Called every frame.  Think of this as Update()
    public virtual void ProcessAbility()
    {

    }

    // Called every frame.  Think of this as LateUpdate()
    public virtual void LateProcessAbility()
    {

    }

}
