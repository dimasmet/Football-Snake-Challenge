using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreensController : MonoBehaviour
{
    public static ScreensController I;

    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _gamePanel;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _recordsPanel;
    [SerializeField] private GameObject _choiceTypePanel;
    [SerializeField] private GameObject _shopPanel;
    [SerializeField] private GameObject _difficultyPanel;

    [SerializeField] private Button _openStoreBtn;
    [SerializeField] private Button _closeStoreBtn;

    public enum ScreenName
    {
        Menu,
        Game,
        Settings,
        Records,
        TypeGame,
        Shop,
        Difficulty
    }

    private GameObject _currentActive;

    private void Awake()
    {
        if (I == null)
        {
            I = this;
        }

        _openStoreBtn.onClick.AddListener(() =>
        {
            ShowPanel(ScreenName.Shop);
        });

        _closeStoreBtn.onClick.AddListener(() =>
        {
            ShowPanel(ScreenName.Menu);
        });
    }

    private void Start()
    {
        ShowPanel(ScreenName.Menu);
    }

    public void ShowPanel(ScreenName screen)
    {
        if (_currentActive != null) _currentActive.SetActive(false);

        switch (screen)
        {
            case ScreenName.Menu:
                _currentActive = _menuPanel;
                break;
            case ScreenName.Game:
                _currentActive = _gamePanel;
                break;
            case ScreenName.Settings:
                _currentActive = _settingsPanel;
                break;
            case ScreenName.Records:
                _currentActive = _recordsPanel;
                break;
            case ScreenName.TypeGame:
                _currentActive = _choiceTypePanel;
                break;
            case ScreenName.Shop:
                _currentActive = _shopPanel;
                break;
            case ScreenName.Difficulty:
                DifficultyHandler.Instance.UpdateData();
                _currentActive = _difficultyPanel;
                break;
        }

        _currentActive.SetActive(true);
    }
}
