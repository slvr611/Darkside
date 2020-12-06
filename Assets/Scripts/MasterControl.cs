using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterControl : MonoBehaviour
{
    public bool isPaused;
    public GameObject PauseMenu;
    public TMPro.TMP_Dropdown dd;

    public Vector3[] camPositions;
    public int currentCamPosition;
    private Camera cam;

    public Vector3 currentCheckpoint;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        isPaused = false;

        for (int i = 0; i<camPositions.Length; i++)
        {
            camPositions[i].z = cam.transform.position.z;
        }

        if (camPositions.Length <= 0)
        {
            camPositions = new Vector3[1] { cam.transform.position };
        }

        cam.transform.position = camPositions[0];
        currentCamPosition = 0;
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

    public void ReloadCheckpoint()
    {

    }

    public void ReloadLevel()
    {

    }

    public void loadNextLevel()
    {

    }

    public void setCheckpoint(Vector3 checkpoint)
    {
        currentCheckpoint = checkpoint;
    }

    public void ExitToMenu()
    {
        //Save progress
        SceneManager.LoadScene(0);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void setScreenSize()
    {
        int ddValue = dd.value;

        if (ddValue == 0)
        {
            //Fullscreen
        }
        else if (ddValue == 1)
        {
            //large screen
        }
        else if (ddValue == 2)
        {
            //medium screen
        }
        else if (ddValue == 3)
        {
            //small screen
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
