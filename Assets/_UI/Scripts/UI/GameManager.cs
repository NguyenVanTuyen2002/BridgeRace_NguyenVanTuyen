using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    //[SerializeField] UserData userData;
    //[SerializeField] CSVData csv;
    //private static GameState gameState = GameState.MainMenu;

    public ColorDataSO colorDataSO;
    public List<ColorType> colorTypes = new List<ColorType>();

    // Start is called before the first frame update
    protected void Awake()
    {
        OnInit();
        //base.Awake();
        //Input.multiTouchEnabled = false;
        //Application.targetFrameRate = 60;
        //Screen.sleepTimeout = SleepTimeout.NeverSleep;

        //int maxScreenHeight = 1280;
        //float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        //if (Screen.currentResolution.height > maxScreenHeight)
        //{
        //    Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        //}

        //csv.OnInit();
        //userData?.OnInitData();

        //ChangeState(GameState.MainMenu);

        //UIManager.Ins.OpenUI<MianMenu>();
    }

    //public static void ChangeState(GameState state)
    //{
    //    gameState = state;
    //}

    //public static bool IsState(GameState state)
    //{
    //    return gameState == state;
    //}

    /*private void OnInit()
    {
        colorTypes = colorDataSO.GetListColor();
        // Assign the first color to the player
        //player.SetColor(colorTypes[0]);
        // Assign the second color to the bot
        //bot.SetColor(colorTypes[1]);
    }*/

    private void OnInit()
    {
        colorTypes = colorDataSO.GetRandomEnumColors(4);
        //AssignColorsToCharacters();
    }

    /*private void AssignColorsToCharacters()
    {
        // Lấy danh sách màu từ ColorDataSO
        var colors = colorDataSO.GetRandomEnumColors(4);

        // Gán màu cho player và bot
        LevelManager.Ins.player.SetColor(colors[0]);

        for (int i = 0; i < LevelManager.Ins.bots.Count; i++)
        {
            LevelManager.Ins.bots[i].SetColor(colors[i + 1]);
        }
    }*/
}
