using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public void OnStartClick()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnExitClick()
    {
        Application.Quit();
    }
}
