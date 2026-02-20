using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour
{
    [SerializeField]
    bool _hideOnAwake = false;
    void Awake()
    {
        if( _hideOnAwake ) GetComponent<MeshRenderer>().enabled = false;
    }
}
