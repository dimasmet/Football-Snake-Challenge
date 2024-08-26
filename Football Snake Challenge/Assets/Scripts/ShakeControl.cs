using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShakeControl : MonoBehaviour
{
    public enum MoveDirection
    {
        Down,
        Up,
        Left,
        Right
    }

    [SerializeField] private List<BallItem> ballItems;
    [SerializeField] private BallItem _prefabBall;
    [SerializeField] private GameObject _startShakePrefab;

    private GameObject _currentShake;

    public bool isTakeBonus = false;

    private Vector2 _currentDirection;

    public float _timeStep = 0.1f;
    public float _deltaStep = 0.5f;

    [SerializeField] private Button _leftBtn;
    [SerializeField] private Button _rightBtn;
    [SerializeField] private Button _upBtn;
    [SerializeField] private Button _downBtn;

    private void Awake()
    {
        _leftBtn.onClick.AddListener(() =>
        {
            if (_currentDirection != Vector2.right)
                _currentDirection = Vector2.left;
        });
        _rightBtn.onClick.AddListener(() =>
        {
            if (_currentDirection != Vector2.left)
                _currentDirection = Vector2.right;
        });
        _upBtn.onClick.AddListener(() =>
        {
            if (_currentDirection != Vector2.down)
                _currentDirection = Vector2.up;
        });
        _downBtn.onClick.AddListener(() =>
        {
            if (_currentDirection != Vector2.up)
                _currentDirection = Vector2.down;
        });
    }

    private void Start()
    {
        Main.OnTakeBonus += TakeBonus;
        Main.OnStartGame += StartGame;
        Main.OnGameOver += StopMove;
    }

    private void StartGame()
    {
        if (_currentShake != null)
        {
            for (int i = 0; i < ballItems.Count; i++)
            {
                Destroy(ballItems[i].gameObject);
            }
            Destroy(_currentShake.gameObject);
        }

        _currentShake = Instantiate(_startShakePrefab, Vector3.zero, Quaternion.identity);

        ballItems = new List<BallItem>();
        ballItems.Add(_currentShake.transform.GetChild(0).GetComponent<BallItem>());
        ballItems.Add(_currentShake.transform.GetChild(1).GetComponent<BallItem>());
        ballItems.Add(_currentShake.transform.GetChild(2).GetComponent<BallItem>());

        _currentDirection = Vector2.down;
        StartCoroutine(WaitToNextStepShake());

        Time.timeScale = 0.5f;
    }

    private void OnDestroy()
    {
        Main.OnTakeBonus -= TakeBonus;
        Main.OnStartGame -= StartGame;
        Main.OnGameOver -= StopMove;
    }

    private void StopMove()
    {
        StopAllCoroutines();
    }

    private void TakeBonus()
    {
        isTakeBonus = true;
    }

    public void MoveShake(Vector2 direction)
    {
        _currentDirection = direction;
    }

    private IEnumerator WaitToNextStepShake()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeStep);
            ballItems[0].NextStep((new Vector2(ballItems[0].transform.position.x, ballItems[0].transform.position.y) + (_currentDirection * _deltaStep)));

            for (int i = 1; i < ballItems.Count; i++)
            {
                ballItems[i].NextStep(ballItems[i - 1].GetPrevPos());
            }

            if (isTakeBonus)
            {
                isTakeBonus = false;
                CreateNewBall();
            }
        }
    }

    private void CreateNewBall()
    {
        BallItem ballnew = Instantiate(_prefabBall, ballItems[ballItems.Count - 1].GetPrevPos(), Quaternion.identity);
        ballItems.Add(ballnew);
    }
}
