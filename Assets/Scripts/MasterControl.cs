using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterControl : MonoBehaviour
{
    public bool isPaused;
    public GameObject PauseMenu;
    public TMPro.TMP_Dropdown dd;

    
    private CamControl cam;

    public Vector3 currentCheckpoint;

    public Animator fadeAnim;

    private const string SAVE_DIV = "#SAVE_DATA#";

    public static MasterControl instance;
    public static SoundManager soundManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<CamControl>();

        isPaused = false;

        

        SceneManager.sceneLoaded += OnSceneLoaded;
        

        fadeAnim = GameObject.FindGameObjectWithTag("Fade").GetComponent<Animator>();
        soundManager = GetComponent<SoundManager>();
        PauseMenu = GameObject.FindGameObjectWithTag("MainCanvas").transform.GetChild(1).gameObject;

        PlayerScript player = FindObjectOfType<PlayerScript>();
        player.OnPlayerDeath += playerDeath;
    }

    // Update is called once per frame
    void Update()
    {
        //press r to reload scene
        if (Input.GetKeyDown("r"))
        {
            PlayerScript player = FindObjectOfType<PlayerScript>();
            player.OnPlayerDeath += playerDeath;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (Input.GetKeyDown("escape"))
        {
            if (PauseMenu.activeSelf)
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
        string contents = File.ReadAllText(Application.dataPath+"/SaveFiles/save1.txt");
        string[] load = contents.Split(new[] { SAVE_DIV }, System.StringSplitOptions.None);
        //0 - Level index, 1 - checkpoint position x, 2 - checkpoint position y, 
        //3 - checkpoint position z, 4 - just started level, 5 -cam position (index)
        FindObjectOfType<PlayerScript>().setPosition(new Vector3(float.Parse(load[1]), float.Parse(load[2]), float.Parse(load[3])));
        print(load[5]);
        cam.currentCamPosition = int.Parse(load[5]);
        cam.setCam();
    }

    public void ReloadLevel()
    {
        Resume();
        StartCoroutine(transitionToScene(SceneManager.GetActiveScene().buildIndex));
    }

    public void LoadNextLevel()
    {
        StartCoroutine(transitionToScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadLevel(int index)
    {
        StartCoroutine(transitionToScene(index));
    }

   

    public void setCheckpoint(Vector3 checkpoint)
    {
        currentCheckpoint = checkpoint;
        SaveCheckpoint();
    }

    public void ExitToMenu()
    {
        //Save progress
        SaveGame();
        Resume();
        print("Going back to main menu");
        StartCoroutine(transitionToScene(0));
    }

    public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //OR load current level
        string contents = File.ReadAllText(Application.dataPath + "/SaveFiles/save1.txt");
        string[] load = contents.Split(new[] { SAVE_DIV }, System.StringSplitOptions.None);
        //0 - Level index, 1 - checkpoint position x, 2 - checkpoint position y, 
        //3 - checkpoint position z, 4 - just started level, 5 -cam position (index)
        
        SceneManager.LoadScene(int.Parse(load[0]));
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

    public void NextScreen()
    {
        cam.NextScreen();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator transitionToScene(int index)
    {
        //fade out
        print("woo");
        fadeAnim.SetTrigger("FadeTrigger");
        yield return new WaitForSeconds(1.5f);
        print("wootube");
        SceneManager.LoadScene(index);
    }

    IEnumerator playerDeathTimer()
    {
        print("waiting....");
        yield return new WaitForSeconds(2);
        StartCoroutine(transitionToScene(SceneManager.GetActiveScene().buildIndex));
    }

    public void SaveCheckpoint()
    {
        print("saving...");
        //Save list - 
        //Scene index
        //checkpoint/player position
        //just started level?
        //player stats (If malleable)

        string[] contents = {
            ""+SceneManager.GetActiveScene().buildIndex,
            ""+ currentCheckpoint.x,
            ""+ currentCheckpoint.y,
            ""+ currentCheckpoint.z,
            "false",
            ""+cam.currentCamPosition
        };
        string save = string.Join(SAVE_DIV, contents);
        File.WriteAllText(Application.dataPath+"/SaveFiles/save1.txt", save);
    }

    public void SaveGame()
    {
        print("saving...");
        //Save list - 
        //Scene index
        //checkpoint/player position
        //just started level?
        //player stats (If malleable)

        string[] contents = {
            ""+SceneManager.GetActiveScene().buildIndex,
            ""+ currentCheckpoint.x,
            ""+ currentCheckpoint.y,
            ""+ currentCheckpoint.z,
            "true",
            ""+cam.currentCamPosition
        };
        string save = string.Join(SAVE_DIV, contents);
        File.WriteAllText(Application.dataPath + "/SaveFiles/save1.txt", save);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string contents = File.ReadAllText(Application.dataPath + "/SaveFiles/save1.txt");
        string[] load = contents.Split(new[] { SAVE_DIV }, System.StringSplitOptions.None);

        Start();
        soundManager.refresh();

        if (load[4] == "false")
        {
            ReloadCheckpoint();
        }
        else
        {
            print("was not false");
        }
        
    }

    public void playerDeath()
    {
        print("here?");
        StartCoroutine(playerDeathTimer());
    }
}
