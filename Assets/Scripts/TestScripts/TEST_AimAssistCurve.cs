using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_AimAssistCurve : MonoBehaviour
{

    [SerializeField]
    AnimationCurve curve;

    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            // test for out of bounds curve
            Debug.Log(curve.Evaluate(i));
        }
    }

}
