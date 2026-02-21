using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    GameObject _head;
    [SerializeField]
    CharacterController _characterController;
    [SerializeField]
    InputHandler _inputHandler;


    //TESTING/DEBUG
    [SerializeField]
    BasketballFactory _basketballFactory;
    [SerializeField]
    BallisticLauncher _ballisticLauncher;



    [SerializeField]
    float _movementSpeed = 5f;
    [SerializeField]
    float _rotationSpeed = .1f;
    [SerializeField]
    float _jumpForce     = 5f;
    [SerializeField]
    float _gravity = 10f;


    float _headAngle = 0f;
    float _velocityY = -1f;


    


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
    public void ThrowBall(bool auto = true)
    {
        GameObject ball = _basketballFactory.CreateBasketball(_head.transform.position, _head.transform.rotation);
        if (auto) _ballisticLauncher.LaunchGameObject(ball, BallisticLauncher.LaunchMode.Direct, ball.GetComponent<Basketball>().BallDiameter/2f);
    }

    void Start()
    {
  
    }

    private void OnEnable()
    {
        _inputHandler.OnJump += Jump;
        _inputHandler.OnLook += Look;
        _inputHandler.OnMove += Move;
        _inputHandler.OnThrowBall += ThrowBall;
    }
    private void OnDisable()
    {
        _inputHandler.OnJump -= Jump;
        _inputHandler.OnLook -= Look;
        _inputHandler.OnMove -= Move;
        _inputHandler.OnThrowBall -= ThrowBall;
    }

    void Update()
    {
 
    }
}
