using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menace : MonoBehaviour
{
    public float speed;
    public float maxDistance;
    private GameObject player;

    //acceleration in unity unit / second
    public float acceleration;

    new private Rigidbody2D rigidbody;

    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector2(speed, 0);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float newSpeed = rigidbody.velocity.x + acceleration * Time.deltaTime;
        rigidbody.velocity = new Vector2(newSpeed, 0);

        if (player.transform.position.x > transform.position.x + maxDistance)
        {
            transform.position = new Vector3(player.transform.position.x - maxDistance, transform.position.y, transform.position.z);
        }
    }
}
