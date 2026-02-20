using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BasketballShot
{
    Perfect,
    Backboard,
    HoopTouch,
    Miss
}
public class Basketball : MonoBehaviour
{
    public int BallPoints { get; private set; } = 1;
    public float BallDiameter { get { return transform.localScale.x; } }

    BasketballShot _shotType = BasketballShot.Miss;
    bool _isValid = false;
    bool _isLegal = true;

    

    private void Start()
    {
        
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
                GameState gameState = GameplayController.Instance.gameState;

                if (gameState.FireballStatus) BallPoints *= GameConfig.FIREBALL_MULTIPLIER;

                trigger.gameObject.GetComponent<FloaterTextController>()?.DisplayPoints(BallPoints);
                trigger.gameObject.GetComponent<ScoreFXController>()?.PlayFX();

                gameState.AddScore(BallPoints);
                gameState.AddFireball(GameConfig.FIREBALL_INCREMENT);



                _isValid = false;
            }

            

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("BasketballBoard"))
        {
            //Debug.Log("Hit Board");
            int PointBonus = collision.gameObject.GetComponent<BasketballBoard>().PointBonus;
            BallPoints = PointBonus;
        }
        else if (collision.gameObject.CompareTag("BasketballHoop"))
        {
            //Debug.Log("Hit Hoop");
        }
        
    }
}
