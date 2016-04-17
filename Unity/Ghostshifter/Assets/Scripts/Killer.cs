using UnityEngine;
using System.Collections;

public class Killer : MonoBehaviour
{

    private LoseGame playerLoseScript;
    private int playerFormLayer;


    // Use this for initialization
    void Start()
    {
        playerFormLayer = LayerMask.NameToLayer("PlayerForms");
        playerLoseScript = GameObject.FindGameObjectWithTag("Player").GetComponent<LoseGame>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.layer);
        if (other.gameObject.layer == playerFormLayer)
        {
            playerLoseScript.GetComponent<LoseGame>().SetGameLost(true);
        }
    }
}
