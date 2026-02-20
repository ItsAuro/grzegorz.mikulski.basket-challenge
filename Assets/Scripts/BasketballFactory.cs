using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class BasketballFactory : MonoBehaviour
{

    [SerializeField]
    private GameObject _basketballPrefab;
    //[SerializeField]
    //private Transform _spawnOrigin;
    [SerializeField]
    private Vector3 _spawnOffset;


    private enum LaunchType
    {
        Direct,
        Reflect
    }
    [SerializeField]
    private LaunchType _launchType = LaunchType.Direct;
    [SerializeField]
    Transform _directTarget;
    [SerializeField]
    Transform _reflectTarget;
    [SerializeField]
    float _angleTarget = -60f;


    [SerializeField]
    private float _throwForce = 10f;

    

    public bool LaunchBasketball(GameObject basketball)
    {
        switch (_launchType)
        {
            case LaunchType.Direct:
                return LaunchBasketballDirect(basketball, _directTarget, _angleTarget);
            case LaunchType.Reflect:
                return LaunchBasketballReflect(basketball, _directTarget, _reflectTarget, _angleTarget);
            default: return false;
        }
    }
   
    public bool LaunchBasketballDirect(GameObject basketball, Transform target, float angle)
    {
        bool solution_found = false;
        solution_found = Ballistics.SolveArcTargetAngle(
            basketball.transform.position,
            target.position,
            angle,
            Physics.gravity.y,
            out Vector3 launch_velocity
            );
        if ( !solution_found ) return false;

        basketball.GetComponent<Rigidbody>().velocity = launch_velocity;
        return true;
    }
    public bool LaunchBasketballReflect(GameObject basketball, Transform target, Transform reflect, float angle)
    {

        Vector3 plane_point = reflect.gameObject.GetComponent<BoxCollider>()? 
            reflect.gameObject.GetComponent<BoxCollider>().ClosestPointOnBounds(target.position) - reflect.forward * basketball.GetComponent<Basketball>().BallDiameter / 2f 
            : reflect.position;
        
        Vector3 projected_point = Ballistics.ReflectPointAcrossPlane(
            target.position,
            plane_point,
            reflect.forward
            );

        bool solution_found = false;
        solution_found = Ballistics.SolveArcTargetAngle(
            basketball.transform.position,
            projected_point,
            angle,
            Physics.gravity.y,
            out Vector3 launch_velocity
            );
        if (!solution_found) return false;

        basketball.GetComponent<Rigidbody>().velocity = launch_velocity;
        return true;
    }
    public GameObject CreateBasketball()
    {
        GameObject basketball = Instantiate(
            _basketballPrefab,
            //_spawnOrigin.position + _spawnOrigin.rotation * _spawnOffset, 
            //_spawnOrigin.rotation
            transform.position + transform.rotation * _spawnOffset,
            transform.rotation
            );

        //GameObject basketball = Instantiate(_basketballPrefab, _spawnOrigin);
        //basketball.transform.localPosition = _spawnOffset;
        //basketball.transform.localRotation = Quaternion.identity;
        //basketball.transform.parent = null;

        //basketball.GetComponent<Rigidbody>().AddForce(basketball.transform.forward*_throwForce, ForceMode.Impulse);
        //StartCoroutine(DelayDestroy(basketball));
        
        return basketball;
    }
    public GameObject CreateBasketball(Vector3 position, Quaternion rotation)
    {
        GameObject basketball = Instantiate(
            _basketballPrefab,
            position + rotation * _spawnOffset,
            rotation
            );
        return basketball;
    }

    public GameObject DelayDestroy(GameObject obj, float delay)
    {
        StartCoroutine(_DelayDestroy(obj, delay));
        return obj;
    }
    private IEnumerator _DelayDestroy(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(5f);
        Destroy(obj);
    }


}
