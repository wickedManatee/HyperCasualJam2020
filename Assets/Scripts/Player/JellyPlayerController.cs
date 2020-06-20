using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyPlayerController : PlayerController
{
    protected JellySprite _jelly;

    protected override void Awake()
    {
        base.Awake();
        _jelly = GetComponent<UnityJellySprite>();
    }
    protected override void FixedUpdate()
    {
        //base.FixedUpdate();
    }

    public override void AddForce(Vector3 jump, ForceMode impulse)
    {
        
        _jelly.AddForce(jump, impulse);
    }

    protected override bool Grounded()
    {
        return _jelly.IsGrounded(GroundLayers, 2);
    }
}
