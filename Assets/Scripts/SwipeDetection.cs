using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{

    private Vector2 startPosition;
    private float startTime;
    private Vector2 endPosition;
    private float endTime;

    [SerializeField]
    private InputHandler inputHandler;
    [SerializeField]
    private float minimumDistance = .2f;
    [SerializeField]
    private float maximumTime = 1.0f;
    [SerializeField, Range(0f,1f)]
    private float directionThreshold = .9f;

    [SerializeField]
    bool trailEnabled = false;
    [SerializeField]
    private TrailRenderer trail;
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

    private void SwipeStart(Vector2 position, float time)
    {
        startPosition = position;
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
            float distance = Vector2.Distance(startPosition, trail.transform.position);
            inputHandler.SwipeUpdate(distance);
            yield return null;
        }
    }


    private void SwipeEnd(Vector2 position, float time)
    {
        if (trailEnabled)
        {
            trail.enabled = false;
            StopCoroutine(trailCoroutine);
        }

        StopCoroutine(swipeCoroutine);
        endPosition = position;
        endTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if (Vector2.Distance(startPosition, endPosition) >= minimumDistance && (endTime - startTime) <= maximumTime)
        {
            Vector2 direction = endPosition - startPosition;
            inputHandler.SwipeEnd(direction);
            //SwipeDirection(direction.normalized);
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
