using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterControl : MonoBehaviour
{
    public bool isPaused;
    public GameObject PauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        //press r to reload scene
        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (Input.GetKeyDown("escape"))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Time.timeScale = 1;
        isPaused = false;
        PauseMenu.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        isPaused = true;
        PauseMenu.SetActive(true);
    }
}
