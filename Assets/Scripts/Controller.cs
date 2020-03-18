using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Controller : MonoBehaviour
{
    private bool pauseToggle;
    public GameObject MainMenu;
    // Start is called before the first frame update
    void Start()
    {
        pauseToggle = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseToggle)
            {
                MainMenu.GetComponent<Canvas>().enabled = (false);
                Time.timeScale = 1;
            }
            else
            {
                MainMenu.GetComponent<Canvas>().enabled =(true);
                Time.timeScale = 0;
            }


            pauseToggle = !pauseToggle;
        }
    }

    public void ResetGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("terrain_test");
    }
}
