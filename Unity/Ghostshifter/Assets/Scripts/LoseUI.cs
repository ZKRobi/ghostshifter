using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoseUI : MonoBehaviour
{
    public void OnRestartClick()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnQuitClick()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
