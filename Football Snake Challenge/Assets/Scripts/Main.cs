using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public static Main Instance;

    public enum TypeGame
    {
        Liner,
        FreeMove
    }

    public static Action OnTakeBonus;
    public static Action OnStartGame;
    public static Action OnGameOver;
    public static Action OnResetGame;

    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _balanceText;

    public static ScorePlayer _scorePlayer;
    [SerializeField] private ResultScreen _resultScreen;
    [SerializeField] private RecordsScreen _recordsScreen;

    private TypeGame _currentTypeGame;

    [SerializeField] private GameObject _joystick;
    [SerializeField] private GameObject _buttonsControl;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void ChoiceTypeControl(TypeGame typeGame)
    {
        switch (typeGame)
        {
            case TypeGame.Liner:
                _joystick.SetActive(false);
                _buttonsControl.SetActive(true);
                break;
            case TypeGame.FreeMove:
                _joystick.SetActive(true);
                _buttonsControl.SetActive(false);
                break;
        }
    }

    private void Start()
    {
        _scorePlayer = new ScorePlayer(_balanceText, _scoreText);

        OnTakeBonus += TakeBonus;
        OnGameOver += GameOver;
        OnStartGame += GameStart;
    }

    private void OnDestroy()
    {
        OnTakeBonus -= TakeBonus;
        OnGameOver -= GameOver;
        OnStartGame -= GameStart;
    }

    private void GameStart()
    {
        _scorePlayer.StartGameScore();
        TimeHandler.Instance.StartTime();
    }

    private void TakeBonus()
    {
        _scorePlayer.AddScoreGame();
    }

    private void GameOver()
    {
        int resultScore = _scorePlayer.GetScore();
        float timeresult = TimeHandler.Instance.GetValueStopTime();

        bool isNew = _recordsScreen.CheckRecord(resultScore, timeresult);

        if (isNew)
            _resultScreen.ShowResult(ResultScreen.TypeResult.RecordNew, resultScore, timeresult);
        else
            _resultScreen.ShowResult(ResultScreen.TypeResult.None, resultScore, timeresult);

    }
}
