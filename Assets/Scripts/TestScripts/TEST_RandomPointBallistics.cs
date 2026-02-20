using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_RandomPointBallistics : MonoBehaviour
{

    [SerializeField]
    GameObject _vertex1;
    [SerializeField]
    GameObject _vertex2;

    [SerializeField]
    BasketballFactory _basketballFactory;



    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(_ThrowBall), 1f, 4f);
    }

    void _ThrowBall()
    {
        Vector3 random_point = RandomPoint.RandomPointInBox(_vertex1 , _vertex2);
        GameObject ball = _basketballFactory.CreateBasketball(random_point, Quaternion.identity);
        _basketballFactory.LaunchBasketball(ball);
        _basketballFactory.DelayDestroy(ball, 6f);
    }
}
