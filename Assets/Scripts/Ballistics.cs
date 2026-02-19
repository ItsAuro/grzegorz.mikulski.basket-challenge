using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Ballistics
{
    public static bool SolveArcTargetAngle(
        Vector3 start_position,
        Vector3 target_position,
        float impact_angle,
        float gravity,
        out Vector3 initial_velocity
        )
    {
        initial_velocity = Vector3.zero;

        Vector3 horizontal_delta = new Vector3(target_position.x - start_position.x, 0f, target_position.z - start_position.z);
        float R = horizontal_delta.magnitude;
        float H = target_position.y - start_position.y;
        if (R < 0.001f) return false;

        // launch angle theta_0
        // atan(
        //                           H - R * tan(theta_i)
        //     tan(theta_i) + 2 * ----------------------------
        //                                    R
        // )

        // launch speed v_0
        // sqrt(
        //                         g * R
        //   ---------------------------------------------------
        //     cos^2(theta_0) * [tan(theta_0) - tan(theta_i)]
        // )

        // launch direction
        //                           (x_t - x_0 , 0 , z_t - z_0)
        //   v_0 * cos(theta_0) * ---------------------------------  + (0 , v_0 * sin(theta_0) , 0)
        //                                        R

        // launch angle
        float theta_i = impact_angle * Mathf.Deg2Rad;
        float tan_ti = Mathf.Tan(theta_i);
        float tan_t0 = tan_ti + 2f * (H - R * tan_ti) / R;
        if (tan_t0 <= tan_ti) return false; // must be positive or solution is invalid
        float theta0 = Mathf.Atan(tan_t0);

        // launch speed
        float v0v0 = -gravity * R / (Mathf.Cos(theta0) * Mathf.Cos(theta0) * (tan_t0 - tan_ti));
        if (v0v0 <= 0f) return false; // must be positive or solution is invalid
        float v0 = Mathf.Sqrt(v0v0);

        // initial velocity vector
        Vector3 dirHorizontal = horizontal_delta.normalized;
        initial_velocity = dirHorizontal * (v0 * Mathf.Cos(theta0)) + Vector3.up * (v0 * Mathf.Sin(theta0));

        return true;
    }

    public static Vector3 ReflectPointAcrossPlane(Vector3 point, Vector3 planePoint, Vector3 planeNormal)
    {
        Vector3 toPoint = point - planePoint;
        float distance = Vector3.Dot(toPoint, planeNormal);

        //Debug.Log(point);
        //Debug.Log(planePoint);
        //Debug.Log(point - 2f * distance * planeNormal);
        return point - 2f * distance * planeNormal;
    }

}
