using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Win : UICanvas
{
    public void MainMenuButton()
    {
        UIManager.Ins.OpenUI<MianMenu>();
        Close(0);
    }

    public void RetryButton()
    {
        Time.timeScale = 1;

        LevelManager.Ins.LoadLevel();

        GameManager.ChangeState(GameState.Playing);

        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI<GamePlay>();
    }
}
