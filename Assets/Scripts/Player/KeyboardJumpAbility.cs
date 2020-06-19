using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A Keyboard implementation of Jumping
public class KeyboardJumpAbility : Ability
{
    public float JumpForce = 5f;

    protected float _jump;

    // If grounded and the player presses jump
    public override void EarlyProcessAbility()
    {
        base.EarlyProcessAbility();

        if (_playerController.IsGrounded)
        {
            _jump = Input.GetAxis("Jump");
            return;
        }
    }

    // Modifies controller
    public override void ProcessAbility()
    {
        base.ProcessAbility();
        _playerController.JumpForce = new Vector3(0f, _jump * JumpForce, 0f);
    }

    
}
