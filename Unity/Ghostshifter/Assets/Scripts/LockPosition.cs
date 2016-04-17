using UnityEngine;
using System.Collections;

public class LockPosition : MonoBehaviour
{

    public Transform target;
    public bool lockX = true;
    public bool lockY = false;
    public bool lockZ = false;

    public Vector3 offset;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var x = lockX ? target.position.x : transform.position.x;
        var y = lockY ? target.position.y : transform.position.y;
        var z = lockZ ? target.position.z : transform.position.z;

        transform.position = new Vector3(x + offset.x, y + offset.y, z + offset.z);
    }
}
