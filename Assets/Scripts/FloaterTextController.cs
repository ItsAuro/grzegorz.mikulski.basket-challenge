using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloaterTextController : MonoBehaviour
{   
    [SerializeField] TextMeshPro _TMP_ballPoints;
    [SerializeField] Transform   _rotateTowards;
    [SerializeField] float       _autoDisable = 5;
    
    Coroutine _displayCoroutine = null;

    public void DisplayPoints(int points)
    {
        if (_displayCoroutine != null) StopCoroutine(_displayCoroutine);
        _displayCoroutine = StartCoroutine(_DisplayPoints(points));
    }
    private IEnumerator _DisplayPoints(int points)
    {   
        _TMP_ballPoints.SetText(points.ToString());
        _TMP_ballPoints.gameObject.SetActive(true);
        _TMP_ballPoints.GetComponent<Animator>().Play("Float", 0, 0f);
        yield return new WaitForSeconds(_autoDisable);
        _TMP_ballPoints.gameObject.SetActive(false);
        _displayCoroutine = null;
    }

    private void Awake()
    {
        _TMP_ballPoints.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_rotateTowards != null)
        {
            Vector3 direction = _TMP_ballPoints.transform.position - _rotateTowards.position;
            _TMP_ballPoints.transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
