using System.Collections;
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
    [HideInInspector]
    public Transform fxContainer;

    GameController gameCtrl;
    Vector3 storedVelocity;

    void Start()
    {
        gameCtrl = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    public void PassContainers(Transform seed, Transform branch, Transform fx)
    {
        seedContainer = seed;
        branchContainer = branch;
        fxContainer = fx;
    }

    public void DestroyAcorn()
    {
        GameObject clickPrefab = Resources.Load<GameObject>("Prefabs/FX_Click");
        GameObject fxClick = Instantiate(clickPrefab, fxContainer);
        fxClick.transform.position = transform.position + Vector3.back * 2;
        Destroy(fxClick, 2f);
        GameObject lightningPrefab = Resources.Load<GameObject>("Prefabs/FX_Lightnings");
        GameObject fxLightning = Instantiate(lightningPrefab, fxContainer);
        fxLightning.transform.position = transform.position + Vector3.back* 2;
        Destroy(fxLightning, 2f);

        GameObject seeds = Instantiate(seedsPrefab, seedContainer);
        seeds.transform.position = transform.position;
        GameObject branch = Instantiate(branchPrefab, branchContainer);
        branch.transform.position = transform.position;
        gameCtrl.AddScore(2);

        //Now that we are done with acorn, destroy it
        Destroy(transform.gameObject);
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