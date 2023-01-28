using System;
using UnityEngine;

public class GameData : GSC<GameData>
{
    public static event Action<int> OnChangeSpeed;


    public Color[] COLORS = new Color[10];


    private int typeGamePlay = 0; //0 - лоадер, 1 - туториал, 3 - тест, 2 - соло, 4 - пвп, 5 - майнменю
    private int gameSpeed = 1;
    public bool IsConnected { get; set; }

    public bool IsPayForPlay { get; set; }

    public bool IsPrepareStage { get; set; }

    public bool IsCameraMove { get; set; }

    public bool IsGameStart { get; set; }

    public bool IsPVE { get; set; }

    public int GetTypeGameplay() {
        return typeGamePlay;
    }

    public int GetGameSpeed()
    {
        return gameSpeed;
    }

    public void SetTypeGameplay(int _type)
    {
        typeGamePlay = _type;
    }

    public void SetGameSpeed(int _speed)
    {
        gameSpeed = _speed;
        OnChangeSpeed?.Invoke(gameSpeed);
    }
    
    public void SetSpeedGame(int _state)
    {
        
        if (_state == 0)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = _state;
        }
        

     }

    public void Pause()
    {
        SetGameSpeed(0);
    }

    public bool IsBD { get; set; }

    public bool IsCheckVersion { get; set; }

    public bool IsFiles { get; set; }

    public bool IsFirstRun { get; set; }

    public bool IsCanOnline { get; set; }

    private void Start()
    {
        IsCanOnline = true;
    }

}
