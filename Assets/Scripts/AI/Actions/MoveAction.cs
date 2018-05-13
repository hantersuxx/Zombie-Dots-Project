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
        if (controller.WaypointList.Count != 0 && !controller.MovementAgent.InMove)
        {
            var dequeued = controller.WaypointList.Dequeue();
            var curDest = new Vector3(dequeued.x, dequeued.y);
            controller.MovementAgent.MoveTo(new Vector3(curDest.x, curDest.y));
        }
    }
}
