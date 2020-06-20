using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAcornAbility : Ability
{
    public bool AcornMarkedForDeath { get { return _markedForDeath != null; } }
    protected AcornController _markedForDeath;

    public override void EarlyProcessAbility()
    {
        base.EarlyProcessAbility();
        if (Input.GetMouseButtonDown(0)) //mouse or phone touch
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            { //If we clicked on an acorn
                if (hit.transform.name.StartsWith("Acorn"))
                {
                    _markedForDeath = hit.transform.GetComponent<AcornController>();
                }
            }
        }
    }

    public override void LateProcessAbility()
    {
        base.ProcessAbility();
        if(_markedForDeath != null)
        {
            _markedForDeath.DestroyAcorn();
            _markedForDeath = null;
        }
        
    }
}
