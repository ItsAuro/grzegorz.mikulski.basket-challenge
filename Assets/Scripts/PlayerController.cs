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


    //TESTING/DEBUG
    [SerializeField]
    BasketballFactory _basketballFactory;


    [SerializeField]
    float _movementSpeed = 5f;
    [SerializeField]
    float _rotationSpeed = 20f;
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
        movementDelta *= _movementSpeed * Time.deltaTime;
        _characterController.Move(movementDelta);
    }
    public void Look(Vector2 lookVector)
    {
        // X rotates body Y
        transform.localRotation *= Quaternion.Euler(Vector3.up * _rotationSpeed * lookVector.x * Time.deltaTime);

        //Y rotates head X
        _headAngle = Mathf.Clamp(_headAngle + _rotationSpeed * lookVector.y * Time.deltaTime, -90, 90);
        _head.transform.localRotation = Quaternion.Euler(Vector3.left * _headAngle);
    }
    public void Jump()
    {
        if (!_characterController.isGrounded) return;
        _velocityY = _jumpForce;
    }
    public void ThrowBall()
    {
        _basketballFactory.CreateBasketball();
    }



    void Start()
    {

    }

    void Update()
    {
        // character gravity pull down
        if (!_characterController.isGrounded)
        {
            _velocityY -= _gravity * Time.deltaTime;
        }
        else 
        {
            _velocityY = -1f;
        }

        _characterController.Move(Vector3.up * _velocityY * Time.deltaTime);

    }
}
