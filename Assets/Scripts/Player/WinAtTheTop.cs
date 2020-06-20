using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinAtTheTop : MonoBehaviour
{
    public bool LevelComplete = false;
    public float RisingToCloudSpeed = 2f;
    public MeshRenderer DropletMesh;
    public MeshRenderer CloudMesh;

    protected virtual void Start()
    {
        CloudMesh.enabled = false;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        TriggerEnter(other);
    }

    protected virtual void OnJellyTriggerEnter(JellySprite.JellyCollider collision)
    {
        TriggerEnter(collision.Collider);
    }

    protected virtual void TriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            if (!LevelComplete)
            {
                FinishedLevel();
            }
        }
    }

    protected virtual void FinishedLevel()
    {
        LevelComplete = true;
        GameController.Instance.LevelComplete();
        //GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<PlayerController>().SetKinematic(true);
        StartCoroutine(TurnToCloud());
    }

    protected virtual IEnumerator TurnToCloud()
    {
        var raiseForTime = Time.time + 2f;
        var speed = new Vector3(0f, RisingToCloudSpeed, 0f);
        TransformMesh();
        while (Time.time < raiseForTime)
        {
            GetComponent<PlayerController>().SetPosition(transform.position + speed * Time.deltaTime);
            yield return null;
        }
        Congrats();
    }

    protected virtual void TransformMesh()
    {
        if(DropletMesh != null) DropletMesh.enabled = false;
        if (CloudMesh != null) CloudMesh.enabled = true;
    }

    protected virtual void Congrats()
    {
        Debug.Log("Level Complete");
    }

    
}
