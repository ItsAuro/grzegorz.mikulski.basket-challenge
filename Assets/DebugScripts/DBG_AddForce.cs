using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBG_AddForce : MonoBehaviour
{
    [SerializeField]
    Vector3 Force;
    [SerializeField]
    ForceMode ForceMode;

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogWarning("Cannot apply force, missing Rigidbody component");
            return;
        }

        rb.AddForce(Force, ForceMode);
    }

}
