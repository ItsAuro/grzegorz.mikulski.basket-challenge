using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    PlayerController _playerController;
    
    InputAction _moveAction;
    InputAction _jumpAction;
    InputAction _lookAction;

    InputAction _togglemouseAction;

    InputAction _throwballAction;

    void Start()
    {
        _lookAction = InputSystem.actions.FindAction("Look");
        _moveAction = InputSystem.actions.FindAction("Move");
        _jumpAction = InputSystem.actions.FindAction("Jump");
        _jumpAction.performed += OnJumpPerformed;

        _togglemouseAction = InputSystem.actions.FindAction("ToggleMouse");
        _togglemouseAction.performed += OnMouseToggle;

        _throwballAction = InputSystem.actions.FindAction("ThrowBall");
        _throwballAction.performed += OnThrowBallPerformed;
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input_movementVector = _moveAction.ReadValue<Vector2>();
        _playerController.Move(input_movementVector);

        Vector2 input_lookVector = _lookAction.ReadValue<Vector2>();
        _playerController.Look(input_lookVector);

    }

    void OnMouseToggle(InputAction.CallbackContext context)
    {
        if (_lookAction.enabled) 
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            _lookAction.Disable();
        }
        else
        { 
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            _lookAction.Enable(); 
        }
    }

    void OnJumpPerformed(InputAction.CallbackContext context)
    {
        _playerController.Jump();
    }


    void OnThrowBallPerformed(InputAction.CallbackContext context)
    {
        _playerController.ThrowBall();
    }
}
