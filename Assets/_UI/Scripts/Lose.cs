using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lose : UICanvas
{
    public void RetryButton()
    {
        Time.timeScale = 1;

        LevelManager.Ins.LoadLevel();

        GameManager.ChangeState(GameState.Playing);

        UIManager.Ins.CloseAll();

        UIManager.Ins.OpenUI<GamePlay>();
    }
}
