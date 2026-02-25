using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UIElements;

public class SwipeDetection : MonoBehaviour
{

    private Vector2 startPosition;
    private float startTime;
    private Vector2 endPosition;
    private float endTime;

    [SerializeField]                private InputHandler inputHandler;
    [SerializeField]                private float        minimumDistance    = 0.2f;
    [SerializeField]                private float        maximumTime        = 1.0f;
    [SerializeField, Range(0f,1f)]  private float        directionThreshold = 0.9f;
    [SerializeField]                private float        clampMaximumDistance = 500f;

    [SerializeField] private bool trailEnabled = false;
    [SerializeField] private TrailRenderer trail;
    private Coroutine trailCoroutine;
    private Coroutine swipeCoroutine;



    private void OnEnable()
    {
        inputHandler.OnStartTouch += SwipeStart;
        inputHandler.OnEndTouch   += SwipeEnd;
    }
    private void OnDisable()
    {
        inputHandler.OnStartTouch -= SwipeStart;
        inputHandler.OnEndTouch   -= SwipeEnd;
    }
    private void SwipeStart(float time)
    {   
        // swipe started
        inputHandler.SwipeStart();

        startPosition = inputHandler.Primary2DPosition();
        endPosition   = inputHandler.Primary2DPosition();
        startTime = time;
        swipeCoroutine = StartCoroutine(SwipeUpdate());

        if (trailEnabled)
        {
            trail.transform.position = startPosition;
            trail.Clear();
            trail.enabled = true;
            trailCoroutine = StartCoroutine(UpdateTrail());
        }
    }
    IEnumerator UpdateTrail()
    {
        while (true)
        {
            trail.transform.position = inputHandler.Primary3DPosition();
            yield return null;
        }
    }
    IEnumerator SwipeUpdate()
    {
        while (true)
        {   
            // swipe ongoing
            float distance = Vector2.Distance(startPosition, inputHandler.Primary2DPosition());
            inputHandler.SwipeUpdate(Mathf.Clamp01(distance / clampMaximumDistance));
            yield return null;
        }
    }
    private void SwipeEnd(float time)
    {
        if (trailEnabled)
        {
            trail.enabled = false;
            StopCoroutine(trailCoroutine);
        }

        StopCoroutine(swipeCoroutine);
        endPosition = inputHandler.Primary2DPosition();
        endTime = time;
        DetectSwipe();
    }
    private void DetectSwipe()
    {
        float distance = Vector2.Distance(startPosition, endPosition);
        if (distance >= minimumDistance && (endTime - startTime) <= maximumTime)
        {
            Vector2 direction = endPosition - startPosition;
            // swipe successful
            inputHandler.SwipeSuccessful(direction, Mathf.Clamp01(distance / clampMaximumDistance));
            //SwipeDirection(direction.normalized);
        }
        else
        {   
            // swipe canceled
            inputHandler.SwipeCanceled();
        }
    }

    private void SwipeDirection(Vector2 direction)
    {
        if( Vector2.Dot(Vector2.up, direction) > directionThreshold)
        {
            Debug.Log("Swipe up");
        }
        else if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
        {
            Debug.Log("Swipe down");
        }
        else if(Vector2.Dot(Vector2.left, direction) > directionThreshold)
        {
            Debug.Log("Swipe left");
        }
        else if(Vector2.Dot(Vector2.right, direction) > directionThreshold)
        {
            Debug.Log("Swipe right");
        }
    }



}
