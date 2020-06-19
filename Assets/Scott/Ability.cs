using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Derive to add abilities called the the PlayerController
public class Ability : MonoBehaviour
{
    protected PlayerController _playerController;

    protected virtual void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }

    // An early update
    public virtual void EarlyProcessAbility()
    {

    }

    // An early update
    public virtual void ProcessAbility()
    {

    }

    // A Late Update
    public virtual void LateProcessAbility()
    {

    }

}
