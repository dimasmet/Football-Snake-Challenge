using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeHandler : MonoBehaviour
{
    public static TimeHandler Instance;

    private float time;
    [SerializeField] private Text timerText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private IEnumerator WaitSecond()
    {
        while (true)
        {
            time += 1;
            UpdateTimeText();
            yield return new WaitForSeconds(1f * Time.timeScale);
        }
    }

    public void StartTime()
    {
        time = 0;
        UpdateTimeText();
        StartCoroutine(WaitSecond());
    }

    public float GetValueStopTime()
    {
        StopAllCoroutines();
        return time;
    }

    private void UpdateTimeText()
    {
        timerText.text = string.Format("{0:00}:{1:00}", Mathf.FloorToInt(time / 60), Mathf.FloorToInt(time % 60));
    }
}
