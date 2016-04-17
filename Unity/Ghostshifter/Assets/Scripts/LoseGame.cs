using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoseGame : MonoBehaviour
{
    public GameObject loseUI;
    public float backToMenuAfterSeconds = 5;

    private float deathTime = 0;

    private bool gameLost = false;
    public void SetGameLost(bool really)
    {
        gameLost = gameLost || really;
        if (really)
        {
            Debug.Log("Game Lost set");
            deathTime = Time.time;
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameLost)
        {
            loseUI.SetActive(true);
            // TODO: turn off character scripts
            // TODO: fade screen out
            if (Time.time - backToMenuAfterSeconds > deathTime)
            {
                SceneManager.LoadScene("MenuScene");
            }
        }
    }
}
