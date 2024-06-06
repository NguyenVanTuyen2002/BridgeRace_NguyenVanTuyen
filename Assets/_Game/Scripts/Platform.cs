using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public int rows = 10;
    public int columns = 10;
    public float spacing = 2.0f;
    public Vector3 startPosition;
    public Vector3 startPosition_2;
    public Brick brickPrefab;
    public List<Brick> brickList;
    public List<Brick> brickListLv2;
    public Brick brick;

    void Start()
    {
        brickList = new List<Brick>();
        brickListLv2 = new List<Brick>();

        SpawnGrid(startPosition, brickList); // Bricks tại startPosition sẽ được kích hoạt
        SpawnGrid(startPosition_2, brickListLv2); // Bricks tại startPosition_2 sẽ được kích hoạt
        SetBrickColor();

        SetBricksInactive(brickListLv2);
    }

    void SetBricksInactive(List<Brick> bricks)
    {
        foreach (var brick in bricks)
        {
            brick.gameObject.SetActive(false);
        }
    }

    void SpawnGrid(Vector3 startPosition, List<Brick> brickList)
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
        GameManager.Ins.ShuffleList(brickListLv2);

        int z = 0;
        ColorBricks(brickList, ref z);
        ColorBricks(brickListLv2, ref z);
    }

    void ColorBricks(List<Brick> bricks, ref int z)
    {
        foreach (var brick in bricks)
        {
            int k = z % 4;
            brick.ChangeColor(GameManager.Ins.colorTypes[k]);
            Debug.Log(k);
            z++;
        }
    }
}
