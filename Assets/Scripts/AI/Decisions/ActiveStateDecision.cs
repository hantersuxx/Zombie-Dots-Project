using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/ActiveState")]
public class ActiveStateDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        if (controller.ChaseTarget == null
            || !BoardManager.Instance.GridDictionary.GetClosestPosition(controller.ChaseTarget.position).Value.walkable)
        {
            return false;
        }
        if (controller.ChaseTarget.position != controller.transform.position
            && Vector3.Distance(controller.transform.position, controller.ChaseTarget.position) <= controller.FOV.ViewRange)
        {
            return true;
        }
        return false;
    }
}
