using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BranchController : MonoBehaviour
{
    public Collider BranchCollider;
    public Collider PlayerTrigger;

    protected PlayerController _player;
    protected float _y;

    protected virtual void Awake()
    {
        _player = FindObjectOfType<PlayerController>();
        Assert.IsNotNull(_player);
        Assert.IsNull(_player.transform.parent, "Logic in this BranchController depends on the Player not being parented under any scene object.  Change Update() in this controller to use _player.transform.position, or unparent the Player from whatever it was just parented under.");
    }

    protected virtual void Start()
    {
        PlayerTrigger.transform.position = new Vector3( 
            PlayerTrigger.transform.position.x,
            transform.position.y + BranchCollider.bounds.extents.y + _player.PlayerCollider.bounds.size.y + PlayerTrigger.bounds.extents.y,
            PlayerTrigger.transform.position.z
            );
        _y = transform.position.y;
    }

    protected virtual void Update()
    {
        if(BranchCollider.enabled && _player.transform.localPosition.y < _y)
        {
            BranchCollider.enabled = false;
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(BranchCollider.enabled && other.GetComponent<PlayerController>() != null)
        {
            BranchCollider.enabled = true;
        }
    }
}
