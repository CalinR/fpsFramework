using UnityEngine;
using System.Collections;

public class ProjectileWeapon : BulletWeapon {

	public GameObject explosiveProjectile;

	void Awake(){
		if (idle) {
			Animation anim = gameObject.AddComponent<Animation> ();
			anim.AddClip (idle, idle.name);
			anim.AddClip (walking, walking.name);
			anim.AddClip (reloading, reloading.name);
			animation.Play (idle.name);
		}
	}

	void PlayerMoving(){
		if (idle && !isReloading) {
			animation.CrossFade (walking.name);
		}
	}
	
	void PlayerIdle(){
		if (idle && !isReloading) {
			animation.CrossFade (idle.name);
		}
	}

	public override void Shoot() {
		if (Time.time >= lastShot + fireRate && !isReloading) {
			lastShot = Time.time;
			if (clipBullets > 0) {
				clipBullets--;
				GameObject rocketShell;
				rocketShell = Instantiate (explosiveProjectile, transform.position, Quaternion.identity) as GameObject;
				Debug.Log (explosiveProjectile.transform.rotation);
				Quaternion q = Quaternion.FromToRotation (Vector3.up, transform.forward);
				rocketShell.transform.rotation = q * rocketShell.transform.rotation; 
				Rocket rocketscript = rocketShell.GetComponent<Rocket> ();
				rocketscript.LaunchProjectile (Vector3.forward, transform);
				AudioSource.PlayClipAtPoint (gunShot, transform.position);
			}
			else {
				AudioSource.PlayClipAtPoint (dryFire, transform.position);
			}
		}
	}
}
