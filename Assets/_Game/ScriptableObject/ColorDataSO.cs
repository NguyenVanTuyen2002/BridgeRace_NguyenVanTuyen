using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorDataSO", menuName = "ScriptableObjects/ColorDataSO", order = 1)]
public class ColorDataSO : ScriptableObject
{
    [SerializeField] Material[] mats;

    public Material GetMat(ColorType type)
    {
        return mats[(int)type];
    }
    public List<ColorType> GetListColor()
    {
        var colors = Enum.GetValues(typeof(ColorType))
                         .Cast<ColorType>()
                         .Where(c => c != ColorType.None)
                         .ToList();
        var randomColors = GameManager.Ins.ShuffleList(colors);

        // Select the first four colors
        var selectedColors = randomColors.Take(4).ToList();
        return selectedColors;
    }
    
}
