using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishColumn : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Transform point;

    public void ChangeColor(ColorType color)
    {
        meshRenderer.material = LevelManager.Ins.colorDataSO.GetMat(color);
    }

    public Vector3 GetPoint()
    {
        return point.position;
    }
}
