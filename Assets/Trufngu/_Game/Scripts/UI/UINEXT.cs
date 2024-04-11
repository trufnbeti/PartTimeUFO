using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINEXT : UICanvas
{
    public void OnNextLevel() {
        LevelManager.Ins.OnNextLevel();
        CloseDirectly();
    }
}
