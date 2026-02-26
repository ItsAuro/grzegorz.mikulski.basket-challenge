using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// singleton class
public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic _instance;
    public static BackgroundMusic Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this) Destroy(gameObject);
        else 
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    [SerializeField] private AudioSource _source;

    public void PlayFromStart()
    {
        if (!_source) return;

        _source.Stop();
        _source.time = 0;
        _source.Play();
    }
    public void Stop()
    {
        if (!_source) return;
        _source.Stop();
    }

}
