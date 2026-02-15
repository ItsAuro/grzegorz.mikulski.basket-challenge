using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasketballFactory : MonoBehaviour
{

    [SerializeField]
    private GameObject BasketballPrefab;
    [SerializeField]
    private Transform SpawnOrigin;

    private void CreateBasketball()
    {
        GameObject basketball = Instantiate(BasketballPrefab, SpawnOrigin.position + SpawnOrigin.forward*10, SpawnOrigin.rotation);
        basketball.GetComponent<Rigidbody>().AddForce(SpawnOrigin.forward*10, ForceMode.Impulse);
        StartCoroutine(DelayDestroy(basketball));
    }
    private IEnumerator DelayDestroy(GameObject obj)
    {
        yield return new WaitForSeconds(5f);
        Destroy(obj);
    }

    //private void OnMouseDown()
    //{
    //    Debug.Log("Mouse Clicked!");
    //    CreateBasketBall();
    //
    //   
    //}


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CreateBasketball();
        }
        
    }


}
