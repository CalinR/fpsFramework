using UnityEngine;
using System.Collections;

public class ExplosiveObject : MonoBehaviour {
	public float health = 100;
	public bool hasExploded = false;
	public Material explodedMaterial;
	public GameObject explosion;
	public AudioClip explosionSound;

	public void ApplyDamage(DamageData damageData)
	{
		health -= damageData.damageAmount;
		if (health <= 0) {
			if (!hasExploded) {
					Explode();
					hasExploded = true;
			}
			health = 0;
		}
	}

	private void Explode(){
		renderer.material = explodedMaterial;
		Instantiate(explosion, transform.position, transform.rotation);
		transform.rigidbody.AddExplosionForce(1, transform.position, 5, 0, ForceMode.Impulse);
		audio.PlayOneShot (explosionSound);
	}
}
