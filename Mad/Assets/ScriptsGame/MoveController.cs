using Spine.Unity;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    private const string walk = "walk";

    [SerializeField]
    private SkeletonAnimation _skeletonAnimation;

    [SerializeField]
    private List<Vector3> _positions;

    [SerializeField]
    private int _nowPosition = 2, _minPos, _maxPos;

    [SerializeField]
    private float _moveSpeed = 0.001f, changeFrame;
    private Transform _transform;

    private Vector3 _position;

    private bool _isMoving = false, _isMoveRight = false, _isStay = false;

    [SerializeField]
    private int frame = 0, frames = 1000;


    private void Start()
    {
        _transform = transform;
    }

    private void OnEnable()
    {
        EventBus.CalcHeroPositions.Subscribe(CalcHeroPositions);
        EventBus.MoveLeftButtonClicked.Subscribe(MoveLeft);
        EventBus.MoveRightButtonClicked.Subscribe(MoveRight);
        EventBus.HeroUP.Subscribe(HeroUP);
        EventBus.HeroDOWN.Subscribe(HeroDOWN);
    }

    private void OnDisable()
    {
        EventBus.CalcHeroPositions.Unsubscribe(CalcHeroPositions);
        EventBus.MoveLeftButtonClicked.Unsubscribe(MoveLeft);
        EventBus.MoveRightButtonClicked.Unsubscribe(MoveRight);
        EventBus.HeroUP.Unsubscribe(HeroUP);
        EventBus.HeroDOWN.Unsubscribe(HeroDOWN);
    }

    private void HeroDOWN()
    {
        if (_isStay)
        {
            _skeletonAnimation.AnimationName = "sit_idle";
            _isStay = false;
        }
    }

    private void HeroUP()
    {
        if (!_isStay)
        {
            _skeletonAnimation.AnimationName = "stand_idle";
            _isStay = true;
        }
    }

    private void CalcHeroPositions(int countPoss)
    {
        int startX = (int)(countPoss / 2f - 0.5f);
        _minPos = 0;
        _maxPos = countPoss - 1;
        _nowPosition = startX;
        for (int i = -startX; i <= startX; i++)
        {
            var x = i * DataSettings.CELL_X_DISTANCE;
            _positions.Add(new Vector3(x, 0, 0));
        }
    }

    private void MoveRight()
    {
        if (_nowPosition < _maxPos)
        {
            _position = _transform.localPosition;

            if (frame != 0)
            {
                if (!_isMoveRight)
                {
                    frame = frames - frame;
                    _position = _positions[_nowPosition];
                }
            }

            _nowPosition += 1;

            _isMoveRight = true;
            if (_isStay)
            {
                _skeletonAnimation.AnimationName = "stand_go_right";
            }
            else
            {
                _skeletonAnimation.AnimationName = "sit_go_right";
            }
            

            _isMoving = true;
        }
    }

    private void MoveLeft()
    {
        if (_nowPosition > _minPos)
        {
            _position = _transform.localPosition;

            if (frame != 0)
            {
                if (_isMoveRight)
                {
                    frame = frames - frame;
                    _position = _positions[_nowPosition];
                }
            }

            _isMoveRight = false;

            _nowPosition -= 1;

            if (_isStay)
            {
                _skeletonAnimation.AnimationName = "stand_go_left";
            }
            else
            {
                _skeletonAnimation.AnimationName = "sit_go_left";
            }
            

            _isMoving = true;
        }
    }

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            frame++;
            changeFrame = frame * DinamicTest.Instance.GetHeroMoveSpeed() / (float)(frames * _moveSpeed);

            _transform.localPosition = Vector3.Lerp(_position, _positions[_nowPosition], changeFrame);
            
            if (frame * DinamicTest.Instance.GetHeroMoveSpeed() >= frames * _moveSpeed)
            {
                _isMoving = false;
                frame = 0;
                _skeletonAnimation.AnimationName = "sit_idle";
            }
        }
    }
}
