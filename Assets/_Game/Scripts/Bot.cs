using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    /*public override void OnInit()
    {
        base.OnInit();
        ChangeState(new IdleState());
        SetColor(colorType);
    }*/
    [SerializeField] public NavMeshAgent agent;
    public Transform posRaycastCheckStair;
    //public Transform pos1;
    public Platform currentPlatform;
    public Brick targetBrick;

    private IState<Bot> _currentState;
    private bool isSecondGridActive = false;


    public NavMeshAgent Agent { get => agent; set => agent = value; }
    public List<Brick> bricks;
    public void Move(Vector3 pos)
    {
        // truyen vao vi tri finish
        Agent.SetDestination(pos);
    }

    private void Start()
    {
        ChangeState(new IdleState());
    }
    private void Update()
    {
        if (_currentState != null)
        {
            _currentState.OnExecute(this);
        }
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

        // Kiểm tra biến isSecondGridActive để quyết định danh sách viên gạch nào sẽ được tìm kiếm
        bricks = isSecondGridActive ? currentPlatform.brickListLv2 : currentPlatform.GetBrickByColor(colorType);
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

    protected override void ColliderWithDoor(Collider other)
    {
        if (!other.CompareTag(CacheString.Tag_Door)) return;

        Door doors = Cache.GetDoor(other);

        ActivateBricksWithSameColor(colorType);
        // Khi bot chạm vào cửa, chuyển isSecondGridActive thành true
        isSecondGridActive = true;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.ColliderWithBrick(other);
        ColliderWithDoor(other);
        FindBrick();
    }
}