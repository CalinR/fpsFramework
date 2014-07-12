﻿using UnityEngine;
using System.Collections;

public class Ammo : MonoBehaviour {

	public int ammunition = 50;
	public string WeaponId;
	public float ammoRefreshRate = 5.0f;
	public bool ammoEnabled = true;
	private float ammoPickedUpTime = 0f;
	public AudioClip regeneration;

	public void Pickup(){
		ammoPickedUpTime = Time.time;
		ammoEnabled = false;
	}

	void Update(){
		if (!ammoEnabled) {
			if(Time.time >= ammoPickedUpTime+ammoRefreshRate){
				renderer.enabled = true;
				gameObject.collider.enabled = true;
				ammoEnabled = true;
				gameObject.rigidbody.useGravity = true;
				AudioSource.PlayClipAtPoint(regeneration,transform.position);
			}
			else {
				renderer.enabled = false;
				gameObject.collider.enabled = false;
				gameObject.rigidbody.useGravity = false;
			}
		}
	}

	public void ApplyDamage(DamageData damageData)
	{
		transform.rigidbody.AddExplosionForce (5, damageData.damagePosition.point, 5, 0, ForceMode.Impulse);
	}

}
