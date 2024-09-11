using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyView : MonoBehaviour
{
    [SerializeField] private Sprite[] _spritesBackgroundImage;

    [SerializeField] private Button _startBtn;
    [SerializeField] private Sprite _spriteStartBtnSource;
    [SerializeField] private Sprite _spriteStartBtnDeactive;
    [SerializeField] private Button _lockButton;
    [SerializeField] private Button _closeButton;

    [Header("View")]
    [SerializeField] private Image _imageDiff;
    [SerializeField] private Text _nameDiff;
    [SerializeField] private Text _bestResult;

    private List<DifficultyData> difficultyDatas;
    private int numberCurrentActive;
    private DifficultyHandler difficultyHandler;

    [Header("Move Buttons")]
    [SerializeField] private Button _backBtn;
    [SerializeField] private Button _forwardBtn;

    [Header("View Target To Open")]
    [SerializeField] private GameObject _panelTarget;
    [SerializeField] private Button _closeTargetButton;
    [SerializeField] private Text _nameOpenTargetDifText;
    [SerializeField] private Text _valueTargetToOpenText;
    [SerializeField] private Text _namePrevDiffText;


    public void InitView(List<DifficultyData> list, DifficultyHandler difficultyHandler)
    {
        this.difficultyHandler = difficultyHandler;
        difficultyDatas = list;

        numberCurrentActive = 0;

        ShowCurrentDifficulty();
    }

    private void Awake()
    {
        _startBtn.onClick.AddListener(() =>
        {
            difficultyHandler.SetTimeScaleToGame(numberCurrentActive);
        });

        _closeButton.onClick.AddListener(() =>
        {
            ScreensController.I.ShowPanel(ScreensController.ScreenName.Menu);
        });

        _lockButton.onClick.AddListener(() =>
        {
            _panelTarget.SetActive(true);
            _nameOpenTargetDifText.text = difficultyDatas[numberCurrentActive].nameDifficulty;
            _valueTargetToOpenText.text = difficultyDatas[numberCurrentActive].targetScoreToOpen.ToString();
            _namePrevDiffText.text = difficultyDatas[numberCurrentActive - 1].nameDifficulty;
        });

        _closeTargetButton.onClick.AddListener(() =>
        {
            _panelTarget.SetActive(false);
        });

        _backBtn.onClick.AddListener(() =>
        {
            if (numberCurrentActive > 0)
            {
                numberCurrentActive--;
                if (numberCurrentActive == 0)
                    _backBtn.gameObject.SetActive(false);

                if (numberCurrentActive == 1)
                    _forwardBtn.gameObject.SetActive(true);
                ShowCurrentDifficulty();
            }
        });

        _forwardBtn.onClick.AddListener(() =>
        {
            if (numberCurrentActive < 2)
            {
                numberCurrentActive++;

                if (numberCurrentActive == 2)
                    _forwardBtn.gameObject.SetActive(false);

                if (numberCurrentActive == 1)
                    _backBtn.gameObject.SetActive(true);
                ShowCurrentDifficulty();
            }
        });
    }

    private void ShowCurrentDifficulty()
    {
        if (difficultyDatas[numberCurrentActive].state == StateOpenDifficulty.Open)
        {
            _startBtn.enabled = true;
            _startBtn.GetComponent<Image>().sprite = _spriteStartBtnSource;
            _lockButton.gameObject.SetActive(false);

            _bestResult.transform.parent.gameObject.SetActive(true);
            _bestResult.text = difficultyDatas[numberCurrentActive].bestResult.ToString();
        }
        else
        {
            _startBtn.enabled = false;
            _startBtn.GetComponent<Image>().sprite = _spriteStartBtnDeactive;
            _lockButton.gameObject.SetActive(true);
            _bestResult.transform.parent.gameObject.SetActive(false);
        }

        _imageDiff.sprite = _spritesBackgroundImage[numberCurrentActive];
        _nameDiff.text = difficultyDatas[numberCurrentActive].nameDifficulty;
    }

    public void UpdateFields()
    {
        ShowCurrentDifficulty();
    }
}
