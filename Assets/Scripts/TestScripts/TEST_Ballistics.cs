using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_Ballistics : MonoBehaviour
{
    [SerializeField]
    GameObject gm;
    [SerializeField]
    GameObject target;
    [SerializeField]
    float targetAngle;



    void Start()
    {
        if(Ballistics.SolveArcTargetAngle(
            gm.transform.position,
            target.transform.position,
            targetAngle,
            Physics.gravity.y,
            out Vector3 initial_velocity
            ))
        {
            gm.GetComponent<Rigidbody>().velocity = initial_velocity;
        }
        else
        {
            Debug.LogWarning("Ballistic solver no solution");
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
