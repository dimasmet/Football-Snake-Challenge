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

    [SerializeField] private Button _rulesClasicBtn;
    [SerializeField] private Button _rulesFreeMoveBtn;
    [SerializeField] private Button _closeRulesBtn;

    private void Awake()
    {
        _rulesClasicBtn.onClick.AddListener(() =>
        {
            RulesControl(Main.TypeGame.Liner);
        });

        _rulesFreeMoveBtn.onClick.AddListener(() =>
        {
            RulesControl(Main.TypeGame.FreeMove);
        });

        _classicBtn.onClick.AddListener(() =>
        {
            Main.Instance.ChoiceTypeControl(Main.TypeGame.Liner);
            ScreensController.I.ShowPanel(ScreensController.ScreenName.Game);
            Main.OnStartGame?.Invoke();
        });

        _freeMovementBtn.onClick.AddListener(() =>
        {
            Main.Instance.ChoiceTypeControl(Main.TypeGame.FreeMove);
            ScreensController.I.ShowPanel(ScreensController.ScreenName.Game);
            Main.OnStartGame?.Invoke();        
        });

        _backBtn.onClick.AddListener(() =>
        {
            ScreensController.I.ShowPanel(ScreensController.ScreenName.Menu);
        });

        _closeRulesBtn.onClick.AddListener(() => {
            _rules.SetActive(false);
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
