using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardJumpAbility : Ability
{
    public float JumpForce = 5f;
    public LayerMask GroundLayers;

    protected float _jump;

    public override void EarlyProcessAbility()
    {
        base.EarlyProcessAbility();

        if (_playerController.IsGrounded)
        {
            _jump = Input.GetAxis("Jump");
            return;
        }
    }

    public override void ProcessAbility()
    {
        base.ProcessAbility();
        _playerController.JumpForce = new Vector3(0f, _jump * JumpForce, 0f);
    }

    
}
