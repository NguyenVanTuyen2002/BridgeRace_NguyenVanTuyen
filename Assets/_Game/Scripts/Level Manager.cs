using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int rows = 10; // Số hàng
    public int columns = 10; // Số cột
    public float spacing = 2.0f; // Khoảng cách giữa các đối tượng
    public Vector3 startPosition; // Vị trí bắt đầu của lưới
    public Brick brickPrefab;
    void Start()
    {
        SpawnGrid();
    }

    void SpawnGrid()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                // Tính toán vị trí
                Vector3 position = startPosition + new Vector3(i * spacing, 0, j * spacing);
                // Tạo đối tượng
                Brick brick = SimplePool.Spawn<Brick>(brickPrefab, position, Quaternion.identity);
                ColorType randomColor = (Random.value > 0.5f) ? ColorType.Red : ColorType.Black;
                brick.ChangeColor(randomColor); 
            }
        }
    }
}
