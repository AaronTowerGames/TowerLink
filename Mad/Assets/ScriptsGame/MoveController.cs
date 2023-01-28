using Spine.Unity;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    private const string walk = "walk";

    [SerializeField]
    private SkeletonRenderer _skeletonRenderer;

    [SerializeField]
    private SkeletonAnimation _skeletonAnimation;

    [SerializeField]
    private List<Vector3> _positions;

    [SerializeField]
    private int _nowPosition = 2;

    [SerializeField]
    private float _moveSpeed = 2f, changeFrame;

    private Transform _transform;

    private Vector3 _position;

    private bool _isMoving = false, _isMoveRight = false;

    [SerializeField]
    private int frame = 0, frames = 60;


    private void Start()
    {
        _transform = transform;
    }

    private void OnEnable()
    {
        EventBus.MoveLeftButtonClicked.Subscribe(MoveLeft);
        EventBus.MoveRightButtonClicked.Subscribe(MoveRight);
    }

    private void OnDisable()
    {
        EventBus.MoveLeftButtonClicked.Unsubscribe(MoveLeft);
        EventBus.MoveRightButtonClicked.Unsubscribe(MoveRight);
    }

    private void MoveRight()
    {
        if (_nowPosition < 4)
        {
            _position = _transform.position;

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

            //_transform.position = ;
            _skeletonAnimation.AnimationName = "sit_go_right";
            
            //_skeletonAnimation.Skeleton.ScaleX = 1;

            _isMoving = true;
        }
    }

    private void MoveLeft()
    {
        if (_nowPosition > 0)
        {
            _position = _transform.position;

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

            //_transform.position = _positions[_nowPosition];
            _skeletonAnimation.AnimationName = "sit_go_left";
            //_skeletonAnimation.Skeleton.ScaleX = -1;

            _isMoving = true;
        }
    }

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            frame++;
            changeFrame = frame / (float)frames;

            _transform.position = Vector3.Lerp(_position, _positions[_nowPosition], changeFrame);
            
            if (frame >= frames)
            {
                _isMoving = false;
                frame = 0;
                _skeletonAnimation.AnimationName = "sit_idle";
            }
        }
    }

    private void Shoot()
    {
        Debug.Log("FIRE");
    }

    private void Idle()
    {
        _skeletonAnimation.AnimationName = "idle";
    }

    private void Move(Vector2 direction)
    {
        float scaleMoveSpeed = _moveSpeed * Time.deltaTime;

        Vector3 moveDirection = new Vector3(direction.x, direction.y, 0);
        if (direction.x < 0)
        {
            _skeletonAnimation.Skeleton.ScaleX = -1;
        }
        else
        {
            _skeletonAnimation.Skeleton.ScaleX = 1;
        }
        if (direction.y > 0)
        {
            _skeletonAnimation.AnimationName = "jump";
        }
        if (direction.y < 0)
        {
            _skeletonAnimation.AnimationName = "crouch";
        }
        transform.position += moveDirection * scaleMoveSpeed;

        if (direction.x != 0)
        {
            _skeletonAnimation.AnimationName = "walk";
        }
        
    }
}
