using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int rows = 10;
    public int columns = 10;
    public float spacing = 2.0f;
    public Vector3 startPosition;
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
