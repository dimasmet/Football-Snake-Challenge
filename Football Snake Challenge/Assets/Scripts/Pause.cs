using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    [SerializeField] private Button _continumBtn;
    [SerializeField] private Button _homeBtn;

    [SerializeField] private Button _activePauseBtn;

    private void Awake()
    {
        _activePauseBtn.onClick.AddListener(() =>
        {
            _panel.SetActive(true);
            Time.timeScale = 0;
        });

        _continumBtn.onClick.AddListener(() =>
        {
            _panel.SetActive(false);
            Time.timeScale = 0.5f;
        });

        _homeBtn.onClick.AddListener(() =>
        {
            Main.OnResetGame?.Invoke();
            _panel.SetActive(false);
            Time.timeScale = 0.5f;
            ScreensController.I.ShowPanel(ScreensController.ScreenName.Menu);
        });
    }
}
