using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
	public class Shapeshifter : MonoBehaviour 
	{

		private int form = 0;
		private PlatformerCharacter2D formControl;
        private Animator m_Anim;            // Reference to the player's animator component. Totally not copy/pasted
        private Transform body1;

        // Use this for initialization
        private void Start () {
			formControl = gameObject.GetComponent<PlatformerCharacter2D>();
            //form2Control = GetComponent<PlatformerCharacterForm1Base>();
            m_Anim = GetComponent<Animator>();
            body1 = transform.Find("Ghost");
        }

		// Update is called once per frame
		private void Update () { ///I'm apparently too lazy to write good code.
			if (CrossPlatformInputManager.GetButtonDown("Form1") && form != 0) {
				form = 0;
				formControl.MaxSpeed = 6;
                body1.gameObject.SetActive(false);
			} else if (CrossPlatformInputManager.GetButtonDown("Form2") && form != 1){
				form = 1;
				formControl.MaxSpeed = 6;
                body1.gameObject.SetActive(true);
            } else if (CrossPlatformInputManager.GetButtonDown("Form3") && form != 2){
				form = 2;
				formControl.MaxSpeed = 6;
			}
		}
	}
}
