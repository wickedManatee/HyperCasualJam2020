using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineSpawner : MonoBehaviour
{
    public GameObject vineTopPrefab;
    public GameObject vineMiddlePrefab;

    GameController gameCtrl;
    [SerializeField]
    int height;
    [SerializeField]
    int spawnCounter = 0;
    GameObject vineTop;
    
    void Start()
    {
        gameCtrl = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        height = Random.Range(3, 6);
        SpawnTop();
    }

    void Update()
    {
        if (spawnCounter < height && vineTop.GetComponent<VineTop>().needsToSpawn)
        {
            SpawnMid();
        }
        else if (spawnCounter == height)
            vineTop.GetComponent<VineTop>().StopMoving();
    }

    void SpawnTop()
    {
        vineTop = Instantiate(vineTopPrefab, transform, true);
        vineTop.transform.position = transform.position;
        vineTop.transform.localScale = Vector3.one;
        vineTop.GetComponent<VineTop>().gameCtrl = gameCtrl;
    }

    void SpawnMid()
    {
        spawnCounter++;
        GameObject vineMid = Instantiate(vineMiddlePrefab, transform, true);
        vineMid.transform.position = vineTop.transform.position;
        vineMid.transform.localScale = new Vector3(.25f, .5f, .25f);
        vineMid.GetComponent<VineBasic>().gameCtrl = gameCtrl;

        vineTop.GetComponent<VineTop>().needsToSpawn = false;
    }
}
