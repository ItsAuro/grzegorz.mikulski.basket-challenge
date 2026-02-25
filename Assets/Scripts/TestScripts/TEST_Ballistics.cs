using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_Ballistics : MonoBehaviour
{
    [SerializeField] private GameObject gm;
    [SerializeField] private GameObject target;
    [SerializeField] private float targetAngle;

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

}
