using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Win : UICanvas
{
    public void RetryButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevelButton()
    {
        LevelManager.Ins.NextLevel();
        GameManager.ChangeState(GameState.Playing);
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI<GamePlay>();
    }
}
