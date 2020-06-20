using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedController : MonoBehaviour
{
    public GameObject vinePrefab;
    public GameObject flowerPrefab;

    GameController gameCtrl;
    [HideInInspector]
    public Transform vineContainer;

    void Start()
    {
        gameCtrl = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "VineContainer")
        {
            Debug.Log("Spawning vine from floor");
            SpawnVine();
        }
        else if( other.transform.tag == "Player")
        {
            GameObject flower = Instantiate(flowerPrefab, other.transform, false);
            flower.transform.position = transform.position;
            gameCtrl.AddScore(1);
            Destroy(gameObject);
        }
        else if (other.name.StartsWith("seedTrigger"))
        {
            Debug.Log("Spawning vine from branch");
            SpawnVine();
        }
    }

    private void SpawnVine()
    {
        //Create vines and destroy the seed
        GameObject vines = Instantiate(vinePrefab, transform.position, Quaternion.identity);
        vines.transform.parent = vineContainer;
        //vines.transform.position = transform.position;
        //vines.transform.lossyScale = Vector3.one;
        Destroy(gameObject);
    }
}
