using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallItem : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Vector2 _prevPos;
    [SerializeField] private Vector2 _currentPos;

    [SerializeField] private float _velocityMove;
    private bool isMove = false;

    private void Start()
    {
        _sprite.sprite = Store._currentSpriteBall;
        _currentPos = transform.position;
    }

    public Vector2 GetPrevPos()
    {
        return _prevPos;
    }

    public void NextStep(Vector2 pos)
    {
        _prevPos = _currentPos;
        _currentPos = pos;

        isMove = true;
    }

    private void Update()
    {
        if (isMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, _currentPos, Time.deltaTime * _velocityMove);
        }
    }
}
