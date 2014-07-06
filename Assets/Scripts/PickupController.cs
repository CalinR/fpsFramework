using UnityEngine;
using System.Collections;

public class PickupController : MonoBehaviour {

	public AudioClip WeaponPickup;

	public void Pickup(){
		AudioSource.PlayClipAtPoint(WeaponPickup, transform.position);
	}

}
