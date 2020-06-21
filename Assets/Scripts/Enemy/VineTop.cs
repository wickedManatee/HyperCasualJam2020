using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineTop : VineBasic
{
    
    public bool needsToSpawn;

    bool _keepMoving;
    bool _colliding;
    float speed;

    private void Awake()
    {
        needsToSpawn = true;
        _keepMoving = true;
        _colliding = true;
        speed = Random.Range(.25f, 1.5f);
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
