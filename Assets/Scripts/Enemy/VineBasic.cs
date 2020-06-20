using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineBasic : MonoBehaviour
{
    [HideInInspector]
    public GameController gameCtrl;

    public virtual void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            gameCtrl.GameOver();
        }
    }
}
