using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineTop : VineBasic
{
    
    public bool needsToSpawn;
    [SerializeField]
    bool _keepMoving;
    [SerializeField]
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
            transform.parent.localPosition += Time.deltaTime * speed * Vector3.up;
        }
        else if (!_keepMoving && !_colliding)
        {
            transform.GetComponent<Animator>().enabled = false;
        }
    }

    public override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);
        if (collision.name.StartsWith("vineMid") && collision.transform.parent.parent == transform.parent.parent)
        {
            _colliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name.StartsWith("vineMid") && other.transform.parent.parent == transform.parent.parent)
        {
            needsToSpawn = true;
            _colliding = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name.StartsWith("vineMid") && other.transform.parent.parent == transform.parent.parent)
        {
            _colliding = true;
        }
    }

    public void StopMoving()
    {
        _keepMoving = false;
    }
}
