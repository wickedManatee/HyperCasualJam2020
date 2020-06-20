using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineTop : VineBasic
{
    public float speed = .5f;
    public bool needsToSpawn;
    [SerializeField]
    private bool _keepMoving;
    [SerializeField]
    private bool _colliding;

    private void Awake()
    {
        needsToSpawn = true;
        _keepMoving = true;
        _colliding = true;
    }

    private void Update()
    {
        if (_keepMoving || _colliding)
        {
            transform.localPosition += Time.deltaTime * speed * Vector3.up;
            transform.Rotate(0, 50f*Time.deltaTime, 0);
        }
    }

    public override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);
        if (collision.name.StartsWith("vineMid"))
        {
            _colliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name.StartsWith("vineMid"))
        {
            needsToSpawn = true;
            _colliding = false;
        }
    }

    public void StopMoving()
    {
        _keepMoving = false;
    }
}
