using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerRandomizer : MonoBehaviour
{
    public Sprite[] Options;
    protected virtual void Awake()
    {
        var random = Random.Range(0, 2);
        Debug.Log(random);
        GetComponent<SpriteRenderer>().sprite = Options[random];
    }

}
