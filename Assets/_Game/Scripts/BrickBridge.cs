using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBridge : MonoBehaviour
{
    [SerializeField] private Renderer renderer0;
    [SerializeField] private ColorDataSO colorDataSO;

    public ColorType colorType;

    private void Start()
    {

    }
    
    public void SetActiceStairs()
    {
        renderer0.enabled = true;
        // Lấy Renderer của cầu thang
        //Renderer stairRenderer = other.GetComponent<Renderer>();
        if (renderer0.enabled == false)
        {
            renderer0.enabled = true;
            // Đổi màu cầu thang thành màu của player
            //renderer0.material = colorDataSO.GetMat(colorType);
        }

        // Thêm cầu thang vào danh sách brickBridges nếu nó chưa có trong danh sách
       /* BrickBridge brickBridge = other.GetComponent<BrickBridge>();
        if (brickBridge != null && !brickBridges.Contains(brickBridge))
        {
            brickBridges.Add(brickBridge);
            Debug.Log("Cầu thang được thêm vào danh sách brickBridges.");
        }*/




        
    }
    public void ChangeColor(Material material)
    {
        renderer0.material = material;
    }
    public void ChangeColor(ColorType color)
    {
        this.colorType = color;
        renderer0.material = colorDataSO.GetMat(color);
    }
}
