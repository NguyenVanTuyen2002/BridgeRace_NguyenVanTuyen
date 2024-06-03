using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int rows = 10;
    public int columns = 10;
    public float spacing = 2.0f;
    public Vector3 startPosition;
    public Brick brickPrefab;
    public List<Brick> brickList;
    public Brick brick;
    void Start()
    {
        SpawnGrid();
        Debug.Log(brickList.Count);
        SetBrickColor();
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
                brick = SimplePool.Spawn<Brick>(brickPrefab, position, Quaternion.identity);
                brickList.Add(brick);
            }
        }
    }
    void SetBrickColor()
    {
        GameManager.Ins.ShuffleList(brickList);
        int z = 0;
        foreach(var i in brickList)
        {
            int k = z % 4;

            switch (k)
            {
                case 0:
                    i.ChangeColor(GameManager.Ins.colorTypes[0]);
                    Debug.Log(k);
                    break;
                case 1:
                    i.ChangeColor(GameManager.Ins.colorTypes[1]);
                    Debug.Log(k);
                    break;
                case 2:
                    i.ChangeColor(GameManager.Ins.colorTypes[2]);
                    Debug.Log(k);
                    break;
                case 3:
                    i.ChangeColor(GameManager.Ins.colorTypes[3]);
                    Debug.Log(k);
                    break;
            }
            z++;
        }
    }
}
