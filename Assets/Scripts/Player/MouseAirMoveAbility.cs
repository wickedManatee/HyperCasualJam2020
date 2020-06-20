using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAirMoveAbility : Ability
{
    public float MoveForce = 5f; 
    protected Vector3 _movement;

    public override void EarlyProcessAbility()
    {
        base.ProcessAbility();
        if (!_playerController.IsGrounded)
        {

            if (Input.GetAxis("Touch") > 0.01)
            {
                var mouse = Input.mousePosition;
                mouse.z = -Camera.main.transform.position.z;
                _movement = (Camera.main.ScreenToWorldPoint(mouse) - transform.position).normalized * MoveForce;
                return;
            }
        }
        _movement = Vector3.zero;
    }

    public override void ProcessAbility()
    {
        base.ProcessAbility();
        _playerController.CurrentDirection = _movement;
    }
}
