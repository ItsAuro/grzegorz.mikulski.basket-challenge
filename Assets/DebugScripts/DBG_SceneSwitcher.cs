using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DBG_SceneSwitcher : MonoBehaviour
{
    public void SwitchScene(string targetScene)
    {   
        if (targetScene == null)
        {
            Debug.LogWarning("Debug scene switcher has no target scene");
            return;
        }

        SceneManager.LoadSceneAsync(targetScene);
    }
}
