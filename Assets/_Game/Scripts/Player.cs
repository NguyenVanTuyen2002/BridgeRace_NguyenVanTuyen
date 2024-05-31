using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private float movementSpeed = 8f;
    [SerializeField] private Animator anim;
    [SerializeField] private ColorDataSO colorDataSO;
    [SerializeField] private MeshRenderer stairRenderer;

    [SerializeField] private List<BrickBridge> brickBridges = new List<BrickBridge>();

    //protected ColorType colorType;

    private Vector3 moveMent;

    private void Start()
    {
         //ChangeColor(ColorType.Red);
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

    //public void ChangeColor(ColorType color)
    //{
    //    this.colorType = color;

    //    Material newMaterial = colorDataSO.GetMat(color);
    //    foreach (var renderer in renderers)
    //    {
    //        renderer.material = newMaterial;
    //    }
    //}

    /*void OnTriggerEnter(Collider other)
    {
        // Kiểm tra va chạm với cầu thang
        //CollideWithStair(other);
        //ColliderWithBrick(other);
    }*/

    /*private void ColliderWithBrick(Collider other)
    {
        if (!other.CompareTag(CacheString.Tag_Brick)) return;
        Brick brick = Cache.GetBrick(other);
        if (brick.colorType != colorType) return;
        other.gameObject.SetActive(false);
        height = 0.3f;
        BrickCharacter newBrick = Instantiate(brickCharacterPrefab, brickContainer);
        if (newBrick != null)
        {
            Debug.Log("New brick created successfully.");
            brickCharactors.Add(newBrick);
            newBrick.ChangeColor(colorDataSO.GetMat(colorType));
            newBrick.transform.localPosition = brickCharactors.Count * height * Vector3.up;
            Debug.Log("New brick added to list successfully.");
        }
        else
        {
            Debug.LogError("Failed to create new brick.");
        }
    }*/

    //private void CollideWithStair(Collider other)
    //{
    //    if (!other.CompareTag(CacheString.Tag_Stairs)) return;
    //    Debug.Log("Cầu thang được chạm vào.");
    //    stairRenderer.enabled = true;
    //    // Lấy Renderer của cầu thang
    //    //Renderer stairRenderer = other.GetComponent<Renderer>();
    //    if (stairRenderer.enabled == false)
    //    {
    //        stairRenderer.enabled = true;
    //        // Đổi màu cầu thang thành màu của player
    //        stairRenderer.material = colorDataSO.GetMat(colorType);
    //    }

    //    // Thêm cầu thang vào danh sách brickBridges nếu nó chưa có trong danh sách
    //    BrickBridge brickBridge = 
    //        other.GetComponent<BrickBridge>();
    //    if (brickBridge != null && !brickBridges.Contains(brickBridge))
    //    {
    //        brickBridges.Add(brickBridge);
    //        Debug.Log("Cầu thang được thêm vào danh sách brickBridges.");
    //    }




    //    // Kiểm tra xem danh sách có phần tử nào không
    //    if (brickCharactors.Count == 0)
    //    {
    //        Debug.LogWarning("No bricks in the list to remove.");
    //        return;
    //    }

    //    // Lấy phần tử cuối cùng trong danh sách
    //    BrickCharacter newBrick = brickCharactors[brickCharactors.Count - 1];

    //    // Xóa viên gạch ra khỏi danh sách
    //    brickCharactors.RemoveAt(brickCharactors.Count - 1);

    //    // Hủy đối tượng viên gạch
    //    Destroy(newBrick.gameObject);


    

    /*void OnTriggerExit(Collider other)
    {
        // Kiểm tra khi rời khỏi cầu thang
        if (other.CompareTag(CacheString.Tag_Brick))
        {
            //onStairs = false;
        }
    }*/
}
