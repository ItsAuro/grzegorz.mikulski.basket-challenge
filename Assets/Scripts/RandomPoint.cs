using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomPoint
{
    public static Vector3 RandomPointInBox(GameObject vertex1, GameObject vertex2)
    {
        Vector3 pos1 = vertex1.transform.position;
        Vector3 pos2 = vertex2.transform.position;

        return RandomPointInBox(pos1, pos2);
    }
    public static Vector3 RandomPointInBox(Vector3 vertex1, Vector3 vertex2)
    {
        Vector3 random_point = new Vector3(
            vertex1.x + Random.Range(vertex1.x, vertex2.x - vertex1.x),
            vertex1.y + Random.Range(vertex1.y, vertex2.y - vertex1.y),
            vertex1.z + Random.Range(vertex1.z, vertex2.z - vertex1.z)
        );

        return random_point;
    }
}
