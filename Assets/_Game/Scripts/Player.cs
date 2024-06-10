using System;
using System.Collections.Generic;
using static System.Drawing.Color;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private float movementSpeed = 8f;
    //[SerializeField] private Animator anim;
    [SerializeField] private MeshRenderer stairRenderer;
    [SerializeField] private Transform posRaycastCheckStair;

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
            anim.SetBool("Running", true);
            Debug.Log("run");
        }
        else
        {
            anim.SetBool("Running", false);
            Debug.Log("idle");

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
        Debug.DrawRay(posRaycast, Vector3.up * 5f, Color.red);
    }
}
