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
    public void SwitchToGameplay(string gameplay_scene)
    {
        BackgroundMusic.Instance?.PlayFromStart();
        SwitchScene(gameplay_scene);
    }
    public void SwitchToMainMenu(string main_menu_scene)
    {
        BackgroundMusic.Instance?.Stop();
        SwitchScene(main_menu_scene);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
