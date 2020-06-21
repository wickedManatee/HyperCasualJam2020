using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornController : MonoBehaviour
{
    public GameObject seedsPrefab;

    GameController gameCtrl;
    Vector3 storedVelocity;

    void Start()
    {
        gameCtrl = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    public void DestroyAcorn()
    {
        GameObject clickPrefab = Resources.Load<GameObject>("Prefabs/FX_Click");
        GameObject fxClick = Instantiate(clickPrefab, gameCtrl.fxContainer);
        fxClick.transform.position = transform.position + Vector3.back * 2;
        Destroy(fxClick, 2f);

        GameObject lightningPrefab = Resources.Load<GameObject>("Prefabs/FX_Lightnings");
        GameObject fxLightning = Instantiate(lightningPrefab, gameCtrl.fxContainer);
        fxLightning.transform.position = transform.position + Vector3.back * 2;
        Destroy(fxLightning, 2f);

        int seedGenerator = Random.Range(1, 5);
        for (int i = 0; i < seedGenerator; i++)
        {
            GameObject seed = Instantiate(seedsPrefab, gameCtrl.seedContainer);
            seed.transform.position = transform.position + new Vector3(.5f*i, Random.Range(-.5f,-1.5f), 0);
            seed.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-1, 6), Random.Range(0f, 6), 0f), ForceMode.Impulse);
        }

        GameObject branchPrefab = Resources.Load<GameObject>("Prefabs/branch");
        GameObject branch = Instantiate(branchPrefab, gameCtrl.branchContainer);
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