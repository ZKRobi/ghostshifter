using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{

    private bool paused = false;
    private float oldTimeScale = 1;
    public GameObject PauseMenu;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void OnResumeClicked()
    {
        Resume();
    }

    public void OnExitClicked()
    {
        SceneManager.LoadScene("MenuScene");
    }

    void Pause()
    {
        paused = true;
        oldTimeScale = Time.timeScale;
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
    }

    void Resume()
    {
        if (paused)
        {
            paused = false;
            Time.timeScale = oldTimeScale;
            PauseMenu.SetActive(false);
        }
    }
}
