using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerRandomizer : MonoBehaviour
{
    public Sprite[] Options;
    protected virtual void Awake()
    {
        var random = Random.Range(0, Options.Length);
        GetComponent<SpriteRenderer>().sprite = Options[random];
    }

}
