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
    [HideInInspector]
    public Transform vineContainer;

    GameController gameCtrl;
    Vector3 storedVelocity;

    void Start()
    {
        gameCtrl = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    public void PassContainers(Transform seed, Transform branch, Transform fx, Transform vine)
    {
        seedContainer = seed;
        branchContainer = branch;
        fxContainer = fx;
        vineContainer = vine;
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

        GameObject seed = Instantiate(seedsPrefab, seedContainer);
        seed.transform.position = transform.position + Vector3.down;
        seed.GetComponent<SeedController>().vineContainer = vineContainer;

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