using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasketballFactory : MonoBehaviour
{

    [SerializeField]
    private GameObject _basketballPrefab;
    [SerializeField]
    private Transform _spawnOrigin;
    [SerializeField]
    private Vector3 _spawnOffset;

    [SerializeField]
    private float _throwForce = 10f;

    public void CreateBasketball()
    {
        GameObject basketball = Instantiate(
            _basketballPrefab, 
            _spawnOrigin.position + _spawnOrigin.rotation * _spawnOffset, 
            _spawnOrigin.rotation
            );

        //GameObject basketball = Instantiate(_basketballPrefab, _spawnOrigin);
        //basketball.transform.localPosition = _spawnOffset;
        //basketball.transform.localRotation = Quaternion.identity;
        //basketball.transform.parent = null;

        basketball.GetComponent<Rigidbody>().AddForce(basketball.transform.forward*_throwForce, ForceMode.Impulse);
        StartCoroutine(DelayDestroy(basketball));
    }
    private IEnumerator DelayDestroy(GameObject obj)
    {
        yield return new WaitForSeconds(5f);
        Destroy(obj);
    }


}
