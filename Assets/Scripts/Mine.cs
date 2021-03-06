﻿using UnityEngine;
using System.Collections;

public class Mine : MonoBehaviour {
	
	private bool hasStuck = false;
	public AudioClip collisionSound;

	void OnCollisionEnter(Collision collision)
	{
		if (!hasStuck) {
			rigidbody.velocity = new Vector3 (0, 0, 0);
			rigidbody.isKinematic = true;
			rigidbody.useGravity = false;

			AudioSource.PlayClipAtPoint (collisionSound, transform.position);

			transform.rotation = Quaternion.FromToRotation (Vector3.up, collision.contacts [0].normal);
			transform.parent = collision.transform;
			hasStuck = true;
		}
	}
	
}
