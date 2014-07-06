using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Weapon : MonoBehaviour {

	[SerializeField]
	private bool hasWeapon = false;
	public string id;
	public GameObject muzzleFlash;
	private RaycastHit hit;
	private Edelweiss.DecalSystem.Example.BulletDecals bulletDecals;
	public float fireRate = 1;
	private float lastShot = 0;
	private float muzzleTimeSince = 0;
	private float muzzleLastShot = 0;
	private float muzzleShowTime = 0.05f;
	public GameObject spentShell;
	private int maxShellCount = 10;
	List<GameObject> shells = new List<GameObject>();
	public float damage = 10;
	public AudioClip gunShot;

	void Awake(){
		bulletDecals = FindObjectOfType<Edelweiss.DecalSystem.Example.BulletDecals> ();
	}

	void Start(){
		muzzleFlash.SetActive (false);
	}

	void Update(){
		muzzleTimeSince = Time.time-muzzleLastShot;
		if (muzzleTimeSince >= muzzleShowTime) {
			muzzleFlash.SetActive (false);
		}
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
		while (shells.Count >= maxShellCount) {
			Destroy(shells[0]);
			shells.RemoveAt(0);
		}
		if (Time.time >= lastShot+fireRate)
		{
			AudioSource.PlayClipAtPoint(gunShot,transform.position);
			muzzleLastShot = Time.time;
			lastShot = Time.time;
			bulletDecals.CreateDecal (transform, damage);
			muzzleFlash.SetActive (true);
			GameObject newBulletShell = (GameObject)Instantiate(spentShell, transform.position, transform.rotation);
			newBulletShell.rigidbody.velocity = transform.TransformDirection(Vector3.left * 5);
			shells.Add(newBulletShell);
		}
	}
}
