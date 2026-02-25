using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _TMP_MaxTime;
    [SerializeField] private Slider _Slider_MaxTime;

    const string _s_maxTime = "Max Time {0}";

    public void SetGameMaxTime()
    {
        int time = (int)_Slider_MaxTime.value;
        _TMP_MaxTime?.SetText(_s_maxTime, time);
        GameConfig.MAX_TIME = time;
    }
    public void Start()
    {
        _Slider_MaxTime.SetValueWithoutNotify(GameConfig.MAX_TIME);
        _TMP_MaxTime?.SetText(_s_maxTime, GameConfig.MAX_TIME);
    }
}
