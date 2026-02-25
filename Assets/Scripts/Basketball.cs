using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Basketball : MonoBehaviour
{
    [Flags]
    public enum ShotType
    {
        None      = 0,
        Perfect   = 1 << 0,
        Backboard = 1 << 1,
        HoopTouch = 1 << 2,
    }
    public enum ScoreType
    {
        Scored,
        Missed,
    }
    public int BallPoints { get; private set; } = 1;
    public float BallDiameter { get { return transform.localScale.x; } }
    public ShotType BallShotType { get; private set; } = ShotType.None;
    public ScoreType BallScoreType { get; private set; } = ScoreType.Missed;

    [Header("Fire Trail"), Space(10)]
    [SerializeField] ParticleSystem _fireTrail;
    
    [Header("Sounds"), Space(10)]
    public List<AudioClip> BallBounceSounds;
    public List<AudioClip> BallHoopSounds;
    public AudioClip BallScoreSound;

    [Header("Parameters"), Space(10)]
    [SerializeField] bool _autoDelete = true;
    [SerializeField] int  _lifetime   = 5;

    bool _isValid    = false;
    bool _isLegal    = true;
    

    public delegate void Despawn(Basketball ball);
    public delegate void Basket(Basketball ball);
    public delegate void Miss(Basketball ball);
    public event Despawn OnDespawn;
    public event Basket  OnBasket;
    public event Miss    OnMiss;

    private void _AutoDelete()
    {
        _lifetime -= 1;
        if( _lifetime <= 0)
        {
            CancelInvoke(nameof(_AutoDelete));

            if (BallScoreType == ScoreType.Missed)
                OnMiss?.Invoke(this);

            Destroy(gameObject);
        }
    }
    public void ActivateFireTrail()
    {   
        _fireTrail?.Play();
    }
    private void OnDestroy()
    {
        _fireTrail?.Stop();
        OnDespawn?.Invoke(this);
    }
    private void Start()
    {   
        if( _autoDelete) InvokeRepeating(nameof(_AutoDelete), 0, 1);
    }
    private void OnTriggerEnter(Collider trigger)
    {       
        if (trigger.gameObject.CompareTag("TriggerScoreValidate"))
        {
            // validate if the ball enters from the top
            _isValid = true;
        }
        else if (trigger.gameObject.CompareTag("TriggerScoreFinalise"))
        {
            if (_isLegal && !_isValid) {
                // ball enters from below
                _isLegal = false;
            }

            if (_isLegal && _isValid)
            {
                // ball enters from above and is valid, basket successful
                if (BallShotType == ShotType.None) BallShotType |= ShotType.Perfect;
                BallScoreType = ScoreType.Scored;
                OnBasket?.Invoke(this);
                if(BallScoreSound) AudioSource.PlayClipAtPoint(BallScoreSound, transform.position);

                _isValid = false;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        

        if(collision.gameObject.CompareTag("BasketballBoard"))
        {
            // board hit
            BallShotType |= ShotType.Backboard;
            int PointBonus = collision.gameObject.GetComponent<BasketballBoard>().PointBonus;
            BallPoints = PointBonus;

            if (BallBounceSounds.Count > 0)
            {
                AudioClip ac_bounce = BallBounceSounds[(int)UnityEngine.Random.Range(0, BallBounceSounds.Count)];
                AudioSource.PlayClipAtPoint(ac_bounce, transform.position);
            }
        }
        else if (collision.gameObject.CompareTag("BasketballHoop"))
        {
            // hoop hit
            BallShotType |= ShotType.HoopTouch;
            if (BallHoopSounds.Count > 0)
            {
                AudioClip ac_bounce = BallHoopSounds[(int)UnityEngine.Random.Range(0, BallHoopSounds.Count)];
                AudioSource.PlayClipAtPoint(ac_bounce, transform.position);
            }
        }
        else
        {
            if (BallBounceSounds.Count > 0)
            {
                AudioClip ac_bounce = BallBounceSounds[(int)UnityEngine.Random.Range(0, BallBounceSounds.Count)];
                AudioSource.PlayClipAtPoint(ac_bounce, transform.position);
            }
        }

    }
}
