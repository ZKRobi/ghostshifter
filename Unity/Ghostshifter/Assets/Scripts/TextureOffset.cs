using UnityEngine;
using System.Collections;

public class TextureOffset : MonoBehaviour {

	public float movespeed = 0.01f;
	void Update () {
		Vector3 pos = transform.position;
		Material mat = GetComponent<Renderer>().material;
		mat.SetTextureOffset("_MainTex", new Vector2(pos.x*movespeed,0));
	}
}
