using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornController : MonoBehaviour
{
    public float speed;
    public GameObject seedsPrefab;
    public GameObject branchPrefab;

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

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name.StartsWith("Acorn"))
                {
                    GameObject seeds = Instantiate(seedsPrefab, hit.transform.position, Quaternion.identity);
                    GameObject branch = Instantiate(branchPrefab, hit.transform.position, Quaternion.identity);
                    Destroy(hit.transform.gameObject);
                }
                else
                {
                    Debug.Log("This isn't an Acorn");
                }
            }
        }
    }
}
