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
        height = Random.Range(1, 4);
        SpawnTop();
    }

    void Update()
    {
        if (spawnCounter < height && vineTop.GetComponentInChildren<VineTop>().needsToSpawn)
        {
            SpawnMid();
        }
        else if (spawnCounter == height)
            vineTop.GetComponentInChildren<VineTop>().StopMoving();
    }

    void SpawnTop()
    {
        vineTop = Instantiate(vineTopPrefab, transform, true);
        vineTop.transform.position = transform.position;
        vineTop.transform.localScale = Vector3.one;
        vineTop.GetComponentInChildren<VineTop>().gameCtrl = gameCtrl;
    }

    void SpawnMid()
    {
        spawnCounter++;
        GameObject vineMid = Instantiate(vineMiddlePrefab, transform, true);
        vineMid.transform.position = vineTop.transform.position;
        vineMid.transform.localScale = Vector3.one;
        vineMid.GetComponentInChildren<VineBasic>().gameCtrl = gameCtrl;

        vineTop.GetComponentInChildren<VineTop>().needsToSpawn = false;
    }
}
