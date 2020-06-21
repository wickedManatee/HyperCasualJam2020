using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{

    public Vector3 CurrentDirection;    // Set this with some Ability script
    public float Friction = 5f;
    public LayerMask GroundLayers;      // Used to determine if the player is grounded
    public SphereCollider PlayerCollider { get; protected set; }
    public Rigidbody PlayerRigidBody { get { return _rigidbody; } }
    public bool IsGrounded {  get { return Grounded(); } }
    public bool Paused;

    protected List<Ability> _abilities;     // The controller calls all abilities every frame
    protected Rigidbody _rigidbody;         

    protected virtual void Awake()
    {
        _abilities = new List<Ability>(GetComponents<Ability>());
        _rigidbody = GetComponent<Rigidbody>();
        PlayerCollider = GetComponent<SphereCollider>();
        Paused = false;
    }

    public virtual void SetKinematic(bool isKinematic)
    {
        _rigidbody.isKinematic = isKinematic;
    }

    // A simple implementation.  We can tweak if needed.  This is the meat of this script since it's where the "controlling" actually occurs.
    protected virtual void FixedUpdate()
    {
        _rigidbody.AddForce(CurrentDirection * 100 * Time.deltaTime, ForceMode.Acceleration);
        if (IsGrounded) _rigidbody.AddForce(-_rigidbody.velocity.normalized * Friction * 100 * Time.deltaTime, ForceMode.Acceleration);
    }

    public virtual void SetPosition(Vector3 vector3, bool resetVelocity = true)
    {
        transform.position = vector3;
    }



    // Calls all abilities.   These calls could just as easily be refactored into a separate Character class but for convenience we'll keep it in the controller for now
    // Doing it this way so we can easily add polished features such as Pausing later if there's time
    protected virtual void Update()
    {
        if (Paused)
            return;

        ClearController();
        EarlyProcess();
        Process();
        LateProcess();
    }

    // Resets all values.  It's on the abilities to set these values themselves every frame.
    protected virtual void ClearController()
    {
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

    public virtual void Respawn()
    {
        transform.position = new Vector3(0, -2.11f, 0);
    }

    public virtual void AddForce(Vector3 jump, ForceMode impulse)
    {
        _rigidbody.AddForce(jump, impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "OutOfBounds" && collision.transform.name == "Floor")
        {
            Respawn();
            //TODO when testing over -> gameCtrl.GameOver();
        }
    }
}
