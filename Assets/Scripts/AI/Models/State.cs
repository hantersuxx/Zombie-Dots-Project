using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/State")]
public class State : ScriptableObject
{
    [SerializeField]
    private Action[] actions;
    public Action[] Actions { get => actions; private set => actions = value; }

    [SerializeField]
    private Transition[] transitions;
    public Transition[] Transitions { get => transitions; private set => transitions = value; }

    [SerializeField]
    private Color sceneGizmoColor;
    public Color SceneGizmoColor { get => sceneGizmoColor; private set => sceneGizmoColor = value; }

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
