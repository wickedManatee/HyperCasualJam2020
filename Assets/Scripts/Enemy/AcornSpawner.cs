﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornSpawner : MonoBehaviour
{
    public static AcornSpawner Instance;
    [Header("Spawn Randomness")]
    public float MaxXRandomSpawnForce = 1f;
    public float MaxYRandomSpawnForce = 1f;
    [Header("Prefab")]
    public GameObject acorn;
    [Header("Containers")] //to keep our editor clean
    public Transform acornContainer;
    public Transform seedContainer;
    public Transform branchContainer;

    GameController gameCtrl;

    [Header("Timers")]
    [SerializeField]
    float spawnTimer;
    [SerializeField]
    float timerToSpawn;
    [SerializeField]
    float spawnIncreaseTimer;
    [SerializeField]
    float timerToIncrease = 10f;

    float _lastSpawn = 0;

    protected bool _stopped = false;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        gameCtrl = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        //timerToSpawn = 5f;
        //timerToIncrease = 10f;
        //spawnTimer = 5f;
        //spawnIncreaseTimer = 0;
    }

    void Update()
    {
        if (_stopped)
            return;

        //Spawn acorn every timerToSpawn seconds
        spawnTimer += Time.deltaTime;
        //Decrease how long it takes to spawn acorns every timerToIncrease seconds
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
        if (_lastSpawn == 0)
            _lastSpawn = Random.Range(-4.5f, 4.5f);
        else if (_lastSpawn < 0)
            _lastSpawn = Random.Range(0, 4.5f);
        else if (_lastSpawn > 0)
            _lastSpawn = Random.Range(-4.5f, 0);

        GameObject acornInstance =Instantiate(acorn, acornContainer);
        acornInstance.transform.position = transform.position + new Vector3(_lastSpawn, 0f, 0f);
        acornInstance.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-MaxXRandomSpawnForce, MaxXRandomSpawnForce), Random.Range(0f, MaxYRandomSpawnForce), 0f), ForceMode.Impulse);
        acornInstance.GetComponent<AcornController>().seedContainer = seedContainer;
        acornInstance.GetComponent<AcornController>().branchContainer = branchContainer;
    }

    public void Stop()
    {
        _stopped = true;
    }
}
