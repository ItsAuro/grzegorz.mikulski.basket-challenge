using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class BasketballFactory : MonoBehaviour
{
    [SerializeField]
    private GameObject _basketballPrefab;
    [SerializeField]
    private Vector3 _spawnOffset;
    [SerializeField]
    private CinemachineVirtualCamera _ballCamera;
    private GameObject _trackedBall;

    
    public GameObject CreateBasketball()
    {
        return CreateBasketball(transform.position + transform.rotation * _spawnOffset, transform.rotation);
    }
    public GameObject CreateBasketball(Vector3 position, Quaternion rotation)
    {
        GameObject basketball = Instantiate(
            _basketballPrefab,
            position + rotation * _spawnOffset,
            rotation
            );

        if (_ballCamera != null)
        {
            if (_trackedBall)
            {
                _trackedBall.GetComponent<Basketball>().OnBasketballDestroy -= ResetTrackingCamera;
                _trackedBall.GetComponent<Basketball>().OnBasketballScore -= ResetTrackingCamera;
            }
            _trackedBall = basketball;
            _trackedBall.GetComponent<Basketball>().OnBasketballDestroy += ResetTrackingCamera;
            _trackedBall.GetComponent<Basketball>().OnBasketballScore += ResetTrackingCamera;
            _ballCamera.Follow = _trackedBall.transform;
            //_ballCamera.LookAt = _trackedBall.transform;
            _ballCamera.Priority = 15;
        }

        return basketball;
    }
    private void ResetTrackingCamera()
    {
        _ballCamera.Priority = 5;
        _ballCamera.Follow = null;
        //_ballCamera.LookAt = null;
        _trackedBall = null;
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
