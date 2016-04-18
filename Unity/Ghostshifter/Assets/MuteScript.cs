using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class MuteScript : MonoBehaviour
{
    public GameObject onText;
    public GameObject offText;

    private bool muted = false;


    public void ToggleMuted()
    {
        if (muted)
        {
            muted = false;
            onText.SetActive(true);
            offText.SetActive(false);
            AudioListener.volume = 1;
        }
        else
        {
            muted = true;
            onText.SetActive(false);
            offText.SetActive(true);
            AudioListener.volume = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMuted();
        }
    }
}
