using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : MonoBehaviour
{
    [SerializeField] private Button _startBtn;
    [SerializeField] private Button _settingsBtn;
    [SerializeField] private Button _recordsBtn;

    private void Awake()
    {
        _startBtn.onClick.AddListener(() =>
        {
            ScreensController.I.ShowPanel(ScreensController.ScreenName.Difficulty);
        });

        _settingsBtn.onClick.AddListener(() =>
        {
            ScreensController.I.ShowPanel(ScreensController.ScreenName.Settings);
        });

        _recordsBtn.onClick.AddListener(() =>
        {
            ScreensController.I.ShowPanel(ScreensController.ScreenName.Records);
        });
    }
}
