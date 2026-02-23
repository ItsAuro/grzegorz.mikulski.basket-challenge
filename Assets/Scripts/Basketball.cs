using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Basketball : MonoBehaviour
{
    [Flags]
    public enum ShotType
    {
        None = 0,
        Perfect = 1 << 0,
        Backboard = 1 << 1,
        HoopTouch = 1 << 2,
    }
    public enum ScoreType
    {
        Scored,
        Missed,
    }

    [SerializeField]
    ParticleSystem _fireTrail;
    public int   BallPoints   { get; private set; } = 1;
    public float BallDiameter { get { return transform.localScale.x; } }
    ShotType  _shotType  = ShotType.None;
    ScoreType _scoreType = ScoreType.Missed;
    bool _isValid    = false;
    bool _isLegal    = true;
    bool _autoDelete = true;
    int  _lifetime   = 5;

    public event Action OnBasketballDestroy;
    public event Action OnBasketballScore;

    private void _AutoDelete()
    {
        _lifetime -= 1;
        if( _lifetime <= 0)
        {
            CancelInvoke(nameof(_AutoDelete));
            if (_scoreType == ScoreType.Missed) 
            { 
                GameplayController.Instance?.gameState.ResetFireball();
            }
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        OnBasketballDestroy?.Invoke();
    }
    private void Start()
    {   
        if (GameplayController.Instance?.gameState.FireballStatus == true) _fireTrail?.Play();

        if( _autoDelete)
        {
            InvokeRepeating(nameof(_AutoDelete), 0, 1);
        }
    }
    private void OnTriggerEnter(Collider trigger)
    {       
        if (trigger.gameObject.CompareTag("TriggerScoreValidate"))
        {
            _isValid = true;
        }
        else if (trigger.gameObject.CompareTag("TriggerScoreFinalise"))
        {
            if (_isLegal && !_isValid) {
                _isLegal = false;
            }

            if (_isLegal && _isValid)
            {
                //Debug.Log("Scored");
                OnBasketballScore?.Invoke();
                if (_shotType == ShotType.None) _shotType |= ShotType.Perfect;
                _scoreType = ScoreType.Scored;

                if (GameplayController.Instance?.gameState.FireballStatus == true) BallPoints *= GameConfig.FIREBALL_MULTIPLIER;

                trigger.gameObject.GetComponent<FloaterTextController>()?.DisplayPoints(BallPoints);
                trigger.gameObject.GetComponent<ScoreFXController>()?.PlayFX();

                GameplayController.Instance?.gameState.AddScore(BallPoints);
                GameplayController.Instance?.gameState.AddFireball(GameConfig.FIREBALL_INCREMENT);

                _isValid = false;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("BasketballBoard"))
        {
            _shotType |= ShotType.Backboard;
            //Debug.Log("Hit Board");

            int PointBonus = collision.gameObject.GetComponent<BasketballBoard>().PointBonus;
            BallPoints = PointBonus;
        }
        else if (collision.gameObject.CompareTag("BasketballHoop"))
        {
            _shotType |= ShotType.HoopTouch;
            //Debug.Log("Hit Hoop");
        }

    }
}
