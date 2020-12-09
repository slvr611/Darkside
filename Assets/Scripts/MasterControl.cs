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

    public Vector3[] camPositions;
    public int currentCamPosition;
    private Camera cam;

    public Vector3 currentCheckpoint;

    public Animator fadeAnim;

    private const string SAVE_DIV = "#SAVE_DATA#";

    public static MasterControl instance;

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

        SceneManager.sceneLoaded += OnSceneLoaded;
        cam.transform.position = camPositions[0];
        currentCamPosition = 0;

        fadeAnim = GameObject.FindGameObjectWithTag("Fade").GetComponent<Animator>();
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
        string contents = File.ReadAllText(Application.dataPath+"/SaveFiles/save1.txt");
        string[] load = contents.Split(new[] { SAVE_DIV }, System.StringSplitOptions.None);
        //0 - Level index, 1 - checkpoint position x, 2 - checkpoint position y, 
        //3 - checkpoint position z
        FindObjectOfType<PlayerScript>().setPosition(new Vector3(float.Parse(load[1]), float.Parse(load[2]), float.Parse(load[3])));
        
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

    public void NextScreen()
    {
        currentCamPosition += 1;
        cam.transform.position = camPositions[currentCamPosition];


    }

    public void setCheckpoint(Vector3 checkpoint)
    {
        currentCheckpoint = checkpoint;
        SaveCheckpoint();
    }

    public void ExitToMenu()
    {
        //Save progress
        SceneManager.LoadScene(0);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //OR load current level
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

    IEnumerator transitionToScene(int index)
    {
        //fade out
        fadeAnim.SetTrigger("FadeTrigger");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(index);
    }

    public void SaveCheckpoint()
    {
        print("saving...");
        //Save list - 
        //Scene index
        //checkpoint/player position
        //player stats (If malleable)

        string[] contents = {
            ""+SceneManager.GetActiveScene().buildIndex,
            ""+ currentCheckpoint.x,
            ""+ currentCheckpoint.y,
            ""+ currentCheckpoint.z
        };
        string save = string.Join(SAVE_DIV, contents);
        File.WriteAllText(Application.dataPath+"/SaveFiles/save1.txt", save);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Start();
        ReloadCheckpoint();
    }
}
