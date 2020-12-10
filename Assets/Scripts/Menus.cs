using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menus : MonoBehaviour
{
    public GameObject PauseMenu;
    public bool isPaused;
    public MasterControl master;

    private void Start()
    {
        master = FindObjectOfType<MasterControl>();
        PauseMenu = transform.GetChild(1).gameObject;
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

    public void ExitToMenu()
    {
        //Save progress
        master.SaveGame();
        Resume();
        print("Going back to main menu");
        master.LoadLevel(0);
    }

    public void ReloadCheckpoint()
    {
        Resume();
        master.ReloadLevel();
    }

    public void LoadGame()
    {
        master.PlayGame();
    }

    public void ExitGame()
    {
        master.ExitGame();
    }
}
