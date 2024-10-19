using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class StateMachine
{
    private StateNode currentState;
    private Dictionary<IState, StateNode> nodes = new();
    private List<ITransition> anyTransitions = new();
            
    public void StateMachineUpdate(){
        var transition = GetTransition();

        if (transition != null){
            ChangeStateNode(transition);
        }

        currentState.State.StateUpdate();
    }

    public void StateMachineFixedUpdate(){
        var transition = GetTransition();

        if (transition != null){
            ChangeStateNode(transition);
        }

        currentState.State.StateFixedUpdate();
    }

    ITransition GetTransition() {
        foreach (var transition in anyTransitions)
            if (transition.Condition.Evaluate())
                return transition;
            
        foreach (var transition in currentState.Transitions)
            if (transition.Condition.Evaluate())
                return transition;
            
        return null;
    }

    public void SetStateNode(IState state){
        currentState = nodes[state];
    }

    void ChangeStateNode(ITransition transition){
        if(currentState.State == transition.To) return;

        var oldState = currentState.State;
        var newState = transition.To;

        oldState.OnExitState();
        newState.OnEnterState();

        currentState = nodes[newState];
    }

    public void AddTransition(IState from, IState to, IPredicate condition){
        GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, condition);
    }

    public void AddAnyTransition(IState to, IPredicate condition) {
        anyTransitions.Add(new Transition(GetOrAddNode(to).State, condition));
    }

    StateNode GetOrAddNode(IState state){

        if (!nodes.TryGetValue(state, out StateNode node))
        {
            node = new StateNode(state);
            nodes.Add(state, node);
        }

        return node;
    }
}