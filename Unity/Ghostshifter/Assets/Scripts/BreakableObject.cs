using UnityEngine;
using System.Collections;

public class BreakableObject : MonoBehaviour
{
    public Shapeshifter Player;

    private Rigidbody2D rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (Player.Form == 1)
        {
            rigidbody.isKinematic = false;
        }
        else
        {
            rigidbody.isKinematic = true;
        }
    }
}
