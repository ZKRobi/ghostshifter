using UnityEngine;
using System.Collections;

public class Killer : MonoBehaviour {

    private LoseGame playerLoseScript;

	// Use this for initialization
	void Start () {
        GameObject.FindGameObjectWithTag("Player").GetComponent<LoseGame>();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerLoseScript.GetComponent<LoseGame>().SetGameLost(true);
        }
    }
}
