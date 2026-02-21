using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private IM_InputMapping _playerControls;
    private Camera _mainCamera;


    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndTouch;

    public delegate void Jump();
    public event Jump OnJump;
    public delegate void ThrowBall(bool auto = true);
    public event ThrowBall OnThrowBall;

    public delegate void Move(Vector2 movement);
    public event Move OnMove;
    public delegate void Look(Vector2 look);
    public event Look OnLook;



    //InputAction _moveAction;
    //InputAction _jumpAction;
    //InputAction _lookAction;
    //
    //InputAction _togglemouseAction;
    //
    //InputAction _throwballAction;
    //
    //bool _isSwiping = false;
    //InputAction _swipeAction;
    //
    //Vector2 DBG_SWIPE = Vector2.zero;

    private void Awake()
    {
        _playerControls = new IM_InputMapping();
        _mainCamera = Camera.main;
    }
    private void OnEnable()
    {
        _playerControls.Enable();
    }
    private void OnDisable()
    {
        _playerControls.Disable();
    }

    void Start()
    {
       
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;

        _playerControls.PlayerActions.PrimaryContact.started += OnPrimaryContactStart;
        _playerControls.PlayerActions.PrimaryContact.canceled += OnPrimaryContactEnd;
        _playerControls.PlayerActions.Jump.started += OnJumpPerformed;
        _playerControls.PlayerActions.ThrowBall.started += OnThrowBallPerformed;
        _playerControls.PlayerActions.ToggleMouse.started += OnMouseToggle;

    }

    void Update()
    {
        //Vector2 input_movementVector = _moveAction.ReadValue<Vector2>();
        //_playerController.Move(input_movementVector);
        //
        //Vector2 input_lookVector = _lookAction.ReadValue<Vector2>();
        //_playerController.Look(input_lookVector);

        OnLook?.Invoke(_playerControls.PlayerActions.Look.ReadValue<Vector2>());
        OnMove?.Invoke(_playerControls.PlayerActions.Move.ReadValue<Vector2>());  
    }


    private void OnPrimaryContactStart(InputAction.CallbackContext context)
    {
        OnStartTouch?.Invoke(
            ScreenToWorld(_mainCamera, _playerControls.PlayerActions.PrimaryPosition.ReadValue<Vector2>()),
            (float)context.startTime
        );
    }

    private void OnPrimaryContactEnd(InputAction.CallbackContext context)
    {
        OnEndTouch?.Invoke(
            ScreenToWorld(_mainCamera, _playerControls.PlayerActions.PrimaryPosition.ReadValue<Vector2>()),
            (float)context.time
        );
    }

    public Vector3 PrimaryPosition()
    {
        return ScreenToWorld(_mainCamera, _playerControls.PlayerActions.PrimaryPosition.ReadValue<Vector2>());

    }

    public static Vector3 ScreenToWorld(Camera camera, Vector3 position)
    {
        position.z = camera.nearClipPlane;
        return camera.ScreenToWorldPoint(position);
    }

    void OnMouseToggle(InputAction.CallbackContext context)
    {
        if (_playerControls.PlayerActions.Look.enabled) 
        {
            //Cursor.visible = true;
            //Cursor.lockState = CursorLockMode.None;
            _playerControls.PlayerActions.Look.Disable();
            _playerControls.PlayerActions.ThrowBall.Disable();
        }
        else
        { 
            //Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.Locked;
            _playerControls.PlayerActions.Look.Enable();
            _playerControls.PlayerActions.ThrowBall.Enable();
        }
    }
    
    void OnJumpPerformed(InputAction.CallbackContext context)
    {
        //_playerController.Jump();
        OnJump?.Invoke();
    }

    void OnThrowBallPerformed(InputAction.CallbackContext context)
    {
        //_playerController.ThrowBall();
        OnThrowBall?.Invoke();
    }

}
