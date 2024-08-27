using System.Collections;
using System.Collections.Generic;
using UnityEngine.iOS;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScreen : MonoBehaviour
{
    [SerializeField] private Button _backBtn;

    [SerializeField] private Button _rateUs;
    [SerializeField] private Button _termsOfUse;
    [SerializeField] private Button _privacyBtn;
    [SerializeField] private Button _closePanelTextBtn;

    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject _privacy;
    [SerializeField] private GameObject _termsOfUsePanel;

    private void Awake()
    {
        Application.targetFrameRate = 90;

        _rateUs.onClick.AddListener(() =>
        {
            Device.RequestStoreReview();
        });

        _backBtn.onClick.AddListener(() =>
        {
            ScreensController.I.ShowPanel(ScreensController.ScreenName.Menu);
        });

        _termsOfUse.onClick.AddListener(() =>
        {
            _panel.SetActive(true);

            _privacy.SetActive(false);
            _termsOfUsePanel.SetActive(true);
        });

        _privacyBtn.onClick.AddListener(() =>
        {
            _panel.SetActive(true);

            _privacy.SetActive(true);
            _termsOfUsePanel.SetActive(false);
        });

        _closePanelTextBtn.onClick.AddListener(() =>
        {
            _panel.SetActive(false);
        });
    }
}
