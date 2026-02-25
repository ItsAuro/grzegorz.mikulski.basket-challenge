using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_FloaterTextController : MonoBehaviour
{
    [SerializeField] private FloaterTextController floaterText;

    private void Start()
    {
        floaterText.DisplayPoints(10);
    }

}
