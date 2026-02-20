using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class BasketballFactory : MonoBehaviour
{
    [SerializeField]
    private GameObject _basketballPrefab;
    [SerializeField]
    private Vector3 _spawnOffset;

    public GameObject CreateBasketball()
    {
        GameObject basketball = Instantiate(
            _basketballPrefab,
            transform.position + transform.rotation * _spawnOffset,
            transform.rotation
            );
        return basketball;
    }
    public GameObject CreateBasketball(Vector3 position, Quaternion rotation)
    {
        GameObject basketball = Instantiate(
            _basketballPrefab,
            position + rotation * _spawnOffset,
            rotation
            );
        return basketball;
    }

    public GameObject DelayDestroy(GameObject obj, float delay)
    {
        StartCoroutine(_DelayDestroy(obj, delay));
        return obj;
    }
    private IEnumerator _DelayDestroy(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(5f);
        Destroy(obj);
    }


}
