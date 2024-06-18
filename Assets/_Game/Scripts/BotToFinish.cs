using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotToFinish : IState<Bot>
{
    public void OnEnter(Bot t)
    {

    }

    public void OnExecute(Bot t)
    {
        Vector3 pointNavMesh = LevelManager.Ins.currentLevel.finishPoint.position;
        t.Move(pointNavMesh);
        t.CheckStair();
        if (t.brickCharactors.Count <= 0)
        {
            t.agent.velocity = Vector3.zero;

            t.ChangeState(new IdleState());
        }
    }

    public void OnExit(Bot t)
    {

    }
}