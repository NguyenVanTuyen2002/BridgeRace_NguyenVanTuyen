using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MianMenu : UICanvas
{
    public void PlayButton()
    {
        //UIManager.Ins.CloseAll();

        GameManager.ChangeState(GameState.Playing);
        LevelManager.Ins.LoadLevel();

        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI<GamePlay>();
    }
}
