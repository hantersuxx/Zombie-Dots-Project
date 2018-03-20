using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/LookDecision")]
public class LookDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetVisible = Look(controller);
        return targetVisible;
    }

    private bool Look(StateController controller)
    {
        Transform firstSeenTarget = controller.FOV.VisibleTargets.FirstOrDefault();
        if (firstSeenTarget != null && controller.transform.position != firstSeenTarget.position)
        {
            controller.ChaseTarget = firstSeenTarget;
            return true;
        }
        else
        {
            controller.ChaseTarget = GameObject.FindGameObjectWithTag(Tags.Vault.ToString()).transform;
            return false;
        }
        //if (controller.FOV.VisibleTargets.Count > 0)
        //{
        //    if (firstSeenTarget == controller.ChaseTarget)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        controller.ChaseTarget = firstSeenTarget;
        //        return true;
        //    }
        //}
        //controller.ChaseTarget = GameObject.FindGameObjectWithTag(Tags.Vault.ToString()).transform;
        //return false;
        //float angle = Quaternion.Angle(Quaternion.Euler(new Vector3(0, 0, 0)), controller.Eyes.rotation);
        //var direction = controller.Eyes.transform.DirectionFromAngle(-angle, true);
        //var test = direction * controller.Stats.lookSphereCastRadius;
        //var test2 = controller.Eyes.position + test;
        //Debug.DrawRay(test2, direction * (controller.Stats.viewRange + controller.Stats.lookSphereCastRadius), Color.yellow);
        //var hit = Physics2D.CircleCast(test2, controller.Stats.lookSphereCastRadius, direction, controller.Stats.viewRange);
        //if (hit.transform?.tag == Tags.Vault)
        //{
        //    controller.ChaseTarget = hit.transform;
        //    return true;
        //}
        //return false;
    }
}
