using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MianMenu : UICanvas
{
    public void PlayButton()
    {
        UIManager.Ins.OpenUI<GamePlay>();
        LevelManager.Ins.LoadLevel();
        Close(0);
    }
}
