using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreFXController : MonoBehaviour
{
    [SerializeField]
    ParticleSystem _particleSystem;

    public void PlayFX()
    {
        _particleSystem?.Play();
    }
}
