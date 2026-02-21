using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private IM_InputMapping _playerControls;
    private Camera _mainCamera;


    public delegate void TouchStart(Vector2 position, float time);
    public delegate void TouchEnd(Vector2 position, float time);


    public delegate void Jump();
    public delegate void Move(Vector2 movement);
    public delegate void Look(Vector2 look);
    public delegate void ThrowBall(bool auto = true);

    public delegate void PowerUpdate(float distance);
    public delegate void PowerEnd(Vector2 direction);


    public event TouchStart OnStartTouch;
    public event TouchEnd   OnEndTouch;


    public event Jump       OnJump;
    public event Move       OnMove;
    public event Look       OnLook;
    public event ThrowBall  OnThrowBall;

    public event PowerUpdate OnPowerUpdate;
    public event PowerEnd    OnPowerEnd;


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

        _playerControls.PlayerActions.PrimaryContact.started  += PrimaryContactStart;
        _playerControls.PlayerActions.PrimaryContact.canceled += PrimaryContactEnd;
        _playerControls.PlayerActions.Jump.started            += JumpPerformed;
        _playerControls.PlayerActions.ThrowBall.started       += ThrowBallPerformed;
        _playerControls.PlayerActions.ToggleMouse.started     += MouseToggle;

    }

    void Update()
    {
        OnLook?.Invoke(_playerControls.PlayerActions.Look.ReadValue<Vector2>());
        OnMove?.Invoke(_playerControls.PlayerActions.Move.ReadValue<Vector2>());  
    }


    private void PrimaryContactStart(InputAction.CallbackContext context)
    {
        OnStartTouch?.Invoke(
            //ScreenToWorld(_mainCamera, _playerControls.PlayerActions.PrimaryPosition.ReadValue<Vector2>()),
            _playerControls.PlayerActions.PrimaryPosition.ReadValue<Vector2>(),
            (float)context.startTime
        );
    }

    private void PrimaryContactEnd(InputAction.CallbackContext context)
    {
        OnEndTouch?.Invoke(
            //ScreenToWorld(_mainCamera, _playerControls.PlayerActions.PrimaryPosition.ReadValue<Vector2>()),
            _playerControls.PlayerActions.PrimaryPosition.ReadValue<Vector2>(),
            (float)context.time
        );
    }

    public Vector3 Primary3DPosition()
    {
        return ScreenToWorld(_mainCamera, _playerControls.PlayerActions.PrimaryPosition.ReadValue<Vector2>());
    }

    public Vector2 Primary2DPosition()
    {
        return _playerControls.PlayerActions.PrimaryPosition.ReadValue<Vector2>();
    }

    public static Vector3 ScreenToWorld(Camera camera, Vector3 position)
    {
        position.z = camera.nearClipPlane + 0.5f;
        return camera.ScreenToWorldPoint(position);
    }

    private void MouseToggle(InputAction.CallbackContext context)
    {
        if (_playerControls.PlayerActions.Look.enabled) 
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            _playerControls.PlayerActions.Look.Disable();
            _playerControls.PlayerActions.ThrowBall.Disable();
            _playerControls.PlayerActions.PrimaryContact.Enable();
        }
        else
        { 
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            _playerControls.PlayerActions.Look.Enable();
            _playerControls.PlayerActions.ThrowBall.Enable();
            _playerControls.PlayerActions.PrimaryContact.Disable();

        }
    }

    private void JumpPerformed(InputAction.CallbackContext context)
    {
        OnJump?.Invoke();
    }

    private void ThrowBallPerformed(InputAction.CallbackContext context)
    {
        OnThrowBall?.Invoke();
    }




    public void SwipeUpdate(float power)
    {
        OnPowerUpdate?.Invoke(power);
    }
    public void SwipeEnd(Vector2 direction)
    {
        OnPowerEnd?.Invoke(direction);
    }
}
