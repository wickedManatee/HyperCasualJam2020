using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineController : MonoBehaviour
{
    public float speed;

    GameController gameCtrl;

    void Start()
    {
        speed = .25f;
        gameCtrl = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        transform.localPosition += Time.deltaTime * speed * Vector3.up;
        //gameObject.transform.Rotate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameCtrl.GameOver();
        }
    }
}
