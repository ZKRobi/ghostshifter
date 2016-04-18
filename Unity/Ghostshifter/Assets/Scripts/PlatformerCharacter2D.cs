using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlatformerCharacter2D : MonoBehaviour
{

    public float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
    public float m_JumpForce = 850f;                  // Amount of force added when the player jumps.

    public float m_BaseSpeed = 10f;                    // Character is running this quickly alll the time

    public LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

    public LayerMask m_WhatIsObstacle;
    private float m_BoostPower = 1f; //Current base speed multiplier

    public float m_BoostWeight = 0.1f; //Rate of increase, in speed per second

    private int form = 0;
    private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
    const float k_GroundedRadius = 0.3f; // Radius of the overlap circle to determine if grounded
    const float k_FaceplantRadius = 0.05f;
    private bool m_Grounded;            // Whether or not the player is grounded.

    const float k_CeilingRadius = 0.1f; // Radius of the overlap circle to determine if the player can stand up
    private Animator[] m_Anim = new Animator[3];            // Reference to the player's animator component.
    private Rigidbody2D m_Rigidbody2D;
    private Shapeshifter m_Shapeshifter;

    public float immaterializedTimeAfterCollision = 1f;
    //private bool m_FacingRight = true;  // For determining which way the player is currently facing.

    private float oldGravityScale = 0;

    private void Start()
    {
        // Setting up references.
        m_GroundCheck = transform.Find("GroundCheck");
        m_Anim[0] = transform.Find("Frog").GetComponent<Animator>();
        m_Anim[1] = transform.Find("Tatu").GetComponent<Animator>();
        m_Anim[2] = transform.Find("Snake").GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Shapeshifter = GetComponent<Shapeshifter>();
    }


    private void FixedUpdate()
    {
        form = transform.GetComponent<Shapeshifter>().Form;
        m_Grounded = false;
        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                m_Grounded = true;
        }
        m_Anim[form].SetBool("Ground", m_Grounded);
        m_BoostPower += (Time.deltaTime * m_BoostWeight);
        ////++++

        // Set the vertical animation
        m_Anim[form].SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
    }

    private void restoreCollision()
    {
        m_Rigidbody2D.gravityScale = oldGravityScale;
        m_Shapeshifter.CollisionEnabled(true);
    }

    public void Move(float move, bool crouch, bool jump)
    {
        if (m_Rigidbody2D.gravityScale == 0)
        {
            m_Rigidbody2D.velocity = new Vector2(m_BaseSpeed * m_BoostPower, 0f);
            return;
        }

        // Move the character
        m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed * m_BoostPower + m_BaseSpeed * m_BoostPower, m_Rigidbody2D.velocity.y);

        // If the player should jump...
        if (m_Grounded && jump)
        {
            // Add a vertical force to the player.
            m_Grounded = false;
            m_Anim[form].SetBool("Ground", false);
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
    }

    public float MaxSpeed
    {
        get
        {
            return m_MaxSpeed;
        }
        set
        {
            m_MaxSpeed = value;
        }
    }

    public float JumpForce
    {
        get
        {
            return m_JumpForce;
        }
        set
        {
            m_JumpForce = value;
        }
    }
}