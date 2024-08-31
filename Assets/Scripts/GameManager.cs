using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private State _state;
    
    private enum State
    {
        WaitingToStart,
        GamePlaying,
        GameOver
    }
    
    private void Awake()
    {
        _state = State.WaitingToStart;
        
        if (!PlayerPrefs.HasKey(SaveDataType.BestScore))
            PlayerPrefs.SetInt(SaveDataType.BestScore, 0);
    }

    private void Start()
    {
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
        Bird.Instance.OnGameOver += Bird_OnGameOver;
        Bird.Instance.OnScoreChanged += Bird_OnScoreChanged;
    }

    private void ChangeState(State state)
    {
        _state = state;
    }

    private void TryUpdateBestScore(int score)
    {
        if (score> PlayerPrefs.GetInt(SaveDataType.BestScore))
            PlayerPrefs.SetInt(SaveDataType.BestScore, score);
    }
    
    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (_state != State.WaitingToStart)
            return;
        
        LevelUI.Instance.ShowGetReadyImageAnimation();
        
        ChangeState(State.GamePlaying);
    }
    
    private void Bird_OnGameOver(object sender, EventArgs e)
    {
        ChangeState(State.GameOver);
    }
    
    private void Bird_OnScoreChanged(object sender, Bird.OnScoreChangedEventArgs e)
    {
        TryUpdateBestScore(e.Score);
    }
}