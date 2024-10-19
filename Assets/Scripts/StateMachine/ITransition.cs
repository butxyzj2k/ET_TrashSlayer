using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface ITransition
{
    IState To { get; }
    IPredicate Condition { get; }
}