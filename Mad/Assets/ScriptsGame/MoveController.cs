using Spine.Unity;
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
        EventBus.CalcHeroPositions.Subscribe(CalcHeroPositions);
        EventBus.MoveLeftButtonClicked.Subscribe(MoveLeft);
        EventBus.MoveRightButtonClicked.Subscribe(MoveRight);
    }

    private void OnDisable()
    {
        EventBus.CalcHeroPositions.Unsubscribe(CalcHeroPositions);
        EventBus.MoveLeftButtonClicked.Unsubscribe(MoveLeft);
        EventBus.MoveRightButtonClicked.Unsubscribe(MoveRight);
    }

    private void CalcHeroPositions(int countPoss)
    {
        int startX = (int)(countPoss / 2f - 0.5f);
        _minPos = 0;
        _maxPos = countPoss;
        _nowPosition = startX;
        for (int i = -startX; i <= startX; i++)
        {
            var x = i * DataSettings.CELL_X_DISTANCE;
            _positions.Add(new Vector3(x, DataSettings.START_Y_DISTANCE, 0));
        }
    }

    private void MoveRight()
    {
        if (_nowPosition < _maxPos)
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

            _skeletonAnimation.AnimationName = "sit_go_right";

            _isMoving = true;
        }
    }

    private void MoveLeft()
    {
        if (_nowPosition > _minPos)
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

            _skeletonAnimation.AnimationName = "sit_go_left";

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
}
