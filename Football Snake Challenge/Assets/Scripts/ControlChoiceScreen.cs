using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlChoiceScreen : MonoBehaviour
{
    [SerializeField] private Button _classicBtn;
    [SerializeField] private Button _freeMovementBtn;

    [SerializeField] private Button _backBtn;

    [SerializeField] private GameObject _rules;
    [SerializeField] private GameObject _rulesClassicControl;
    [SerializeField] private GameObject _rulesFreeMoveControl;

    [SerializeField] private Button _startBtn;

    private void Awake()
    {
        _classicBtn.onClick.AddListener(() =>
        {
            Main.Instance.ChoiceTypeControl(Main.TypeGame.Liner);

            RulesControl(Main.TypeGame.Liner);
        });

        _freeMovementBtn.onClick.AddListener(() =>
        {
            Main.Instance.ChoiceTypeControl(Main.TypeGame.FreeMove);

            RulesControl(Main.TypeGame.FreeMove);         
        });

        _backBtn.onClick.AddListener(() =>
        {
            ScreensController.I.ShowPanel(ScreensController.ScreenName.Menu);
        });

        _startBtn.onClick.AddListener(() => {
            _rules.SetActive(false);
            ScreensController.I.ShowPanel(ScreensController.ScreenName.Game);
            Main.OnStartGame?.Invoke();
        });
    }

    private void RulesControl(Main.TypeGame type)
    {
        switch(type)
        {
            case Main.TypeGame.Liner:
                _rulesClassicControl.SetActive(true);
                _rulesFreeMoveControl.SetActive(false);
                break;
            case Main.TypeGame.FreeMove:
                _rulesClassicControl.SetActive(false);
                _rulesFreeMoveControl.SetActive(true);
                break;
        }

        _rules.SetActive(true);
    }
}
