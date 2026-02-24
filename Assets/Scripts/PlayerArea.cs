using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArea : MonoBehaviour
{

    [SerializeField] private Vector3 size = Vector3.one;
    [SerializeField] public Transform FocalPoint;

    public Vector3 GetPoint()
    {
        return RandomPoint.RandomPointInBox(transform, size);
    }

    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Vector3.zero, size);
    }
}