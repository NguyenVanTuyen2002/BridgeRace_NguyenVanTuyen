using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public Player player;
    public ColorDataSO colorDataSO;
    public List<ColorType> colorTypes = new List<ColorType>();
    public List<Bot> bots = new List<Bot>();
    public Level currentLevel;

    private void Awake()
    {
        colorTypes = colorDataSO.GetRandomEnumColors(4);
    }

    private void Start()
    {
        player.SetColor(colorTypes[0]);
        SpawnBot(colorTypes, 3);
    }

    public void SpawnBot(List<ColorType> colors, int quantityBot)
    {
        for (int i = 0; i < quantityBot; i++)
        {
            Bot bot = SimplePool.Spawn<Bot>(PoolType.Bot, transform);
            bot.gameObject.SetActive(true);
            bot.SetColor(colors[i + 1]);
            bots.Add(bot);
        }
    }
}