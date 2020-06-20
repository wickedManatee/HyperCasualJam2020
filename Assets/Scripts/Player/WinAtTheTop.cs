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
    public Canvas LevelCompleteHUD;

    protected virtual void Start()
    {
        CloudMesh.enabled = false;
        if(LevelCompleteHUD == null)
        {
            Debug.LogWarning("LevelCompleteHUD not set.  Trying to find one in scene.");
            LevelCompleteHUD = FindObjectOfType<Canvas>();
        }
        LevelCompleteHUD.gameObject.SetActive(false);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Finish"))
        {
            if(!LevelComplete)
            {
                FinishedLevel();
            }
        }
    }

    protected virtual void FinishedLevel()
    {
        LevelComplete = true;
        GameController.Instance.LevelComplete();
        GetComponent<Rigidbody>().isKinematic = true;
        StartCoroutine(TurnToCloud());
    }

    protected virtual IEnumerator TurnToCloud()
    {
        var raiseForTime = Time.time + 2f;
        var speed = new Vector3(0f, RisingToCloudSpeed, 0f);
        TransformMesh();
        while (Time.time < raiseForTime)
        {
            transform.position = transform.position + speed*Time.deltaTime;
            yield return null;
        }
        Congrats();
    }

    protected virtual void TransformMesh()
    {
        DropletMesh.enabled = false;
        CloudMesh.enabled = true;
    }

    protected virtual void Congrats()
    {
        Debug.Log("Level Complete");
        LevelCompleteHUD.gameObject.SetActive(true);
    }
}
