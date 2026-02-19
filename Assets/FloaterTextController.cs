using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloaterTextController : MonoBehaviour
{   
    [SerializeField]
    TextMeshPro _TMP_ballPoints;
    [SerializeField]
    float _autoDisable = 5;
    [SerializeField]
    bool _playOnStart = false;
    [SerializeField]
    GameObject _rotateTowards;
    [SerializeField]
    bool _autoRotateTowards = false;
    

    bool _isActive = false;

    public void DisplayPoints(int points)
    {
        if (_isActive) StopCoroutine(nameof(_DisplayPoints));
        StartCoroutine(_DisplayPoints(points));
    }
    IEnumerator _DisplayPoints(int points)
    {   
        _isActive = true;
        _TMP_ballPoints.SetText(points.ToString());
        _TMP_ballPoints.gameObject.SetActive(true);
        _TMP_ballPoints.GetComponent<Animator>().Play("Float", 0, 0f);
        yield return new WaitForSeconds(_autoDisable);
        _TMP_ballPoints.gameObject.SetActive(false);
        _isActive = false;
    }

    private void Start()
    {
        if(_playOnStart) _TMP_ballPoints.GetComponent<Animator>().Play("Float", 0, 0f);
    }

    private void Update()
    {
        if (_autoRotateTowards)
        {
            Vector3 direction = _TMP_ballPoints.transform.position - _rotateTowards.transform.position;
            _TMP_ballPoints.transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
