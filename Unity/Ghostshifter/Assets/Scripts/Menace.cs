﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menace : MonoBehaviour
{
    public float speed;
    public float maxDistance;
    public GameObject player;

    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > transform.position.x + maxDistance)
        {
            transform.position = new Vector3(player.transform.position.x - maxDistance, transform.position.y, transform.position.z);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.GetComponent<LoseGame>().SetGameLost(true);
        }
    }
}
