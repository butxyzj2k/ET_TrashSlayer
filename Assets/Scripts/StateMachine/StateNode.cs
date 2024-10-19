using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StateNode
{
    public IState State { get; }
    public List<ITransition> Transitions { get; }
            
    public StateNode(IState state) {
        State = state;
        Transitions = new List<ITransition>();
    }
            
    public void AddTransition(IState to, IPredicate condition) {
        Transitions.Add(new Transition(to, condition));
    }    
}