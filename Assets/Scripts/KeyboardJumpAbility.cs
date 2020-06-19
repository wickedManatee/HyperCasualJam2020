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

        if (IsGrounded())
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

    protected virtual bool IsGrounded()
    {
        var grounded = Physics.OverlapSphere(transform.position, _playerController.PlayerCollider.radius, GroundLayers.value);
        return grounded != null && grounded.Length > 0;
    }
}
