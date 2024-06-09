using System.Collections.Generic;
using UnityEngine;

public class Brick : GameUnit
{
    [SerializeField] Renderer renderer0;
    [SerializeField] ColorDataSO colorDataSO;

    public List<Brick> bricks = new List<Brick>();
    public ColorType colorType;
    //public bool IsInSecondGrid { get; set; }
    private void Start()
    {
        //OnInit();
    }
    public void ChangeColor(ColorType color)
    {
        this.colorType = color;
        renderer0.material = colorDataSO.GetMat(color);
    }

    /*public void OnInit()
    {
        var colors = colorDataSO.GetListColor();
        ChangeColor(colors[0]);
    }*/

    /*public void ActiveColors(ColorType colorType)
    {
        for (int i = 0; i < bricks.Count; i++)
        {
            if (bricks[i].colorType == colorType)
            {
                bricks[i].gameObject.SetActive(true);
            }
        }
    }*/
}
