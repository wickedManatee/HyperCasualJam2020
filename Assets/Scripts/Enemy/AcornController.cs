﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornController : MonoBehaviour
{
    public GameObject seedsPrefab;
    public GameObject branchPrefab;

    [HideInInspector]
    public Transform seedContainer;
    [HideInInspector]
    public Transform branchContainer;

    GameController gameCtrl;

    void Start()
    {
        gameCtrl = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        //transform.localPosition += Time.deltaTime * speed * Vector3.back;
        if (transform.localPosition.z <= -22f) //TODO Rewrite to be off camera instead
            Destroy(transform.gameObject);
    }

    public void DestroyAcorn()
    {
        GameObject seeds = Instantiate(seedsPrefab, seedContainer);
        seeds.transform.position = transform.position;
        GameObject branch = Instantiate(branchPrefab, branchContainer);
        branch.transform.position = transform.position;
        gameCtrl.AddScore(2);

        //Now that we are done with acorn, destroy it
        Destroy(transform.gameObject);
    }

    public void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            gameCtrl.GameOver();
        } 
        else if (other.transform.name == "Floor")
            Destroy(gameObject);
    }
}