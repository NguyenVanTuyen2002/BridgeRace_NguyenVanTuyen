using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Drawing;

public class Player : Character
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private float movementSpeed = 8f;
    [SerializeField] private MeshRenderer stairRenderer;
    [SerializeField] private Transform posRaycastCheckStair;

    public bool isMoving;

    private float _v;
    private float _h;
    private Vector3 _moveMent;

    private void Start()
    {
        OnInit();
    }

    void Update()
    {
        Move();
        CheckStair();
    }

    public void OnInit()
    {
        TF.rotation = Quaternion.Euler(0, 0, 0);
        ClearBrick();
        gameObject.SetActive(true);
    }

    private void Move()
    {
        _v = joystick.Vertical;
        _h = joystick.Horizontal;
        Vector3 rot = new Vector3(_h, 0, _v).normalized;
        _moveMent = new Vector3(_h, 0, _v) * Time.deltaTime * movementSpeed;

        if (!_moveMent.Equals(Vector3.zero))
        {
            ChangeAnim(CacheString.Anim_Run);
        }
        else
        {
            ChangeAnim(CacheString.Anim_Idle);
        }

        transform.Translate(_moveMent, Space.World);
        transform.rotation = Quaternion.LookRotation(rot);
    }

    private void CheckStair()
    {
        // raycast
        Vector3 posRaycast = posRaycastCheckStair.position;

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
                    if (brickBridge.colorType == ColorType.None || brickBridge.colorType != this.colorType && _v > 0)
                    {
                        brickBridge.ChangeColor(this.colorType);
                        brickBridge.SetActiceStairs();
                        RemoveBrick();
                    }
                }
                // không có gạnh trên người 
                else
                {
                    float rotationY = transform.rotation.eulerAngles.y;
                    if (brickBridge.colorType != this.colorType && _v > 0)
                    {
                        movementSpeed = 0;
                    }
                    else if (_v < 0)
                    {
                        movementSpeed = 8f;
                    }
                }
            }
        }
        // Vẽ raycast trong Scene view để dễ hình dung
        Debug.DrawRay(posRaycast, Vector3.up * 5f, UnityEngine.Color.red);
    }

    protected void ColliderWithFinishPoint(Collider other)
    {
        if (!other.CompareTag(CacheString.Tag_Finish)) return;
        Finish finish = Cache.GetFinish(other);

        if (finish != null)
        {
            movementSpeed = 0;
            foreach (Bot bot in LevelManager.Ins.bots)
            {
                bot.Agent.isStopped = true; // Dừng NavMeshAgent của bot
            }
            //Set player và bot 
            SetPlayerAndBotsOnWin(finish);


            Debug.Log("win");

            //Show Ui Win
            Invoke(nameof(ShowUIWin), 2f);
        }
    }

    private void SetPlayerAndBotsOnWin(Finish finish)
    {

        //Set Player
        //isMoving = false;
        ChangeAnim(CacheString.Anim_Idle);
        GameManager.ChangeState(GameState.Win);

        finish.finishColonms[0].ChangeColor(colorType);
        TF.position = finish.finishColonms[0].GetPoint();
        TF.rotation = Quaternion.Euler(0, 180f, 0);
        ClearBrick();


        //Set Bot
        LevelManager.Ins.ChangeStateWinnerState();
        List<Bot> botCtls = LevelManager.Ins.bots;
        for (int i = 0; i < 2; i++)
        {
            finish.finishColonms[i + 1].ChangeColor(botCtls[i].colorType);
            botCtls[i].TF.position = finish.finishColonms[i + 1].GetPoint();
            botCtls[i].TF.rotation = Quaternion.Euler(0, 180f, 0);
            botCtls[i].ClearBrick();
        }
    }

    private void ShowUIWin()
    {
        UIManager.Ins.OpenUI<Win>();
        UIManager.Ins.CloseUI<GamePlay>();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.ColliderWithBrick(other);
        base.ColliderWithDoor(other);
        ColliderWithFinishPoint(other);
    }
}
