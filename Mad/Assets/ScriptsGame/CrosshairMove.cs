using UnityEngine;

public class CrosshairMove : MonoBehaviour
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
    private int _frame = 0, _frames = 1000;

    [SerializeField]
    private float _parentMaxX, _parentMaxY;

    private bool _isJoystickMoving = false;
    private bool _isMoving = false;

    [SerializeField]
    private float _speed = 0.001f;

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

        EventBus.AutoFireOff.Invoke();
    }

    private void JoystickActivated()
    {
        _isJoystickMoving = true;
        _isMoving = false;

        EventBus.AutoFireOn.Invoke();
    }

    public void MoveToPoint(Vector2 vector)
    {
        _startPosition = _transform.position;

        float y = vector.y;

        if ( vector.y < -_parentMaxY + _parentMaxY * 0.2f)
        {
            y = -_parentMaxY + _parentMaxY * 0.2f;
        }

        _endPosition = new Vector3(vector.x , y, _startPosition.z);

        _frame = 0;
        _isJoystickMoving = false;
        _isMoving = true;
    }

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            _frame++;
            _changeFrame = _frame * DinamicTest.Instance.GetCrosshairSpeed() / (float)(_frames * _speed) * Screen.width / (Vector3.Distance(_startPosition, _endPosition) + Screen.width / 4);

            _transform.position = Vector3.Lerp(_startPosition, _endPosition, _changeFrame);
            Debug.Log($"{Vector3.Distance(_startPosition, _endPosition)} {Screen.width / (Vector3.Distance(_startPosition, _endPosition) + Screen.width / 4)} {_changeFrame}");
            if (_frame * DinamicTest.Instance.GetCrosshairSpeed() * Screen.width >= _frames * _speed * (Vector3.Distance(_startPosition, _endPosition) + Screen.width / 4))
            {
                _isMoving = false;
            }
        }

        if (_isJoystickMoving)
        {
            float x = 0f;
            if (Mathf.Abs(_joystick.Horizontal) > 0.2)
            {
                x = _transform.localPosition.x + _joystick.Horizontal * _speed * DinamicTest.Instance.GetCrosshairSpeed() / _frames;
            }
            else
            {
                x = _transform.localPosition.x;
            }

            float y = 0f;
            if (Mathf.Abs(_joystick.Vertical) > 0.2)
            {
                y = _transform.localPosition.y + _joystick.Vertical * _speed * DinamicTest.Instance.GetCrosshairSpeed() / _frames;
            }
            else
            {
                y = _transform.localPosition.y;
            }
            

            if (x > _parentMaxX || x < -_parentMaxX)
            {
                x = _transform.localPosition.x;
            }

            if (y > _parentMaxY || y < -_parentMaxY + _parentMaxY * 0.6f)
            {
                y = _transform.localPosition.y;
            }

            _transform.localPosition = new Vector3(x, y, _transform.localPosition.z);
        }
    }
}
