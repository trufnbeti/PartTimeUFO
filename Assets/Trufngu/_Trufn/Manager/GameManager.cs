using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
    private GameState _gameState;
    public void ChangeState(GameState state)
    {
        _gameState = state;
    }
    public bool IsState(GameState state) => _gameState == state;
    private void Awake() {
        ChangeState(GameState.MainMenu);
        // Tranh viec nguoi choi cham da diem vao man hinh
        Input.multiTouchEnabled = false;
        // Target frame rate ve 60 fps
        Application.targetFrameRate = 60;
        // Tranh viec tat man hinh
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        // Xu tai tho
        int maxScreenHeight = 1920;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight) {
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        }
        
        // UserData.Ins.OnInitData();
    }

    private void Start() {
        // UIManager.Ins.OpenUI<UIMainMenu>();
    }
}
