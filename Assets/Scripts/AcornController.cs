using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornController : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition += Time.deltaTime * speed * Vector3.back;
        if (transform.localPosition.z <= -22f)
            Destroy(transform.gameObject);
    }

}
