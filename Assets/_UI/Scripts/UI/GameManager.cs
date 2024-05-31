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
    public Character player;
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
    private void OnInit()
    {
        colorTypes = colorDataSO.GetListColor();
        player.SetColor(colorTypes[0]);
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
}
