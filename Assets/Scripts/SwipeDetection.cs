using System.Collections;
using System.Collections.Generic;
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
    private TrailRenderer trail;
    private Coroutine coroutine;


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
        trail.transform.position = startPosition;
        //trail.SetActive(true);
        trail.Clear();
        trail.enabled = true;
        coroutine = StartCoroutine(UpdateTrail());
    }

    IEnumerator UpdateTrail()
    {
        while (true)
        {
            trail.transform.position = inputHandler.PrimaryPosition();
            yield return null;
        }
    }


    private void SwipeEnd(Vector2 position, float time)
    {
        //trail.SetActive(false);
        trail.enabled = false;

        StopCoroutine(coroutine);
        endPosition = position;
        endTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if (Vector3.Distance(startPosition, endPosition) >= minimumDistance && (endTime - startTime) <= maximumTime)
        {
            Debug.Log("Swiped");
            Vector3 direction = endPosition - startPosition;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
            SwipeDirection(direction2D);
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
