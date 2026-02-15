using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject _Head;
    private CharacterController _Character;
    private Rigidbody _Rigidbody;

    private const float JUMP_FORCE = 10f;


    private InputAction _jump;


    // Start is called before the first frame update
    void Start()
    {
        _Character = GetComponent<CharacterController>();
        _Rigidbody = GetComponent<Rigidbody>();

        _jump = InputSystem.actions.FindAction("Jump");


    }

    // Update is called once per frame
    void Update()
    {


        if (_jump.triggered){
            //Vector3 jumpDirection = new Vector3(0, 1, 0);
            //Vector3 moveDirection = transform.forward + jumpDirection * Time.deltaTime;
            //_Character.SimpleMove(moveDirection);            
        }

    }
}
