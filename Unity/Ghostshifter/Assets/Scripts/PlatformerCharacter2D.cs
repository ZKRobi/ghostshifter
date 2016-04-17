using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

    public class PlatformerCharacter2D : MonoBehaviour
    {
		[SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
		[SerializeField] private float m_BaseSpeed = 10f;					// Character is running this quickly alll the time
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character
        [SerializeField] private LayerMask m_WhatIsObstacle;
        private float m_BoostPower = 1f; //Current base speed multiplier
        [SerializeField] private float m_BoostWeight = 0.1f; //Rate of increase, in speed per second

        private int form = 0;
        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .3f; // Radius of the overlap circle to determine if grounded
        const float k_FaceplantRadius = 0.1f;
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        private Transform m_FaceplantCheck;
        const float k_CeilingRadius = .1f; // Radius of the overlap circle to determine if the player can stand up
        private Animator[] m_Anim = new Animator[3];            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private Shapeshifter m_Shapeshifter;
        //private bool m_FacingRight = true;  // For determining which way the player is currently facing.

        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_FaceplantCheck = transform.Find("FaceplantCheck");
            m_Anim[0] = transform.Find("Frogger").GetComponent<Animator>();
            m_Anim[1] = transform.Find("Robot").GetComponent<Animator>();
            m_Anim[2] = transform.Find("Robot").GetComponent<Animator>();
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
            colliders = Physics2D.OverlapCircleAll(m_FaceplantCheck.position, k_FaceplantRadius, m_WhatIsObstacle);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject && m_Rigidbody2D.gravityScale != 0) {
                    m_Rigidbody2D.gravityScale = 0;
                    Invoke("restoreCollision", 0.3f);
                    m_BoostPower *= 0.95f;
                    if (m_BoostPower < 0.5f)
                    {
                        m_BoostPower = 0.5f;
                    }
                    m_Shapeshifter.CollisionEnabled(false);
                }
            }
            /*if (m_BoostTimer > 10)
            {
                m_BoostPower = 1;
            }
            /*if (CrossPlatformInputManager.GetButtonDown(m_BoostTimer))
            {
                m_BoostPower = m_BoostWeight
            }*/
            // Set the vertical animation
            m_Anim[form].SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
        }

        private void restoreCollision()
        {
            m_Rigidbody2D.gravityScale = 3;
            m_Shapeshifter.CollisionEnabled(true);
        }

        public void Move(float move, bool crouch, bool jump)
        {
            if (m_Rigidbody2D.gravityScale == 0)
            {
                m_Rigidbody2D.velocity = new Vector2(7f*m_BoostPower, 0f);
                return;
            }
            // If crouching, check to see if the character can stand up
            /*if (!crouch && m_Anim.GetBool("Crouch"))
            {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                {
                    crouch = true; //We don't really need this
                }
            }

            // Set whether or not the character is crouching in the animator
            m_Anim.SetBool("Crouch", crouch);*/

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                move = (crouch ? move*m_CrouchSpeed : move);

                // The Speed animator parameter is set to the forward velocity of the player character.
				m_Anim[form].SetFloat("Speed", move*m_MaxSpeed + m_BaseSpeed);

                // Move the character
				m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed * m_BoostPower + m_BaseSpeed * m_BoostPower, m_Rigidbody2D.velocity.y);

                // If the input is moving the player right and the player is facing left...///Our character doesn't change facing -Adam
                /*if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                    // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }*/
            }
            // If the player should jump...
            if (m_Grounded && jump /*&& m_Anim[form].GetBool("Ground")*/)
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Anim[form].SetBool("Ground", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }
        }

		public float MaxSpeed {
			get {
				return m_MaxSpeed;
			}
			set {
				m_MaxSpeed = value;
			}
		}

        public float JumpForce {
            get {
                return m_JumpForce;
            }
            set {
                m_JumpForce = value;
            }
            }

        /*private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }*/
    }