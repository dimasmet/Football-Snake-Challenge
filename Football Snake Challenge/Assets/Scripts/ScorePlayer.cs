using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePlayer
{
    [SerializeField] private Text _balanceText;
    [SerializeField] private Text _scoreCurrentGameText;

    private int _balanceInt;
    private int _scoreInt;

    public ScorePlayer(Text balanceText, Text scoreText)
    {
        _balanceText = balanceText;
        _scoreCurrentGameText = scoreText;

        _balanceInt = PlayerPrefs.GetInt("Balance Player");

#if UNITY_EDITOR
        _balanceInt = 100;
#endif

        UpdateBalanceTextField();
    }

    private void UpdateBalanceTextField()
    {
        _balanceText.text = _balanceInt.ToString();
        PlayerPrefs.SetInt("Balance Player", _balanceInt);
    }

    private void UpdateScoreCurrentTextField()
    {
        _scoreCurrentGameText.text = _scoreInt.ToString();
    }

    public void StartGameScore()
    {
        _scoreInt = 0;
        UpdateScoreCurrentTextField();
    }

    public void AddScoreGame()
    {
        _scoreInt++;
        UpdateScoreCurrentTextField();
    }

    public void AddBalancePlayer(int value)
    {
        _balanceInt += value;
        UpdateBalanceTextField();
    }

    public int GetScore()
    {
        return _scoreInt;
    }

    public int GetBalance()
    {
        return _balanceInt;
    }

    public void DiscreaseBalance(int value)
    {
        Debug.Log(value);
        _balanceInt -= value;
        UpdateBalanceTextField();
    }
}
