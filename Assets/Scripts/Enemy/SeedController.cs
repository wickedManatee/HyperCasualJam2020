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

    void Update()
    {
        //transform.localPosition += Time.deltaTime * speed * Vector3.back;
        if (transform.localPosition.z <= -22f) //TODO Rewrite to be off camera instead
            Destroy(transform.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "VineContainer")
        {
            //Create vines and destroy the seed
            GameObject vines = Instantiate(vinePrefab, other.transform, false);
            vines.transform.position = transform.position;
            vines.transform.localPosition += new Vector3(0, -24, 0f);
            Destroy(gameObject);
        }
        else if( other.transform.tag == "Player")
        {
            GameObject flower = Instantiate(flowerPrefab, other.transform, false);
            flower.transform.position = transform.position;
            gameCtrl.AddScore(1);
            Destroy(gameObject);
        }
    }
}
