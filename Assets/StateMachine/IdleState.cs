using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Bot>
{
    public void OnEnter(Bot t)
    {

    }

    public void OnExecute(Bot t)
    {
        t.FindBrick();
        t.ChangeState(new PatrolState());
    }

    public void OnExit(Bot t)
    {

    }
}
