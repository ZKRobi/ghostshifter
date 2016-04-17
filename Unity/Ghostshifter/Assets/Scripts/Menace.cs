using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menace : MonoBehaviour
{

    public float speed;

    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
