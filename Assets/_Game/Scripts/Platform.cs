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
    public List<Brick> brickList = new List<Brick>();
    public List<Brick> brickListLv2 = new List<Brick>();
    public Brick brick;
    public List<ColorType> colorTypes = new List<ColorType>();
    public ColorDataSO colorDataSO;

    private void Awake()
    {
        //colorTypes = colorDataSO.GetRandomEnumColors(4);
    }
    void Start()
    {
        SpawnGrid(startPosition, brickList); // Bricks tại startPosition sẽ được kích hoạt
        SpawnGrid(startPosition_2, brickListLv2); // Bricks tại startPosition_2 sẽ được kích hoạt
        SetBrickColor();

        SetBricksInactive(brickListLv2);
    }

    public void SetBricksInactive(List<Brick> bricks)
    {
        foreach (var brick in bricks)
        {
            brick.gameObject.SetActive(false);
        }
    }

    public void SpawnGrid(Vector3 startPosition, List<Brick> brickList)
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

    public void DelBrick()
    {
        foreach(var i in brickList)
        {
            Destroy(i.gameObject);
            Debug.Log("aaaa");
        }

        foreach (var i in brickListLv2)
        {
            Destroy(i.gameObject);
        }

        brickList.Clear();
        brickListLv2.Clear();
    }

    public void SetBrickColor()
    {
        ShuffleList(brickList);
        ShuffleList(brickListLv2);

        int z = 0;
        ColorBricks(brickList, ref z);
        ColorBricks(brickListLv2, ref z);
    }

    public void ColorBricks(List<Brick> bricks, ref int z)
    {
        foreach (var brick in bricks)
        {
            int k = z % 4;
            brick.ChangeColor(LevelManager.Ins.colorTypes[k]);
            //brick.ChangeColor(colorTypes[k]);
            //brick.ChangeColor();
            z++;
        }
    }

    public List<T> ShuffleList<T>(List<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
        return list;
    }

    public List<Brick> GetBrickByColor(ColorType type, bool useSecondList = false)
    {
        List<Brick> bricks = new List<Brick>();
        List<Brick> targetList = useSecondList ? brickListLv2 : brickList;

        for (int i = 0; i < targetList.Count; i++)
        {
            if (targetList[i].colorType == type)
            {
                bricks.Add(targetList[i]);
            }
        }
        return bricks;
    }
}
