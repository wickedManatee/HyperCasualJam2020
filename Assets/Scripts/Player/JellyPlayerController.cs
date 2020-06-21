using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyPlayerController : PlayerController
{
    [Header("Jelly Tuning")]
    public int WarpFrames = 2;  // How many frames to warp a jelly.  The first have will move on the first frame, the rest this many frames later

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


    protected CollisionDetectionMode[] _modes;
    public override void SetKinematic(bool isKinematic)
    {
        if (isKinematic)
            UpdateRigidbodyModes();
        else
            ReturnToOldModes();

        _jelly.SetKinematic(isKinematic, false);
    }
    private void UpdateRigidbodyModes()
    {
        if (_modes == null)
            _modes = new CollisionDetectionMode[_jelly.ReferencePoints.Count];

        for (int i = 0; i < _jelly.ReferencePoints.Count; ++i)
        {
            _modes[i] = _jelly.ReferencePoints[i].Body3D.collisionDetectionMode;
            _jelly.ReferencePoints[i].Body3D.collisionDetectionMode = CollisionDetectionMode.Discrete;
        }
    }
    private void ReturnToOldModes()
    {
        if (_modes == null)
            return;

        for (int i = 0; i < _jelly.ReferencePoints.Count; ++i)
        {
            _jelly.ReferencePoints[i].Body3D.collisionDetectionMode = _modes[i];
        }
    }


    public override void SetPosition(Vector3 position, bool resetVelocity = true)
    {

        if (resetVelocity) // So I don't have to write another method.  This is called by WinAtTheTop
        {
            _jelly.SetPosition(position, resetVelocity);
            return;
        }

        StartCoroutine(MoveOverFrames(position, WarpFrames));
        //_jelly.CentralPoint.Body3D.MovePosition(position);
    }

    protected bool _alreadyRunning = false;
    protected IEnumerator MoveOverFrames(Vector3 position, int frames)
    {
        if (_alreadyRunning)
            yield break;
        _alreadyRunning = true;
        
        int startIndex = 1;
        int indicesBeforeWaiting = 4;

        for (int i = 0; i < _jelly.ReferencePoints.Count; ++i)
        {
            if (i > indicesBeforeWaiting)
            {
                for (int j = 0; j < frames; ++j)
                {
                    yield return null;
                }
                indicesBeforeWaiting = _jelly.ReferencePoints.Count + 1;
            }

            _jelly.ReferencePoints[(i + startIndex) % _jelly.ReferencePoints.Count].Body3D.MovePosition(position);
        }
        
        _alreadyRunning = false;
    }

}
