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
        renderer0.enabled = false;
    }
    
    public void SetActiceStairs()
    {
        if (renderer0.enabled == false)
        {
            renderer0.enabled = true;
        }
    }

    public void ChangeColor(ColorType color)
    {
        this.colorType = color;
        renderer0.material = colorDataSO.GetMat(color);
    }
}
