using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_BallisticsReflect: MonoBehaviour
{
    [SerializeField]
    GameObject ball;
    [SerializeField]
    GameObject target;
    [SerializeField]
    float targetAngle;
    [SerializeField]
    GameObject reflect;



    void Start()
    {

        BoxCollider bc = reflect.GetComponent<BoxCollider>();

        if(Ballistics.SolveArcTargetAngle(
            ball.transform.position,
            Ballistics.ReflectPointAcrossPlane(
                target.transform.position,
                bc.ClosestPointOnBounds(target.transform.position) - reflect.transform.forward * ball.GetComponent<Basketball>().BallDiameter/2f, 
                reflect.transform.forward ),
            targetAngle,
            Physics.gravity.y,
            out Vector3 initial_velocity
            ))
        {
            ball.GetComponent<Rigidbody>().velocity = initial_velocity;
        }
        else
        {
            Debug.LogWarning("Ballistic solver no solution");
        }


    }

}
