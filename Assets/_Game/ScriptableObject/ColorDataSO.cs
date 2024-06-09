using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorDataSO", menuName = "ScriptableObjects/ColorDataSO", order = 1)]
public class ColorDataSO : ScriptableObject
{
    [SerializeField] Material[] mats;

    public Material GetMat(ColorType type)
    {
        return mats[((int)type)];
    }

    public List<ColorType> GetRandomEnumColors(int numberOfColors)
    {
        // Lấy tất cả các giá trị của enum, loại bỏ giá trị None
        List<ColorType> colorValues = new List<ColorType>((ColorType[])Enum.GetValues(typeof(ColorType)));
        colorValues.Remove(ColorType.None);
        List<ColorType> selectedColors = new List<ColorType>();
        System.Random random = new System.Random();

        while (selectedColors.Count < numberOfColors && colorValues.Count > 0)
        {
            int randomIndex = random.Next(0, colorValues.Count);

            // Chọn màu ngẫu nhiên và thêm vào danh sách
            selectedColors.Add(colorValues[randomIndex]);
            // Loại bỏ màu đã chọn để không chọn lại
            colorValues.RemoveAt(randomIndex);
        }

        return selectedColors;
    }
}
