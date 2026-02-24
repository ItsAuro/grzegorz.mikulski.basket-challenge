using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class InputHandler : MonoBehaviour
{
    private IM_InputMapping _playerControls;
    private Camera          _mainCamera;
    private bool _primaryContact;

    public bool Movement { get { return _playerControls.Movement.enabled; }  set { if (value) _playerControls.Movement.Enable(); else _playerControls.Movement.Disable(); } }
    public bool Actions { get { return _playerControls.Actions.enabled; }  set { if (value) _playerControls.Actions.Enable(); else _playerControls.Actions.Disable(); } }
    public bool Swipes { get { return _playerControls.Swipes.enabled; }  set { if (value) _playerControls.Swipes.Enable(); else _playerControls.Swipes.Disable(); } }
    public bool Utility { get  { return _playerControls.Utility.enabled; }  set { if (value) _playerControls.Utility.Enable(); else _playerControls.Utility.Disable(); } }

    

    // events related to swipes
    public delegate void TouchStart(Vector2 position, float time);
    public delegate void TouchEnd(Vector2 position, float time);
    public event TouchStart OnStartTouch;
    public event TouchEnd OnEndTouch;

    // events related to player actions
    public delegate void Jump();
    public delegate void Move(Vector2 movement);
    public delegate void Look(Vector2 look);
    public delegate void ThrowBall(bool auto = true);
    public delegate void SwipeThrowUpdate(float distance);
    public delegate void SwipeThrowStarted();
    public delegate void SwipeThrowSuccessful(Vector2 direction, float power);
    public delegate void SwipeThrowCanceled();

    public event Jump                 OnJump;
    public event Move                 OnMove;
    public event Look                 OnLook;
    public event ThrowBall            OnThrow;
    public event SwipeThrowUpdate     OnSwipeThrowUpdate;
    public event SwipeThrowStarted    OnSwipeThrowStarted;
    public event SwipeThrowSuccessful OnSwipeThrowSuccessful;
    public event SwipeThrowCanceled   OnSwipeThrowCanceled;


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

        SetMouseVisibility(false);

        _playerControls.Swipes.PrimaryContact.started  += PrimaryContactStart;
        _playerControls.Swipes.PrimaryContact.canceled += PrimaryContactEnd;
        _playerControls.Movement.Jump.started          += JumpPerformed;
        _playerControls.Actions.Throw.started          += ThrowPerformed;
        _playerControls.Utility.ToggleMouse.started    += MouseTogglePerformed;
    }
    void Update()
    {
        OnLook?.Invoke(_playerControls.Movement.Look.ReadValue<Vector2>());
        OnMove?.Invoke(_playerControls.Movement.Move.ReadValue<Vector2>());  
    }
    private void PrimaryContactStart(InputAction.CallbackContext context)
    {
        _primaryContact = true;

        OnStartTouch?.Invoke(
            _playerControls.Swipes.PrimaryPosition.ReadValue<Vector2>(),
            (float)context.startTime
        );
    }
    private void PrimaryContactEnd(InputAction.CallbackContext context)
    {
        if (_primaryContact) OnEndTouch?.Invoke(
            _playerControls.Swipes.PrimaryPosition.ReadValue<Vector2>(),
            (float)context.time
        );

        _primaryContact = false;
    }

    public Vector3 Primary3DPosition()
    {
        return ScreenToWorld(_mainCamera, _playerControls.Swipes.PrimaryPosition.ReadValue<Vector2>());
    }

    public Vector2 Primary2DPosition()
    {
        return _playerControls.Swipes.PrimaryPosition.ReadValue<Vector2>();
    }

    public static Vector3 ScreenToWorld(Camera camera, Vector3 position)
    {
        position.z = camera.nearClipPlane + 0.5f;
        return camera.ScreenToWorldPoint(position);
    }

    private void MouseTogglePerformed(InputAction.CallbackContext context)
    {
        SetMouseVisibility(!Cursor.visible);
    }

    public void SetMouseVisibility(bool active)
    {
        if (active)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Swipes = true;
            Actions = false;
            Movement = false;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Swipes = false;
            Actions = true;
            Movement = true;
        }
    }


    

    private void JumpPerformed(InputAction.CallbackContext context)
    {
        OnJump?.Invoke();
    }

    private void ThrowPerformed(InputAction.CallbackContext context)
    {
        OnThrow?.Invoke();
    }


    public void SwipeUpdate(float distance)
    {
        OnSwipeThrowUpdate?.Invoke(distance);
    }
    public void SwipeStart()
    {
        OnSwipeThrowStarted?.Invoke();
    }
    public void SwipeSuccessful(Vector2 direction, float distance)
    {
        OnSwipeThrowSuccessful?.Invoke(direction.normalized, distance);
    }
    public void SwipeCanceled()
    {
        OnSwipeThrowCanceled?.Invoke();
    }








}
