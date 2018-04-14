using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/MoveAction")]
public class MoveAction : Action
{
    public override void Act(StateController controller)
    {
        Move(controller);
    }

    private void Move(StateController controller)
    {
        if (controller.NextWaypoint < controller.WaypointList.Length && controller.MovementAgent.MoveTo(controller.WaypointList[controller.NextWaypoint]))
        {
            controller.NextWaypoint++;
        }
        else
        {
            controller.NextWaypoint = 0;
        }
        //if (controller.transform.position == controller.MovementAgent.Destination && controller.NextWaypoint < controller.WaypointList.Count)
        //{
        //    controller.NextWaypoint++;
        //}
    }
}
