using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseJumpAbility : Ability
{
    public float JumpForce = 5f;
    public float MaxJumpsPerSecond = 3f;

    protected float _nextJump = 0f; // Just in case.  So we don't get two jump impulses
    protected Vector3 _jump;

    public override void EarlyProcessAbility()
    {
        base.EarlyProcessAbility();
        
        if (_playerController.IsGrounded)
        {
            
            if (Input.GetAxis("Touch") > 0.01 && Time.time > _nextJump)
            {
                _nextJump = Time.time + 1 / MaxJumpsPerSecond;
                var mouse = Input.mousePosition;
                mouse.z = -Camera.main.transform.position.z;
                _jump = (Camera.main.ScreenToWorldPoint(mouse) - transform.position).normalized * JumpForce;
                return;
            }
        }
        _jump = Vector3.zero;
    }

    public override void ProcessAbility()
    {
        base.ProcessAbility();
        _playerController.PlayerRigidBody.AddForce(_jump, ForceMode.Impulse);
    }
}
