using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DifficultyWrapper
{
    public List<DifficultyData> difficultyDatas;
}

[System.Serializable]
public class DifficultyData
{
    public string nameDifficulty;
    public float timeSpeed;
    public int targetScoreToOpen;
    public int bestResult;
    public StateOpenDifficulty state;
}

public enum StateOpenDifficulty
{
    Open,
    Close
}

public class DifficultyHandler : MonoBehaviour
{
    public static DifficultyHandler Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public enum DifficultyType
    {
        EASY,
        AVERAGE,
        HARD
    }

    [SerializeField] private DifficultyWrapper difficultyWrapper;

    [SerializeField] private DifficultyView difficultyView;

    [SerializeField] private ShakeControl shakeControl;

    private int numberDifficulty;

    private void Start()
    {
        string jsonDif = PlayerPrefs.GetString("JsonDifficultySave");
        if (jsonDif != "")
            difficultyWrapper = JsonUtility.FromJson<DifficultyWrapper>(jsonDif);

        difficultyView.InitView(difficultyWrapper.difficultyDatas,this);
    }

    public void SetTimeScaleToGame(int numberDifficulty)
    {
        this.numberDifficulty = numberDifficulty;
        shakeControl.SetSpeedMoveSnake(difficultyWrapper.difficultyDatas[numberDifficulty].timeSpeed);
        ScreensController.I.ShowPanel(ScreensController.ScreenName.TypeGame);
    }

    public void CheckTargetToOpenNextDifficulty(int valueResult)
    {
        if (numberDifficulty < difficultyWrapper.difficultyDatas.Count - 1)
        {
            if (difficultyWrapper.difficultyDatas[numberDifficulty + 1].state != StateOpenDifficulty.Open)
            {
                if (valueResult >= difficultyWrapper.difficultyDatas[numberDifficulty + 1].targetScoreToOpen)
                {
                    difficultyWrapper.difficultyDatas[numberDifficulty + 1].state = StateOpenDifficulty.Open;

                    Save();
                }
            }
        }

        if (valueResult > difficultyWrapper.difficultyDatas[numberDifficulty].bestResult)
        {
            difficultyWrapper.difficultyDatas[numberDifficulty].bestResult = valueResult;
            Save();
        }
    }

    private void Save()
    {
        string jsonDifficulty = JsonUtility.ToJson(difficultyWrapper);
        PlayerPrefs.SetString("JsonDifficultySave", jsonDifficulty);
    }

    public void UpdateData()
    {
        difficultyView.UpdateFields();
    }
}
