using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public Transform player;
    private int score = 0;
    private Text scoreArea;

    // Use this for initialization
    void Start()
    {
        scoreArea = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        int newScore = Mathf.FloorToInt(player.transform.position.x);
        if (newScore > score)
        {
            score = newScore;
            scoreArea.text = score.ToString();
        }
    }
}
