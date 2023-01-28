using UnityEngine;

public class MoveCrosshair : MonoBehaviour
{
    [SerializeField]
    private RectTransform _parentTransform;
    
    private RectTransform _transform;

    [SerializeField]
    private Joystick _joystick;

    private Vector3 _startPosition, _endPosition;

    [SerializeField]
    private float _changeFrame;

    [SerializeField]
    private int _frame = 0, _frames = 60;

    [SerializeField]
    private float _parentMaxX, _parentMaxY;

    private bool _isJoystickMoving = false;
    private bool _isMoving = false;

    [SerializeField]
    private float _speed = 30f;

    private void Start()
    {
        _transform = GetComponent<RectTransform>();
        _parentMaxX = _parentTransform.sizeDelta.x / 2f;
        _parentMaxY = _parentTransform.sizeDelta.y / 2f;
    }

    private void OnEnable()
    {
        EventBus.JoystickMoveActivated.Subscribe(JoystickActivated);
        EventBus.JoystickMoveDeactivated.Subscribe(JoystickDeactivated);
    }

    private void OnDisable()
    {
        EventBus.JoystickMoveActivated.Subscribe(JoystickActivated);
        EventBus.JoystickMoveDeactivated.Subscribe(JoystickDeactivated);
    }

    private void JoystickDeactivated()
    {
        _isJoystickMoving = false;
    }

    private void JoystickActivated()
    {
        _isJoystickMoving = true;
    }

    public void MoveToPoint(Vector2 vector)
    {
        _startPosition = _transform.position;

        float y = vector.y;

        if ( vector.y < -_parentMaxY + _parentMaxY * 0.6f)
        {
            y = -_parentMaxY + _parentMaxY * 0.6f;
        }

        _endPosition = new Vector3(vector.x, y, _startPosition.z);

        _frame = 0;
        _isJoystickMoving = false;
        _isMoving = true;
    }

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            _frame++;
            _changeFrame = _frame / (float)_frames;

            _transform.position = Vector3.Lerp(_startPosition, _endPosition, _changeFrame);

            if (_frame >= _frames)
            {
                _isMoving = false;
            }
        }

        if (_isJoystickMoving)
        {
            float x = _transform.localPosition.x + _joystick.Horizontal * _speed;
            float y = _transform.localPosition.y + _joystick.Vertical * _speed;

            if (x > _parentMaxX || x < -_parentMaxX)
            {
                x = _transform.localPosition.x;
            }

            if (y > _parentMaxY || y < -_parentMaxY + _parentMaxY * 0.6f)
            {
                y = _transform.localPosition.y;
            }

            _transform.localPosition = new Vector3(x, y, _transform.localPosition.z);

            EventBus.AutoFireOn.Invoke();
        }
        else
        {
            EventBus.AutoFireOff.Invoke();
        }
    }
}
