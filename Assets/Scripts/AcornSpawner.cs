using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornSpawner : MonoBehaviour
{
    public GameObject acorn;

    GameController gameCtrl;

    [SerializeField]
    float spawnTimer;
    [SerializeField]
    float timerToSpawn;
    [SerializeField]
    float spawnIncreaseTimer;
    [SerializeField]
    float timerToIncrease;

    void Start()
    {
        gameCtrl = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        timerToSpawn = 5f;
        timerToIncrease = 10f;
        spawnTimer = 5f;
        spawnIncreaseTimer = 0;
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;
        spawnIncreaseTimer += Time.deltaTime;

        if (spawnTimer >= timerToSpawn)
        {
            SpawnAcorn();
            spawnTimer = 0;
        }

        if (spawnIncreaseTimer >= timerToIncrease)
        {
            timerToSpawn = Mathf.Max(1f, timerToSpawn - .1f);
            spawnIncreaseTimer = 0;
        }
    }

    void SpawnAcorn()
    {
        float randomX = Random.Range(-4.5f, 4.5f);
        GameObject acornInstance =Instantiate(acorn, transform);
        acornInstance.transform.position = new Vector3(randomX, transform.position.y, transform.position.z);
    }
}
