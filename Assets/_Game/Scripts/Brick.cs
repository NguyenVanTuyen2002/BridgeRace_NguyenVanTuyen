using UnityEngine;

public class Brick : GameUnit
{
    [SerializeField] Renderer renderer0;
    [SerializeField] ColorDataSO colorDataSO;
    public ColorType colorType;

    private void Start()
    {
        
    }
    public void ChangeColor(ColorType color)
    {
        this.colorType = color;
        renderer0.material = colorDataSO.GetMat(color);
    }
}
