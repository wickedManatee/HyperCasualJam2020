using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    public Vector3 JumpForce;
    public Vector3 CurrentDirection;
    public LayerMask GroundLayers;
    public SphereCollider PlayerCollider { get; protected set; }
    public bool IsGrounded {  get { return Grounded(); } }


    protected List<Ability> _abilities;
    protected Rigidbody _rigidbody;

    protected virtual void Awake()
    {
        _abilities = new List<Ability>(GetComponents<Ability>());
        _rigidbody = GetComponent<Rigidbody>();
        PlayerCollider = GetComponent<SphereCollider>();
    }

    protected virtual void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + (CurrentDirection + JumpForce) * Time.deltaTime);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        ClearController();
        EarlyProcess();
        Process();
        LateProcess();
    }

    protected virtual void ClearController()
    {
        JumpForce = Vector3.zero;
        CurrentDirection = Vector3.zero;
    }

    protected virtual void EarlyProcess()
    {
        for(int i = 0; i < _abilities.Count; ++i)
        {
            _abilities[i].EarlyProcessAbility();
        }
    }

    protected virtual void Process()
    {
        for (int i = 0; i < _abilities.Count; ++i)
        {
            _abilities[i].ProcessAbility();
        }
    }

    protected virtual void LateProcess()
    {
        for (int i = 0; i < _abilities.Count; ++i)
        {
            _abilities[i].LateProcessAbility();
        }
    }


    protected virtual bool Grounded()
    {
        var grounded = Physics.OverlapSphere(transform.position, PlayerCollider.radius, GroundLayers.value);
        return grounded != null && grounded.Length > 0;
    }
}
