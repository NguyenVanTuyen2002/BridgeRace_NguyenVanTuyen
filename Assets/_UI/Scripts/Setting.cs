using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Setting : UICanvas
{
    public void ContinueButton()
    {
        Time.timeScale = 1;
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
