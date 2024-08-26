using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordBlockUI : MonoBehaviour
{
    [SerializeField] private Text _numberText;
    [SerializeField] private Text _timeText;
    [SerializeField] private Text _scoreText;

    public void Init(int num, float time, int score)
    {
        _numberText.text = num + ".";
        _timeText.text = string.Format("{0:00}:{1:00}", Mathf.FloorToInt(time / 60), Mathf.FloorToInt(time % 60));
        _scoreText.text = score.ToString();
    }
}
