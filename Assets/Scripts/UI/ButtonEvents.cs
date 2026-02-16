using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvents : MonoBehaviour
{
    public void SwitchScene(string targetScene)
    {   
        if (targetScene == null)
        {
            Debug.LogWarning("Scene switcher has no target scene");
            return;
        }

        SceneManager.LoadSceneAsync(targetScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
