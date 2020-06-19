using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

// A keyboard implementation of movement
public class KeyboardHorizontalMoveAbility : Ability
{
    public float Speed = 5f;

    protected float _horizontal;

    // Sets input.  If not grounded, it decays that input.
    public override void EarlyProcessAbility()
    {
        base.EarlyProcessAbility();
        _horizontal = _playerController.IsGrounded ? Input.GetAxis("Horizontal") : Mathf.Lerp(_horizontal, Input.GetAxis("Horizontal") * 0.5f, Time.deltaTime);
    }

    // Sets controller
    public override void ProcessAbility()
    {
        base.ProcessAbility();
        
        if(_horizontal < 0.01)
        {
            _playerController.CurrentDirection = Vector3.Lerp(_playerController.CurrentDirection, Vector3.zero, Time.deltaTime);
        }
        
        _playerController.CurrentDirection = new Vector3(_horizontal * Speed, 0f, 0f);
    }

}
