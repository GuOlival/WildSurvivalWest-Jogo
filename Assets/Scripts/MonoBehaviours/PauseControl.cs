using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseControl : MonoBehaviour
{
    public static bool jogoPausado = false;
    public GameObject pauseMenuUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        jogoPausado = false;
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        jogoPausado = true;;    
    }

    public void Menu()
    {
        jogoPausado = false;
        SceneManager.LoadScene("Start_Scene");
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(jogoPausado)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    

}
