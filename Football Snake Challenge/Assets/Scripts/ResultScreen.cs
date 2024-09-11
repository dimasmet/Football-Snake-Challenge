using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour
{
    [SerializeField] private GameObject _resultView;

    [SerializeField] private GameObject _backgroundRecord;
    [SerializeField] private Text _resultValueText;
    [SerializeField] private Text _timeValueText;

    public enum TypeResult
    {
        None,
        RecordNew
    }

    [SerializeField] private Button _restartBtn;
    [SerializeField] private Button _homeBtn;

    private void Awake()
    {
        _restartBtn.onClick.AddListener(() =>
        {
            Main.OnStartGame?.Invoke();
            _resultView.SetActive(false);
        });

        _homeBtn.onClick.AddListener(() =>
        {
            ScreensController.I.ShowPanel(ScreensController.ScreenName.Menu);
            _resultView.SetActive(false);
        });
    }

    public void ShowResult(TypeResult typeResult, int scoreResult, float time)
    {
        _resultView.SetActive(true);
        _resultValueText.text = scoreResult.ToString();
        switch (typeResult)
        {
            case TypeResult.None:
                _backgroundRecord.SetActive(false);
                break;
            case TypeResult.RecordNew:
                _backgroundRecord.SetActive(true);
                break;
        }

        DifficultyHandler.Instance.CheckTargetToOpenNextDifficulty(scoreResult);

        SoundTrackHandler.sound.PlaySoundGameOver();

        _timeValueText.text = string.Format("{0:00}:{1:00}", Mathf.FloorToInt(time / 60), Mathf.FloorToInt(time % 60));
    }
}
