using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    [SerializeField] public NavMeshAgent agent;
    public Transform posRaycastCheckStair;
    public Platform currentPlatform;
    public Brick targetBrick;

    private IState<Bot> _currentState;
    private bool isSecondGridActive = false;
    private IdleState idleState;
    private BotToFinish botToFinishState;

    public IdleState IdleState { get { return idleState; } }    
    public NavMeshAgent Agent { get => agent; set => agent = value; }
    public List<Brick> bricks;
    public bool isMove;

    private void Start()
    {
        OnInit();
        idleState = new IdleState();    
        ChangeState(new IdleState());
    }
    private void Update()
    {
        if (_currentState != null)
        {
            _currentState.OnExecute(this);
        }
    }

    private void ChangeIdleState()
    {
        ChangeState(idleState);
    }

    public void OnInit()
    {
        TF.rotation = Quaternion.Euler(0, 0, 0);
        isMove = true;
        Agent.enabled = true;

        agent.velocity = agent.velocity.normalized;

        if (LevelManager.Ins.currentLevel != null)
        {
            currentPlatform = LevelManager.Ins.currentLevel.platforms[0];
        }

        gameObject.SetActive(true);
        ClearBrick();

        ChangeAnim(CacheString.Anim_Idle);
        Invoke(nameof(ChangeIdleState), 0f);
        /*SetMove(true);
        IsActiveAgent(true);
        FindBrick();*/
    }

    public void Move(Vector3 pos)
    {
        if (isMove)
        {
            // truyen vao vi tri finish
            Agent.SetDestination(pos);
            ChangeAnim(CacheString.Anim_Run);
        }
    }

    public void StopMovement()
    {
        agent.isStopped = true;
    }

    public void CheckStair()
    {
        // raycast
        Vector3 posRaycast = posRaycastCheckStair.position;

        // Tạo raycast từ vị trí của player xuống dưới theo trục y
        RaycastHit hit;
        if (Physics.Raycast(posRaycast, Vector3.up, out hit, 5f))
        {
            BrickBridge brickBridge = Cache.GetBrickBridge(hit.collider);
            if (brickBridge != null)
            {
                // có gạnh trên người
                if (brickCharactors.Count > 0)
                {
                    // gạch không màu hoặc màu khác player
                    if (brickBridge.colorType == ColorType.None || brickBridge.colorType != this.colorType)
                    {
                        brickBridge.ChangeColor(this.colorType);
                        brickBridge.SetActiceStairs();
                        RemoveBrick();
                    }
                }
            }
        }

        // Vẽ raycast trong Scene view để dễ hình dung
        Debug.DrawRay(posRaycast, Vector3.up * 5f, Color.red);
    }

    public void ChangeState(IState<Bot> state)
    {
        if (_currentState != null)
        {
            _currentState.OnExit(this);
        }

        _currentState = state;

        if (_currentState != null)
        {
            _currentState.OnEnter(this);
        }
    }

    public void FindBrick()
    {
        agent.velocity = agent.velocity.normalized;

        // Sử dụng biến isSecondGridActive để quyết định danh sách viên gạch nào sẽ được tìm kiếm
        bricks = currentPlatform.GetBrickByColor(colorType, isSecondGridActive);
        Debug.Log(isSecondGridActive);
        float minDistance = float.MaxValue;

        for (int i = 0; i < bricks.Count; i++)
        {
            if (bricks[i].gameObject.activeSelf == true)
            {
                float distance = Vector3.Distance(transform.position, bricks[i].transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    targetBrick = bricks[i];
                }
            }
        }
    }

    public Vector3 FindNearestBrick()
    {
        return Vector3.zero;
    }

    protected override void ColliderWithDoor(Collider other)
    {
        if (!other.CompareTag(CacheString.Tag_Door)) return;

        Door doors = Cache.GetDoor(other);

        ActivateBricksWithSameColor(colorType);
        // Khi bot chạm vào cửa, chuyển isSecondGridActive thành true
        isSecondGridActive = true;
    }

    public void ColliderWithFinishPoint(Collider other)
    {
        if (!other.CompareTag(CacheString.Tag_Finish)) return;
        Finish finish = Cache.GetFinish(other);
        ShowUILose();
    }

    private void ShowUILose()
    {
        UIManager.Ins.OpenUI<Lose>();
        UIManager.Ins.CloseUI<GamePlay>();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.ColliderWithBrick(other);
        ColliderWithDoor(other);
        ColliderWithFinishPoint(other);
        FindBrick();
    }

    public bool IsEnoughBrick(int value)
    {
        return brickCharactors.Count > value;
    }

    internal bool IsOutOfTime(float timer)
    {
        return true;
    }

    public void ChangeStateToPatrolState(float randomTime)
    {
        StartCoroutine(CoChangeStateToPatrolState(randomTime));
    }

    private IEnumerator CoChangeStateToPatrolState(float randomTime)
    {
        yield return new WaitForSeconds(randomTime);
        ChangeState(botToFinishState);
    }

    public void IsActiveAgent(bool isActive)
    {
        Agent.enabled = isActive;
    }

    public void SetMove(bool isMove)
    {
        this.isMove = isMove;
    }
}