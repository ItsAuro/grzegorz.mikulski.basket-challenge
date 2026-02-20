using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallisticLauncher : MonoBehaviour
{
    public enum LaunchMode
    {
        Direct,
        Reflect
    }

    public Transform Target;
    public Transform ReflectAgainst;
    public float ArrivalAngle = -60f;

    public bool LaunchGameObject(GameObject obj, LaunchMode mode = LaunchMode.Direct, float forward_offset = 0f)
    {
        return mode switch
        {
            LaunchMode.Direct => LaunchDirect(obj),
            LaunchMode.Reflect => LaunchReflect(obj, forward_offset),
            _ => false,
        };
    }

    private bool LaunchDirect(GameObject obj)
    {
        bool solution_found = false;
        solution_found = Ballistics.SolveArcTargetAngle(
            obj.transform.position,
            Target.position,
            ArrivalAngle,
            Physics.gravity.y,
            out Vector3 launch_velocity
            );
        if (!solution_found) { 
            return false; 
        }
        Rigidbody obj_rb = obj.GetComponent<Rigidbody>();
        if (obj_rb == null) return false;

        obj_rb.velocity = launch_velocity;
        return true;
    }
    private bool LaunchReflect(GameObject obj, float forward_offset = 0f)
    {
        Vector3 plane_point = ReflectAgainst.gameObject.GetComponent<BoxCollider>() ?
            ReflectAgainst.gameObject.GetComponent<BoxCollider>().ClosestPointOnBounds(Target.position) - ReflectAgainst.forward * forward_offset
            : ReflectAgainst.position;

        Vector3 projected_point = Ballistics.ReflectPointAcrossPlane(
            Target.position,
            plane_point,
            ReflectAgainst.forward
            );

        bool solution_found = false;
        solution_found = Ballistics.SolveArcTargetAngle(
            obj.transform.position,
            projected_point,
            ArrivalAngle,
            Physics.gravity.y,
            out Vector3 launch_velocity
            );
        if (!solution_found)
        {
            return false;
        }
        Rigidbody obj_rb = obj.GetComponent<Rigidbody>();
        if (obj_rb == null) return false;

        obj_rb.velocity = launch_velocity;
        return true;
    }




    public static bool LaunchDirect(GameObject obj, Transform target, float arrival_angle)
    {
        bool solution_found = false;
        solution_found = Ballistics.SolveArcTargetAngle(
            obj.transform.position,
            target.position,
            arrival_angle,
            Physics.gravity.y,
            out Vector3 launch_velocity
            );
        if (!solution_found)
        { 
            return false;
        }
        Rigidbody obj_rb = obj.GetComponent<Rigidbody>();
        if (obj_rb == null) return false;

        obj_rb.velocity = launch_velocity;
        return true;
    }
    public static bool LaunchReflect(GameObject obj, Transform target, Transform reflect_against, float arrival_angle, float forward_offset = 0f)
    {
        Vector3 plane_point = reflect_against.gameObject.GetComponent<BoxCollider>() ?
            reflect_against.gameObject.GetComponent<BoxCollider>().ClosestPointOnBounds(target.position) - reflect_against.forward * forward_offset
            : reflect_against.position;

        Vector3 projected_point = Ballistics.ReflectPointAcrossPlane(
            target.position,
            plane_point,
            reflect_against.forward
            );

        bool solution_found = false;
        solution_found = Ballistics.SolveArcTargetAngle(
            obj.transform.position,
            projected_point,
            arrival_angle,
            Physics.gravity.y,
            out Vector3 launch_velocity
            );
        if (!solution_found)
        {
            return false;
        }
        Rigidbody obj_rb = obj.GetComponent<Rigidbody>();
        if (obj_rb == null) return false;

        obj_rb.velocity = launch_velocity;
        return true;
    }
}
