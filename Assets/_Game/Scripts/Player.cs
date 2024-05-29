using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private float movementSpeed = 8f;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Canvas inputCanvas;
    [SerializeField] private Animator anim;
    [SerializeField] private Renderer renderer0;
    [SerializeField] private Renderer renderer1;
    [SerializeField] private Renderer renderer2;
    [SerializeField] private Renderer renderer3;
    [SerializeField] private Renderer renderer4;
    [SerializeField] private Renderer renderer5;
    [SerializeField] private ColorDataSO colorDataSO;
    [SerializeField] private BrickCharacter brickCharacterPrefab;
    [SerializeField] private List<BrickCharacter> brickCharactors = new List<BrickCharacter>();
    [SerializeField] private Transform brickContainer;
    //private bool onStairs = false;
    Vector3 moveMent;

    protected ColorType colorType;
    private float height;

    private void Start()
    {
         ChangeColor(ColorType.Red);
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        float v = joystick.Vertical;
        float h = joystick.Horizontal;
        Vector3 rot = new Vector3(-h, 0, -v).normalized;
        moveMent = new Vector3(-h, 0, -v) * Time.deltaTime * movementSpeed;

        if (moveMent!= Vector3.zero)
        {
            anim.SetBool("Running", true);
            Debug.Log("run");
        }
        else
        {
            anim.SetBool("Running", false);
            Debug.Log("idle");

        }

        transform.Translate(moveMent, Space.World);
        transform.rotation = Quaternion.LookRotation(rot);
    }

    public void ChangeColor(ColorType color)
    {
        this.colorType = color;
        renderer0.material = colorDataSO.GetMat(color);
        renderer1.material = colorDataSO.GetMat(color);
        renderer2.material = colorDataSO.GetMat(color);
        renderer3.material = colorDataSO.GetMat(color);
        renderer4.material = colorDataSO.GetMat(color);
        renderer5.material = colorDataSO.GetMat(color);
    }

    void OnTriggerEnter(Collider other)
    {
        // Kiểm tra va chạm với cầu thang
        CollideWithStair(other);
        ColliderWithBrick(other);
    }

    private void ColliderWithBrick(Collider other)
    {
        if (!other.CompareTag(CacheString.Tag_Brick)) return;
        Brick brick = Cache.GetBrick(other);
        if (brick.colorType != colorType) return;
        other.gameObject.SetActive(false);

        height = 0.3f;
        BrickCharacter newBrick = Instantiate(brickCharacterPrefab, brickContainer);
        brickCharactors.Add(newBrick);
        newBrick.ChangeColor(colorDataSO.GetMat(colorType));
        newBrick.transform.localPosition = brickCharactors.Count * height * Vector3.up;
        
    }

    private void CollideWithStair(Collider other)
    {
        if (!other.CompareTag(CacheString.Tag_Stairs)) return;

    }

    void OnTriggerExit(Collider other)
    {
        // Kiểm tra khi rời khỏi cầu thang
        if (other.CompareTag(CacheString.Tag_Brick))
        {
            //onStairs = false;
        }
    }
}
