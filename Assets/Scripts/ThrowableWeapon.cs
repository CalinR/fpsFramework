using UnityEngine;
using System.Collections;

public class ThrowableWeapon : Weapon {

	public GameObject throwableItem;
	public float throwSpeed = 10;
	public AudioClip throwing;

	void Start(){
		clipSize = ammo;
		clipBullets = clipSize;
		ammo = -1;
	}

	public override void Shoot() {
		if (Time.time >= lastShot + fireRate) {
			lastShot = Time.time;
			if (clipBullets > 0) {
				clipBullets--;
				GameObject item;
				item = Instantiate (throwableItem, transform.position + transform.forward*1, transform.rotation) as GameObject;
				item.rigidbody.velocity = transform.TransformDirection(Vector3.forward * throwSpeed);
				AudioSource.PlayClipAtPoint (throwing, transform.position);

			}
		}
	}

}
