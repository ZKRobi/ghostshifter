using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Shapeshifter : MonoBehaviour
{

    private int form = 0;
    private PlatformerCharacter2D formControl;
    private Transform[] body = new Transform[3];
    private Transform faceCheck;
    private ParticleSystem m_Particles;

    // Use this for initialization
    private void Start()
    {
        formControl = gameObject.GetComponent<PlatformerCharacter2D>();
        //form2Control = GetComponent<PlatformerCharacterForm1Base>();
        body[0] = transform.Find("Frog");
        body[1] = transform.Find("Tatu");
        body[2] = transform.Find("Snake");
        m_Particles = GetComponentInChildren<ParticleSystem>();

    }

    public bool CanPass()
    {
        return form == 1;
    }

    // Update is called once per frame
    private void Update()
    {//Change form
        int newform = form; //If this becomes different from form, change form.
        if (CrossPlatformInputManager.GetButtonDown("Form1") && form != 0)
        {
            newform = 0;
            formControl.MaxSpeed = formControl.m_BaseSpeed;
            formControl.JumpForce = 850;
        }
        else if (CrossPlatformInputManager.GetButtonDown("Form2") && form != 1)
        {
            newform = 1;
            formControl.MaxSpeed = formControl.m_BaseSpeed * 2;
            formControl.JumpForce = 400;
        }
        else if (CrossPlatformInputManager.GetButtonDown("Form3") && form != 2)
        {
            newform = 2;
            formControl.MaxSpeed = formControl.m_BaseSpeed;
            formControl.JumpForce = 0;
        }
        if (newform == form)
        {
            return;
        }
        m_Particles.Play();
        body[form].gameObject.SetActive(false); //Deactivate old form
        foreach (Collider c in body[form].gameObject.GetComponents<Collider>())
        {
            c.enabled = false;
        }
        form = newform;
        body[form].gameObject.SetActive(true); //Activate new form
        foreach (Collider c in body[form].gameObject.GetComponents<Collider>())
        {
            c.enabled = true;
        }
    }
    public void CollisionEnabled(bool willCollide)
    {
        foreach (var c in body[form].GetComponents<Collider2D>())
        {
            c.enabled = willCollide;
        }
        body[form].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, willCollide ? 1f : 0.5f);
    }

    public int Form
    {
        get
        {
            return form;
        }
    }

    private void stopEmitter()
    {
        m_Particles.Stop();
        enabled = false;
    }
}