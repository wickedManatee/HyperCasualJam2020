using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class KeyboardHorizontalMoveAbility : Ability
{
    public float Speed = 5f;

    protected float _horizontal;

    public override void EarlyProcessAbility()
    {
        base.EarlyProcessAbility();
        _horizontal = Input.GetAxis("Horizontal");
    }
    public override void ProcessAbility()
    {
        base.ProcessAbility();
        _playerController.CurrentDirection = new Vector3(_horizontal * Speed, 0f, 0f);
    }

}
