using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class TEST_RandomPoint : MonoBehaviour
{
    [SerializeField]
    GameObject spawn;
    [SerializeField]
    GameObject vertex1;
    [SerializeField]
    GameObject vertex2;

    int counter = 0;
    int objects = 0;
  

    // Update is called once per frame
    void Update()
    {   
        if(objects < 100)
        {
            counter++;
            if (counter >= 80)
            {
                Instantiate(spawn, RandomPoint.RandomPointInBox(vertex1, vertex2), Quaternion.identity);
                objects++;
                counter = 0;
            }
        }
        

    }
}
