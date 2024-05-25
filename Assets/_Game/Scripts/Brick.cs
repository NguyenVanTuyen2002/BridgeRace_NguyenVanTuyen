using UnityEngine;

public class Brick : GameUnit
{
    public GameUnit brickPrefab; // The prefab to be spawned
    /*public int rows = 10; // Number of rows
    public int columns = 10; // Number of columns
    public float spacing = 2.0f; // Spacing between objects
    public Vector3 startPosition; // Starting position of the grid

    void Start()
    {
        SimplePool.Preload(brickPrefab, rows * columns, transform, true);
        SpawnGrid();
    }

    void SpawnGrid()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Vector3 position = startPosition + new Vector3(i * spacing, 0, j * spacing);
                Brick brick = SimplePool.Spawn<Brick>(brickPrefab, position, Quaternion.identity);
                brick.transform.SetParent(transform);
            }
        }
    }*/
}
