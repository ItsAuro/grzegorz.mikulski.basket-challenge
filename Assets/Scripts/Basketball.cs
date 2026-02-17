using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basketball : MonoBehaviour
{

    int BallPoints = 0;


    private void Start()
    {
        
    }

    

    private void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.CompareTag("BasketballBoard"))
        {
            Debug.Log("Hit Board");
            int? PointBonus = collision.gameObject.GetComponent<BasketballBoard>()?.PointBonus;
            Debug.Log(PointBonus);
        }
        if (collision.gameObject.CompareTag("BasketballHoop"))
        {
            Debug.Log("Hit Hoop");
        }
    }
}
