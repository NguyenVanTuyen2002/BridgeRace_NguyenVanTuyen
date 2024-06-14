using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState<Bot>
{
    public float timer;
    public float randomTime;

    public void OnEnter(Bot t)
    {
        Vector3 targetPos = t.FindNearestBrick();
        t.Agent.SetDestination(targetPos);
        t.ChangeAnim(CacheString.Anim_Run);
        
        timer = 0;
        randomTime = Random.Range(5f, 10f);
        //t.ChangeStateToPatrolState(randomTime);
        t.Move(t.targetBrick.TF.position);
    }

    public void OnExecute(Bot t)
    {
        if (t.IsEnoughBrick(15))
        {
            t.ChangeState(new BotToFinish());
        }
        else
        {
            t.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Bot t)
    {

    }
}
/*if (t.IsOutOfTime(timer))
        {
            t.ChangeState(new BotToFinish());
        }
        else
        {
            t.ChangeState(new PatrolState());
        }*/

/*if (timer > randomTime)
{
    t.ChangeState(new BotToFinish());
    Debug.Log("tim duong");
}

timer += Time.deltaTime;
    Debug.Log("het time");*/