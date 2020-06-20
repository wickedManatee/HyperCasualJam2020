using System.Collections;
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
    float spawnTimer = 5f;
    [SerializeField]
    float timerToSpawn = 5f;
    [SerializeField]
    float spawnIncreaseTimer = 0f;
    [SerializeField]
    float timerToIncrease = 10f;

    float _lastSpawn = 0;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        gameCtrl = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        if (gameCtrl.gameState != GameController.GameState.play)
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
        {
            //Prevent acorn to hit player at start
            _lastSpawn = Random.Range(1.15f, 5f);
            //Random go left or right
            _lastSpawn = Random.Range(0, 2) == 1 ? _lastSpawn * -1 : _lastSpawn; 
        }
        else if (_lastSpawn < 0) 
            _lastSpawn = Random.Range(1f, 5f); // Move farther away from 0 so there's more variance
        else if (_lastSpawn > 0)
            _lastSpawn = Random.Range(-5f, -1f);

        GameObject acornInstance =Instantiate(acorn, acornContainer);
        acornInstance.transform.position = transform.position + new Vector3(_lastSpawn, 0f, 0f);
        acornInstance.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-MaxXRandomSpawnForce, MaxXRandomSpawnForce), Random.Range(0f, MaxYRandomSpawnForce), 0f), ForceMode.Impulse);
        acornInstance.GetComponent<AcornController>().seedContainer = seedContainer;
        acornInstance.GetComponent<AcornController>().branchContainer = branchContainer;
    }
}
