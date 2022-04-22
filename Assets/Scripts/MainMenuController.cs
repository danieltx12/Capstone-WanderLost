using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
        Debug.Log("APPLICATION QUIT");
    }
    
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
}
