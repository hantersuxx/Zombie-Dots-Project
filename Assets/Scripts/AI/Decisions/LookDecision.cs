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
            controller.ChaseTarget = GameManager.Instance.Vault.transform;
            return false;
        }
    }
}
