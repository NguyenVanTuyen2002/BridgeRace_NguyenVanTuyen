using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorDataSO", menuName = "ScriptableObjects/ColorDataSO", order = 1)]
public class ColorDataSO : ScriptableObject
{
    [SerializeField] Material[] mats;

    public Material GetMat(ColorType type)
    {
        return mats[(int)type];
    }
}
