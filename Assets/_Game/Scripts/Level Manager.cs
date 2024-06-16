using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public Player player;
    public ColorDataSO colorDataSO;
    public List<ColorType> colorTypes = new List<ColorType>();
    public List<Level> levels = new List<Level>();
    public List<Bot> bots = new List<Bot>();
    public Level currentLevel;
    public int indexCurrentLevel;

    private void Awake()
    {
        colorTypes = colorDataSO.GetRandomEnumColors(4);
        //PlayerPrefs.SetInt("Current_Level", 0);
    }

    private void Start()
    {
        player.SetColor(colorTypes[0]);
        player.gameObject.SetActive(false);
        SpawnBot(colorTypes, 3);
        indexCurrentLevel = PlayerPrefs.GetInt("Current_Level");
    }

    public void LoadLevel()
    {
        if (currentLevel != null) Destroy(currentLevel.gameObject);

        currentLevel = Instantiate(levels[indexCurrentLevel], transform);

        //currentLevel.ShuffleListStartPoint();

        //ChangeStateWinnerState();
        //set vị trí cho player và bot
        setPlayerAndBot();

    }

    public void setPlayerAndBot()
    {
        //Invoke(nameof(SetPosBot), 0);
        //Invoke(nameof(SetPosPlayer), 0);
        player.OnInit();
        player.TF.position = currentLevel.startPoint[0].position;
        for (int i = 1; i < currentLevel.startPoint.Count; i++)
        {
            bots[i - 1].TF.position = currentLevel.startPoint[i].position;
            bots[i - 1].OnInit();
        }
    }

    public void NextLevel()
    {
        Platform platform = FindObjectOfType<Platform>();
        platform.DelBrick();

        if (currentLevel != null) Destroy(currentLevel.gameObject);

        indexCurrentLevel++;
        Debug.Log(indexCurrentLevel);

        if (indexCurrentLevel >= 3)
        {
            indexCurrentLevel = 0;
        }

        PlayerPrefs.SetInt("Current_Level", indexCurrentLevel);

        currentLevel = Instantiate(levels[indexCurrentLevel], transform);

        //currentLevel.ShuffleListStartPoint();

        //ChangeStateWinnerState();

        //set vị trí cho player và bot
        setPlayerAndBot();



    }

    /*public void ChangeStateWinnerState()
    {
        for (int i = 0; i < bots.Count; i++)
        {
            bots[i].ChangeState(new WinnerState());
        }
    }*/

    public void SpawnBot(List<ColorType> colors, int quantityBot)
    {
        Debug.Log("sinh bot");
        for (int i = 0; i < quantityBot; i++)
        {
            Bot bot = SimplePool.Spawn<Bot>(PoolType.Bot, transform);
            bot.gameObject.SetActive(false);
            bot.SetColor(colors[i + 1]);
            bots.Add(bot);
        }
    }
}