using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour
{

    public GameObject[] levelItems;

    public float levelItemWidth;

    public Transform player;

    private float lastGenerated = 0;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.x - lastGenerated > levelItemWidth)
        {
            var nextItem = Mathf.FloorToInt(levelItems.Length * Random.value);
            if (nextItem >= levelItems.Length)
            {
                nextItem = levelItems.Length - 1;
            }

            lastGenerated += levelItemWidth;

            GameObject newItem = Instantiate(levelItems[nextItem]);
            newItem.transform.position = new Vector3(lastGenerated + (levelItemWidth * 2), 0, 0);
        }
    }
}
