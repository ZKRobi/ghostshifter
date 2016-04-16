using UnityEngine;
using System.Collections;

public class LevelItem : MonoBehaviour
{
    public float width;
    private GameObject referenceObject;

    public void Start()
    {
        referenceObject = GameObject.FindWithTag("Player");
    }

    public void Update()
    {
        if (transform.position.x < referenceObject.transform.position.x - width * 2)
        {
            Destroy(this.gameObject);
        }
    }
}
