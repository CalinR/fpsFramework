using UnityEngine;
using System.Collections;

public class Knife : MonoBehaviour {

	private bool hasStruck = false;
	public AudioClip collisionSound;

	void OnCollisionEnter(Collision collision)
	{
		if (!hasStruck) {
			hasStruck = true;
			AudioSource.PlayClipAtPoint (collisionSound, transform.position);
		}
		rigidbody.velocity = new Vector3 (0,0,0);
	}

	void Update(){
		if (!hasStruck) {
				transform.Rotate (90f * (Time.deltaTime * 6), 0, 0);
		}
	}
}
