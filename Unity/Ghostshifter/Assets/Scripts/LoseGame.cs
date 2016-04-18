using UnityEngine;


public class LoseGame : MonoBehaviour
{
    public GameObject loseUI;
    
    private bool gameLost = false;
    public void SetGameLost(bool really)
    {
        gameLost = gameLost || really;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameLost)
        {
            loseUI.SetActive(true);

            var animators = GetComponentsInChildren<Animator>();

            foreach (var animator in animators)
            {
                animator.SetBool("Dead", true);
            }
            GetComponent<Platformer2DUserControl>().enabled = false;
            GetComponent<PlatformerCharacter2D>().enabled = false;

            Destroy(GetComponent<Rigidbody2D>());
        }
    }
}
