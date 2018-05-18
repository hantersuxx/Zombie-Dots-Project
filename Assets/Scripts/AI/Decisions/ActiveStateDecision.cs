using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/ActiveState")]
public class ActiveStateDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        if (controller.ChaseTarget == null)
        {
            return false;
        }
        if (!Extensions.GetClosestPosition(controller.ChaseTarget.position, BoardManager.Instance.GridDictionary).Value.walkable)
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
