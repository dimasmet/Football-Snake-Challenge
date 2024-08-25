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

    [SerializeField] private Button _createBallsBtn;

    private bool isTest = false;

    private bool isStepActive = true;

    private void Awake()
    {
        _createBallsBtn.onClick.AddListener(() =>
        {
            isTest = !isTest;
        });
    }

    public void MoveShake(Vector2 direction)
    {
        if (isStepActive)
        {
            isStepActive = false;
            StartCoroutine(WaitToNextStepShake());

            ballItems[0].NextStep((new Vector2(ballItems[0].transform.position.x, ballItems[0].transform.position.y) + (direction * 0.5f)));
            /*switch (direction)
            {
                case MoveDirection.Down:
                    ballItems[0].NextStep(new Vector2(ballItems[0].transform.position.x, ballItems[0].transform.position.y) + (direction * 0.5f));
                    break;
                case MoveDirection.Up:
                    ballItems[0].NextStep(new Vector2(ballItems[0].transform.position.x, ballItems[0].transform.position.y + 0.5f));
                    break;
                case MoveDirection.Left:
                    ballItems[0].NextStep(new Vector2(ballItems[0].transform.position.x - 0.5f, ballItems[0].transform.position.y));
                    break;
                case MoveDirection.Right:
                    ballItems[0].NextStep(new Vector2(ballItems[0].transform.position.x + 0.5f, ballItems[0].transform.position.y));
                    break;
            }*/

            for (int i = 1; i < ballItems.Count; i++)
            {
                ballItems[i].NextStep(ballItems[i - 1].GetPrevPos());
            }

            if (isTest)
                CreateNewBall();
        }
    }

    private IEnumerator WaitToNextStepShake()
    {
        yield return new WaitForSeconds(0.1f);
        isStepActive = true;
    }

    private void CreateNewBall()
    {
        BallItem ballnew = Instantiate(_prefabBall, ballItems[ballItems.Count - 1].GetPrevPos(), Quaternion.identity);
        ballItems.Add(ballnew);
    }
}
