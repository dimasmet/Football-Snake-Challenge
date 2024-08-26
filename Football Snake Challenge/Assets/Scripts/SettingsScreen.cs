using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScreen : MonoBehaviour
{
    [SerializeField] private Button _backBtn;

    [SerializeField] private Button _rateUs;
    [SerializeField] private Button _termsOfUse;
    [SerializeField] private Button _privacyBtn;

    private void Awake()
    {
        _backBtn.onClick.AddListener(() =>
        {
            ScreensController.I.ShowPanel(ScreensController.ScreenName.Menu);
        });
    }
}
