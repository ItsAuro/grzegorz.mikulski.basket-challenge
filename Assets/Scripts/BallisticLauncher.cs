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

    [Header("Target Data"), Space(10)]
    public Transform Target;
    public Transform ReflectAgainst;
    public float     ArrivalAngle = -60f;

    [Header("Aim Assist"), Space(10)]
    public AnimationCurve AimAssistCurve = AnimationCurve.Linear(-1,0,1,0);
    public float          AimAssistRange = 5f;


    public static Vector3 AimAssist(Vector3 raw, Vector3 optimal, float range, AnimationCurve assist_curve)
    {
        /*  Aim assist was thought up as a "magnet" towards the optimal vector
         *  modified by a curve f(x) where:
         *  f(x) = 0 -> Aim assist   0% , outputs same as raw
         *  f(x) = 1 -> Aim assist 100% , outputs optimal vector
         *  the curve can be added to penalise or reward accuracy
         *  activate only when undershoot or overshoot happens, and so on
         *  
         *  (-inf , -0  ] is undershoot
         *  [+0   , +inf) is overshoot
         *  
         *  X_out = ( 1 - f(d) ) * X_raw + f(d) * X_opt
         *  
         *  where:
         *  
         *           sign( ||X_raw|| - ||X_opt|| )   *   || X_raw - X_opt ||
         *  d  =   ------------------------------------------------------------
         *                                     range
         */

        float d = Mathf.Sign(raw.magnitude - optimal.magnitude) * (raw-optimal).magnitude / range;

        return ( 1 - assist_curve.Evaluate(d) ) * raw + assist_curve.Evaluate(d) * optimal;

    }
    public bool LaunchGameObject(GameObject obj, LaunchMode mode = LaunchMode.Direct, float forward_offset = 0f, float velocity = -1)
    {
        if (Target == null) return false;

        return mode switch
        {
            LaunchMode.Direct => LaunchDirect(obj, velocity),
            LaunchMode.Reflect => LaunchReflect(obj, forward_offset, velocity),
            _ => false,
        };
    }

    private bool LaunchDirect(GameObject obj, float velocity = -1)
    {
        bool solution_found = false;
        solution_found = Ballistics.SolveArcTargetAngle(
            obj.transform.position,
            Target.position,
            ArrivalAngle,
            Physics.gravity.y,
            out Vector3 optimal_velocity
            );
        if (!solution_found) { 
            return false; 
        }
        Rigidbody obj_rb = obj.GetComponent<Rigidbody>();
        if (obj_rb == null) return false;

       
        obj_rb.velocity = velocity < 0 ? optimal_velocity : AimAssist(optimal_velocity.normalized * velocity, optimal_velocity, AimAssistRange, AimAssistCurve);
        return true;
    }
    private bool LaunchReflect(GameObject obj, float forward_offset = 0f, float velocity = -1)
    {
        if (ReflectAgainst == null) return false;

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
            out Vector3 optimal_velocity
            );
        if (!solution_found)
        {
            return false;
        }
        Rigidbody obj_rb = obj.GetComponent<Rigidbody>();
        if (obj_rb == null) return false;

        obj_rb.velocity = velocity < 0 ? optimal_velocity : AimAssist(optimal_velocity.normalized * velocity, optimal_velocity, AimAssistRange, AimAssistCurve);

        return true;
    }
}
