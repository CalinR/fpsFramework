using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	[SerializeField]
	private bool hasWeapon = false;
	public string id;
	private RaycastHit hit;
	private Edelweiss.DecalSystem.Example.BulletDecals bulletDecals;
	public float fireRate = 1;
	private float lastShot = 0;

	void Awake(){
		bulletDecals = FindObjectOfType<Edelweiss.DecalSystem.Example.BulletDecals> ();
	}

	public bool HasWeapon {
		get 
		{
			return hasWeapon;
		}
		set
		{ 
			hasWeapon = value; 
		}
	}

	public void Shoot() {
		if (Time.time >= lastShot+fireRate)
		{
			lastShot = Time.time;
			bulletDecals.CreateDecal (transform);
		}
	}
}
