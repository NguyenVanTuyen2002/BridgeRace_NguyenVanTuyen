using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState<Bot>
{
    public float timer;
    public float randomTime;

    public void OnEnter(Bot t)
    {
        //Vector3 targetPos = t.FindNearestBrick();
        //t.Agent.SetDestination(targetPos);  
        t.ChangeAnim(CacheString.Anim_Run);
        
        timer = 0;
        randomTime = Random.Range(5f, 10f);
        t.ChangeStateToPatrolState(randomTime);
    }

    public void OnExecute(Bot t)
    {
        t.Move(t.targetBrick.TF.position);

        if (t.IsOutOfTime(timer))
        {

        }

        if (timer > randomTime)
        {
            t.ChangeState(new BotToFinish());
            Debug.Log("tim duong");
        }
        timer += Time.deltaTime;
            Debug.Log("het time");

        //if (t.IsEnoughBrick(10))
        //{
        //    t.ChangeState(new BotToFinish());
        //}
        //else
        //{
        //    t.ChangeState(new PatrolState());
        //}

    }

    public void OnExit(Bot t)
    {

    }

}
