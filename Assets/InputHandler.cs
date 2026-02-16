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

    void Start()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
        _jumpAction = InputSystem.actions.FindAction("Jump");
        _lookAction = InputSystem.actions.FindAction("Look");

        _jumpAction.performed += OnJumpTriggered;

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input_movementVector = _moveAction.ReadValue<Vector2>();
        _playerController.Move(input_movementVector);

        Vector2 input_lookVector = _lookAction.ReadValue<Vector2>();
        _playerController.Look(input_lookVector);

    }

    void OnJumpTriggered(InputAction.CallbackContext context)
    {
        _playerController.Jump();
    }
}
