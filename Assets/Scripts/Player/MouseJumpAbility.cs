﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseJumpAbility : Ability
{
    public float JumpForce = 5f;

    protected Vector3 _jump;

    public override void EarlyProcessAbility()
    {
        base.EarlyProcessAbility();
        
        if (_playerController.IsGrounded)
        {
            
            if (Input.GetMouseButtonDown(0))
            {
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
