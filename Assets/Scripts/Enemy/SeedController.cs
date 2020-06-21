using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedController : MonoBehaviour
{
    public GameObject vinePrefab;
    public GameObject flowerPrefab;

    GameController gameCtrl;

    void Start()
    {
        gameCtrl = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "VineContainer")
        {
            SpawnVine();
        }
        else if( other.transform.tag == "Player")
        {
            GameObject flower = Instantiate(flowerPrefab, transform.position, Quaternion.identity);
            flower.transform.parent = gameCtrl.seedContainer;
            gameCtrl.AddScore(1);
            Destroy(gameObject);
        }
        else if (other.name.StartsWith("seedTrigger"))
        {
            SpawnVine();
        }
    }

    private void SpawnVine()
    {
        //Create vines and destroy the seed
        GameObject vines = Instantiate(vinePrefab, transform.position, Quaternion.identity);
        vines.transform.parent = gameCtrl.vineContainer;
        Destroy(gameObject);
    }
}
