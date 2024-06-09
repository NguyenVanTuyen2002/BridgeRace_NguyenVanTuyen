using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState<Bot>
{
    public float timer;
    public float randomTime;

    public void OnEnter(Bot t)
    {
        t.ChangeAnim(CacheString.Anim_Run);
        timer = 0;
        randomTime = Random.Range(10f, 20f);
    }

    public void OnExecute(Bot t)
    {
        t.Move(t.targetBrick.TF.position);

        if (timer > randomTime)
        {
            t.ChangeState(new BotToFinish());
            Debug.Log("tim duong");
        }
        timer += Time.deltaTime;
            Debug.Log("het time");

    }

    public void OnExit(Bot t)
    {

    }

}
