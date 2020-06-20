using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BranchController : MonoBehaviour
{
    public Collider BranchCollider;
    public Collider PlayerTrigger;

    protected PlayerController _droplet;

    protected virtual void Awake()
    {
        _droplet = FindObjectOfType<PlayerController>();
        Assert.IsNotNull(_droplet);
    }

    protected virtual void Start()
    {
        PlayerTrigger.transform.position = transform.position + BranchCollider.bounds.extents + _droplet.PlayerCollider.bounds.size + PlayerTrigger.bounds.extents;
    }

    protected virtual void Update()
    {

    }
}
