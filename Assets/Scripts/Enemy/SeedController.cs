using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedController : MonoBehaviour
{
    public float speed;
    public GameObject vinePrefab;

    void Start()
    {
        speed = .5f;
    }

    void Update()
    {
        transform.localPosition += Time.deltaTime * speed * Vector3.back;
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
            vines.transform.localPosition += new Vector3(0, 0, -24f);
            Destroy(gameObject);
        }
    }
}
