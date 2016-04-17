using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

	public class Shapeshifter : MonoBehaviour 
	{

		private int form = 0;
		private PlatformerCharacter2D formControl;
        private Animator m_Anim;            // Reference to the player's animator component. Totally not copy/pasted
        private Transform[] body = new Transform[3];
        private Transform m_faceplantCheck;

        // Use this for initialization
        private void Start () {
			formControl = gameObject.GetComponent<PlatformerCharacter2D>();
            //form2Control = GetComponent<PlatformerCharacterForm1Base>();
            m_Anim = GetComponent<Animator>();
            body[0] = transform.Find("Frogger");
            body[1] = transform.Find("Snake");
            body[2] = transform.Find("Ghost");
            m_faceplantCheck = transform.Find("FaceplantCheck");
        }

		// Update is called once per frame
		private void Update () {//Change form
            int newform = form; //If this becomes different from form, change form.
            if (CrossPlatformInputManager.GetButtonDown("Form1") && form != 0)
            {
                newform = 0;
                formControl.MaxSpeed = 4;
                formControl.JumpForce = 850;
                if (form == 1) { m_faceplantCheck.Translate(0f, 0.3f, 0f); }
            }
            else if (CrossPlatformInputManager.GetButtonDown("Form2") && form != 1)
            {
                newform = 1;
                formControl.MaxSpeed = 6;
                formControl.JumpForce = 400;
                if (form != 1) { m_faceplantCheck.Translate(0f, -0.3f, 0f); }
            }
            else if (CrossPlatformInputManager.GetButtonDown("Form3") && form != 2)
            {
                newform = 2;
                formControl.MaxSpeed = 8;
                if (form == 1) { m_faceplantCheck.Translate(0f, 0.3f, 0f); }
            }
            if (newform == form){
                return;
            }
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
            body[form].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, willCollide ? 1f:0.5f);
        }

        public int Form
        {
            get
            {
                return form;
            }
        }
	}