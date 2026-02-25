using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Requrired Components"), Space(10)]
    [SerializeField] GameObject          _head;
    [SerializeField] CharacterController _characterController;
    [SerializeField] InputHandler        _inputHandler;
    [SerializeField] GameState           _gameState;
    [SerializeField] bool _disableInputOnTimeEnd = false;

    [Header("Modules"), Space(10)]
    [SerializeField] BasketballFactory _basketballFactory;
    [SerializeField] BallisticLauncher _ballisticLauncher;
    [SerializeField] BasketballTracker _ballTracker;
    [SerializeField] PlayerArea        _playerArea;

    [Header("Movement Parameters"), Space(10)]
    [SerializeField] float _movementSpeed = 5f;
    [SerializeField] float _rotationSpeed = .1f;
    [SerializeField] float _jumpForce     = 5f;
    [SerializeField] float _gravity       = 10f;

    [Header("Throw Parameters"), Space(10)]
    [SerializeField] float _throwFactor   = 20f;

    float      _headAngle  = 0f;
    float      _velocityY  = -1f;
    Basketball _thrownBall = null;

    // movement
    public void Move(Vector2 movementVector)
    {
        // convert XY input into XZ movement
        Vector3 movementDelta = transform.forward * movementVector.y + transform.right * movementVector.x;
        movementDelta *= _movementSpeed;

        if (_characterController.isGrounded && _velocityY < 0)
            _velocityY = -2f;
        _velocityY -= _gravity * Time.deltaTime;

        movementDelta.y = _velocityY;
        _characterController.Move(movementDelta * Time.deltaTime);

    }
    public void Look(Vector2 lookVector)
    {
        // X rotates body Y
        transform.localRotation *= Quaternion.Euler(Vector3.up * _rotationSpeed * lookVector.x);

        //Y rotates head X
        _headAngle = Mathf.Clamp(_headAngle + _rotationSpeed * lookVector.y, -90, 90);
        _head.transform.localRotation = Quaternion.Euler(Vector3.left * _headAngle);
    }
    public void Jump()
    {
        if (_characterController.isGrounded) 
        {
            _velocityY = _jumpForce;
        }
    }
    public void Teleport(Vector3 position, Vector3 look_at)
    {
        float height_offset = _characterController.height / 2;

        _characterController.enabled = false;
        transform.position = position + transform.up * height_offset;
        _characterController.enabled = true;

        Vector3 target_direction = (look_at - transform.position).normalized;
        if (target_direction != Vector3.zero)
        {
            Quaternion target_rotation = Quaternion.LookRotation(new Vector3(target_direction.x, 0, target_direction.z));
            transform.rotation = target_rotation;
            if (_head != null) _head.transform.LookAt(look_at);
        }
    }
    private void DisableControls()
    {
        if (!_disableInputOnTimeEnd) return;
        _inputHandler.SetMouseVisibility(true);
        _inputHandler.enabled = false;
    }

    // actions
    public void ThrowBallAuto()
    {
        if (_basketballFactory == null) return;

        Basketball ball = _basketballFactory.CreateBasketball(_head.transform.position, _head.transform.rotation);
        ball.OnBasket += OnThrownBallScored;
        
        if (_ballisticLauncher == null) return;

        _ballisticLauncher.LaunchGameObject(
            ball.gameObject, 
            BallisticLauncher.LaunchMode.Direct, 
            ball.BallDiameter/2f
            );

    }




    // throw logic
    private void OnThrownBallScored(Basketball basketball)
    {
        ScoreController.Instance.BasketballScore(_gameState, basketball.BallPoints);
        basketball.OnBasket -= OnThrownBallScored;
        basketball.OnMiss -= OnThrownBallMiss;

        if (_playerArea != null) Teleport(_playerArea.GetPoint(), _playerArea.FocalPoint.position);
    }
    private void OnThrownBallMiss(Basketball basketball)
    {
        ScoreController.Instance.BasketballMiss(_gameState);
        basketball.OnMiss -= OnThrownBallMiss;
        basketball.OnBasket -= OnThrownBallScored;
    }
    private void OnThrownBallDespawn(Basketball basketball)
    {
        basketball.OnDespawn -= OnThrownBallDespawn;


        _inputHandler.Movement = false;
        _inputHandler.Actions = false;
        _inputHandler.Utility = true;
        _inputHandler.Swipes = true;
        _thrownBall = null;
    }
    public void SwipeThrowBall(Vector2 direction, float throw_power)
    {
        if (_basketballFactory == null) return;

        if (_thrownBall) return;

        _inputHandler.Movement = false;
        _inputHandler.Actions = false;
        _inputHandler.Utility = false;
        _inputHandler.Swipes = false;


        _thrownBall = _basketballFactory.CreateBasketball(_head.transform.position, _head.transform.rotation);
        if(_gameState.FireballStatus) _thrownBall.ActivateFireTrail();

        _ballTracker?.TrackBasketball(_thrownBall);

        _thrownBall.OnBasket += OnThrownBallScored;
        _thrownBall.OnMiss += OnThrownBallMiss;
        _thrownBall.OnDespawn += OnThrownBallDespawn;

        if(_ballisticLauncher == null) return;

        Vector2 LEFT_THROW = Vector2.up + Vector2.left;
        Vector2 RIGHT_THROW = Vector2.up + Vector2.right;
        LEFT_THROW.Normalize();
        RIGHT_THROW.Normalize();
        bool backboard_throw = false;


        if (Vector2.Dot(direction, LEFT_THROW) > 0.9f)
        {
            backboard_throw = true;
        }
        else if (Vector2.Dot(direction, RIGHT_THROW) > 0.9f)
        {
            backboard_throw = true;
        }

        if (backboard_throw)
        {
            _ballisticLauncher.LaunchGameObject(
                _thrownBall.gameObject,
                BallisticLauncher.LaunchMode.Reflect,
                _thrownBall.BallDiameter / 2f,
                throw_power * _throwFactor
                );
        }
        else
        {
            _ballisticLauncher.LaunchGameObject(
                _thrownBall.gameObject,
                BallisticLauncher.LaunchMode.Direct,
                _thrownBall.BallDiameter / 2f,
                throw_power * _throwFactor
                );
        }
    }

    private void OnEnable()
    {
        _inputHandler.OnJump                 += Jump;
        _inputHandler.OnLook                 += Look;
        _inputHandler.OnMove                 += Move;
        _inputHandler.OnThrow                += ThrowBallAuto;
        _inputHandler.OnSwipeThrowSuccessful += SwipeThrowBall;

        _gameState.OnTimeEnd += DisableControls;

    }
    private void OnDisable()
    {
        _inputHandler.OnJump                 -= Jump;
        _inputHandler.OnLook                 -= Look;
        _inputHandler.OnMove                 -= Move;
        _inputHandler.OnThrow                -= ThrowBallAuto;
        _inputHandler.OnSwipeThrowSuccessful -= SwipeThrowBall;

        _gameState.OnTimeEnd -= DisableControls;

    }

    private void Start()
    {
        if (_playerArea != null) Teleport(_playerArea.GetPoint(), _playerArea.FocalPoint.position);
        
        _gameState.StartTimer();
    }

   
}
