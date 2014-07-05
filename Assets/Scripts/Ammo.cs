using UnityEngine;
using System.Collections;

public class Ammo : MonoBehaviour {

	public void ApplyDamage(DamageData damageData)
	{
		transform.rigidbody.AddExplosionForce(5, damageData.damagePosition.point, 5, 0, ForceMode.Impulse);
	}
}
