using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAcornAbility : Ability
{
    public override void ProcessAbility()
    {
        base.ProcessAbility();

        if (Input.GetMouseButtonDown(0)) //mouse or phone touch
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            { //If we clicked on an acorn
                if (hit.transform.name.StartsWith("Acorn"))
                {
                    hit.transform.GetComponent<AcornController>().DestroyAcorn();
                }
            }
        }
    }
}
