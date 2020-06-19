using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseJumpAbility : Ability
{
    public float JumpForce = 30f;
    public float MaxJumpControl = 5f;  // You can control how far the player jumps by tapping, at most, this distance away

    protected Vector3 _jump;

    public override void EarlyProcessAbility()
    {
        base.EarlyProcessAbility();
        
        if (_playerController.IsGrounded)
        {
            if (Input.GetAxis("Touch") > 0.01)
            {
                var mouse = Input.mousePosition;
                mouse.z = -Camera.main.transform.position.z;
                Debug.Log((Camera.main.ScreenToWorldPoint(mouse) - transform.position).magnitude);
                _jump = Vector3.ClampMagnitude(Camera.main.ScreenToWorldPoint(mouse) - transform.position, MaxJumpControl) * JumpForce;
                
                return;
            }
        }
        _jump = Vector3.zero;
    }

    public override void ProcessAbility()
    {
        base.ProcessAbility();
        _playerController.JumpForce = _jump;
    }
}
