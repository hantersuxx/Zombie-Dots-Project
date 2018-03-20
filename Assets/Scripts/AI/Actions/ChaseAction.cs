using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/ChaseAction")]
public class ChaseAction : Action
{
    public override void Act(StateController controller)
    {
        Chase(controller);
    }

    private void Chase(StateController controller)
    {
        //controller.WaypointList = controller.BoardManager.ShortestPath.GetPath(controller.transform.position, controller.ChaseTarget.position);
        controller.NextWaypoint = 0;
    }
}
