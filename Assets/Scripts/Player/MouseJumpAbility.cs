using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseJumpAbility : Ability
{
    public float JumpForce = 5f;
    [Range(0f, 0.2f)]
    public float IgnoreTopClicsByPercent = 0.05f; // The Top % of the screen we should ignore for jumps
    protected Vector3 _jump;

    public override void EarlyProcessAbility()
    {
        base.EarlyProcessAbility();
        
        if (_playerController.IsGrounded)
        {
            
            if (Input.GetMouseButtonDown(0))
            {
                var mouse = Input.mousePosition;
                if (mouse.y / Camera.main.pixelHeight < 1 - IgnoreTopClicsByPercent)    // The player has not clicked on the top most edge of the screen as defined by the IgnoreTopClicks property
                {
                    mouse.z = -Camera.main.transform.position.z;
                    _jump = (Camera.main.ScreenToWorldPoint(mouse) - transform.position).normalized * JumpForce;
                    return;
                }
            }
        }
        _jump = Vector3.zero;
    }

    public override void ProcessAbility()
    {
        base.ProcessAbility();
        if (!GetComponent<DestroyAcornAbility>().AcornMarkedForDeath)
        {
            _playerController.PlayerRigidBody.AddForce(_jump, ForceMode.Impulse);
        }
    }
}
