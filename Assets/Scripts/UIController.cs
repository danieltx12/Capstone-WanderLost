using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    GameObject[] pauseObjects;
    GameObject[] quitMenu;
    void Start()
    {
        Time.timeScale = 1;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        quitMenu = GameObject.FindGameObjectsWithTag("ShowQuit");
        hidePaused();
        hideQuit();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale == 1)
            {
                Time.timeScale = 0;
                showPaused();
            }
            else if(Time.timeScale == 0)
            {
                Time.timeScale = 1;
                hidePaused();
            }
        }
    }

    public void showPaused()
    {
        foreach(GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    public void hidePaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }

    public void pauseControl()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            showPaused();
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            hidePaused();
        }
    }

    public void QuitMenu()
    {
        hidePaused();
        foreach (GameObject g in quitMenu)
        {
            g.SetActive(true);
        }
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("APPLICATION QUIT");
    }

    public void NoQuit()
    {
        showPaused();
        hideQuit();
    }

    public void hideQuit()
    {
        foreach (GameObject g in quitMenu)
        {
            g.SetActive(false);
        }
    }
}
