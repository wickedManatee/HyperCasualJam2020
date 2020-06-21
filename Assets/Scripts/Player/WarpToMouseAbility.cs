using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpToMouseAbility : Ability
{
    public int JumpsAllowed = 1;
    public float MaxRange = 3f;
    [Range(0f, 0.2f)]
    public float IgnoreTopClicsByPercent = 0.065f; // The Top % of the screen we should ignore for jumps

    protected bool _jump = false;
    protected Vector3 _toLocation;
    protected int _jumpsLeft = 0;

    public override void EarlyProcessAbility()
    {
        base.EarlyProcessAbility();

        if (_jumpsLeft > 0 || _playerController.IsGrounded)
        {
            
            if (Input.GetMouseButtonDown(0))
            {
                var mouse = Input.mousePosition;
                if (mouse.y / Camera.main.pixelHeight < 1 - IgnoreTopClicsByPercent)    // The player has not clicked on the top most edge of the screen as defined by the IgnoreTopClicks property
                {
                    mouse.z = -Camera.main.transform.position.z;
                    _jump = true;
                    _toLocation = Vector3.ClampMagnitude(Camera.main.ScreenToWorldPoint(mouse) - transform.position, MaxRange) + transform.position;
                    _jumpsLeft--;
                    return;
                }
            }
            if(!_jump && _playerController.IsGrounded) _jumpsLeft = JumpsAllowed;
        }
        _jump = false;
    }

    public override void ProcessAbility()
    {
        base.ProcessAbility();
        if (!GetComponent<DestroyAcornAbility>().AcornMarkedForDeath && _jump)
        {
            //_playerController.AddForce((_toLocation - transform.position).normalized, ForceMode.Impulse);
            _playerController.SetPosition(_toLocation, false);
        }
    }
}
