﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    public Vector3 JumpForce;           // Set this with some Ability script
    public Vector3 CurrentDirection;    // Set this with some Ability script
    public LayerMask GroundLayers;      // Used to determine if the player is grounded
    public SphereCollider PlayerCollider { get; protected set; }    
    public bool IsGrounded {  get { return Grounded(); } }


    protected List<Ability> _abilities;     // The controller calls all abilities every frame
    protected Rigidbody _rigidbody;         

    protected virtual void Awake()
    {
        _abilities = new List<Ability>(GetComponents<Ability>());
        _rigidbody = GetComponent<Rigidbody>();
        PlayerCollider = GetComponent<SphereCollider>();
    }

    // A simple implementation.  We can tweak if needed.  This is the meat of this script since it's where the "controlling" actually occurs.
    protected virtual void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + (CurrentDirection + JumpForce) * Time.deltaTime);
    }

    // Calls all abilities.   These calls could just as easily be refactored into a separate Character class but for convenience we'll keep it in the controller for now
    // Doing it this way so we can easily add polished features such as Pausing later if there's time
    protected virtual void Update()
    {
        ClearController();
        EarlyProcess();
        Process();
        LateProcess();

        if (Input.GetMouseButtonDown(0)) //mouse or phone touch
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            { //If we clicked on an acorn
                if (hit.transform.name.StartsWith("Acorn"))
                {
                    hit.transform.GetComponent<AcornController>().DestroyAcorn();
                }
            }
        }
    }

    // Resets all values.  It's on the abilities to set these values themselves every frame.
    protected virtual void ClearController()
    {
        JumpForce = Vector3.zero;
        CurrentDirection = Vector3.zero;
    }

    // Activates abilities so they can run
    protected virtual void EarlyProcess()
    {
        for(int i = 0; i < _abilities.Count; ++i)
        {
            _abilities[i].EarlyProcessAbility();
        }
    }

    // Activates abilities so they can run
    protected virtual void Process()
    {
        for (int i = 0; i < _abilities.Count; ++i)
        {
            _abilities[i].ProcessAbility();
        }
    }

    // Activates abilities so they can run
    protected virtual void LateProcess()
    {
        for (int i = 0; i < _abilities.Count; ++i)
        {
            _abilities[i].LateProcessAbility();
        }
    }

    // Checks if the player is touching a Ground layer
    protected virtual bool Grounded()
    {
        var grounded = Physics.OverlapSphere(transform.position, PlayerCollider.radius, GroundLayers.value);
        return grounded != null && grounded.Length > 0;
    }
}
