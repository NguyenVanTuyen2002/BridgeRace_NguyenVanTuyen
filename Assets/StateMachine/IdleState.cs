using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Bot>
{
    public void OnEnter(Bot t)
    {
        t.ChangeAnim(CacheString.Anim_Idle);
        t.FindBrick();
    }

    public void OnExecute(Bot t)
    {
        t.ChangeState(new PatrolState());
    }

    public void OnExit(Bot t)
    {

    }
}
