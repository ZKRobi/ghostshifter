using UnityEngine;
using System.Collections;

public class PassableObject : MonoBehaviour
{

    private Shapeshifter player;
    private Collider2D[] colliders;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Shapeshifter>();
        colliders = GetComponents<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (player.CanPass())
        {
            foreach (var collider in colliders)
            {
                collider.enabled = false;
            }
        }
    }
}
