using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornController : MonoBehaviour
{
    public float speed;
    public GameObject seedsPrefab;
    public GameObject branchPrefab;

    [HideInInspector]
    public Transform seedContainer;
    [HideInInspector]
    public Transform branchContainer;

    void Start()
    {
        speed = 1;
    }

    void Update()
    {
        transform.localPosition += Time.deltaTime * speed * Vector3.back;
        if (transform.localPosition.z <= -22f) //TODO Rewrite to be off camera instead
            Destroy(transform.gameObject);
    }

    public void DestroyAcorn()
    {
        GameObject seeds = Instantiate(seedsPrefab, seedContainer);
        seeds.transform.position = transform.position;
        GameObject branch = Instantiate(branchPrefab, branchContainer);
        branch.transform.position = transform.position;

        //Now that we are done with acorn, destroy it
        Destroy(transform.gameObject);
    }
}


// TODO
/**
 * if (Input.GetMouseButtonDown(0)) //mouse or phone touch
*        {
 *           Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
  *          RaycastHit hit;
  *
     *       if (Physics.Raycast(ray, out hit))
      *      { //If we clicked on an acorn
       *         if (hit.transform.name.StartsWith("Acorn"))
        *        {
         *           hit.transform.GetComponent<AcornController>().DestroyAcorn();                   
          *      }
           * }
        * }
 **/
