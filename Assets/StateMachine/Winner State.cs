using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerState : IState<Bot>
{
    public void OnEnter(Bot t)
    {
        t.SetMove(false);
        t.IsActiveAgent(false);
    }

    public void OnExecute(Bot t)
    {
        t.ChangeAnim(CacheString.Anim_Idle);
    }

    public void OnExit(Bot t)
    {

    }

}

