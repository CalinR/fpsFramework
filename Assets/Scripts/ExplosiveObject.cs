using UnityEngine;
using System.Collections;

public class ExplosiveObject : MonoBehaviour {
	public float health = 100;
	public bool hasExploded = false;
	public Material explodedMaterial;

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

		Debug.Log (health);
	}

	private void Explode(){
		renderer.material = explodedMaterial;
		transform.rigidbody.AddExplosionForce(1, transform.position, 5, 0, ForceMode.Impulse);
	}
}
