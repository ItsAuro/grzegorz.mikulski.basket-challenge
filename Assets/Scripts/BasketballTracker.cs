using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketballTracker : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera _ballCamera;
    private Basketball _trackedBall;

    public void TrackBasketball(Basketball basketball)
    {
        if (_ballCamera != null)
        {
            
            // unsubscribe from current target if we're tracking
            if (_trackedBall != null)
            {   
                _trackedBall.OnBasketballDestroy -= ResetTrackingCamera;
                _trackedBall.OnBasketballScore   -= ResetTrackingCamera;
            }
            _trackedBall = basketball;

            // subscribe to target
            _trackedBall.OnBasketballDestroy += ResetTrackingCamera;
            _trackedBall.OnBasketballScore   += ResetTrackingCamera;

            _ballCamera.Follow = _trackedBall.transform;
            _ballCamera.Priority = 15;
        }
    }
    private void ResetTrackingCamera(Basketball basketball)
    {
        if (_trackedBall == basketball)
        {
            _trackedBall.OnBasketballDestroy -= ResetTrackingCamera;
            _trackedBall.OnBasketballScore   -= ResetTrackingCamera;

            _ballCamera.Priority = 5;
            _ballCamera.Follow   = null;
            _trackedBall         = null;
        }

    }
}
