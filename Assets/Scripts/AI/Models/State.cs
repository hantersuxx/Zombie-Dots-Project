using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/State")]
public class State : ScriptableObject
{
    [SerializeField]
    private Action[] actions;
    [SerializeField]
    private Transition[] transitions;
    [SerializeField]
    private Color sceneGizmoColor;

    public Action[] Actions => actions;
    public Transition[] Transitions => transitions;
    public Color SceneGizmoColor => sceneGizmoColor;

    public void UpdateState(StateController controller)
    {
        CheckTransitions(controller);
        DoActions(controller);
    }

    private void DoActions(StateController controller)
    {
        for (int i = 0; i < Actions.Length; i++)
        {
            Actions[i].Act(controller);
        }
    }

    private void CheckTransitions(StateController controller)
    {
        for (int i = 0; i < Transitions.Length; i++)
        {
            bool decisionSucceeded = Transitions[i].Decision.Decide(controller);
            if (decisionSucceeded)
            {
                controller.TransitionToState(Transitions[i].TrueState);
            }
            else
            {
                controller.TransitionToState(Transitions[i].FalseState);
            }
        }
    }
}
