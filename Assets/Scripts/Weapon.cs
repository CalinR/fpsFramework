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
	public AudioClip dryFire;
	public AudioClip reload;
	public AnimationClip idle;
	public AnimationClip walking;
	private int playerMovement = 0;
	public int clipSize = 6;
	public int ammo = 100;
	public int clipBullets;



	void Awake(){
		bulletDecals = FindObjectOfType<Edelweiss.DecalSystem.Example.BulletDecals> ();
		if (idle) {
			Animation anim = gameObject.AddComponent<Animation> ();
			anim.AddClip (idle, idle.name);
			anim.AddClip (walking, walking.name);
			animation.Play (idle.name);
		}
	}

	void Start(){
		if (clipBullets == 0) {
			clipBullets = clipSize;
		}
		muzzleFlash.SetActive (false);
	}
	

	void Update(){
		muzzleTimeSince = Time.time-muzzleLastShot;
		if (muzzleTimeSince >= muzzleShowTime) {
			muzzleFlash.SetActive (false);
		}
	}

	void PlayerMoving(){
		if (idle) {
			animation.CrossFade (walking.name);
		}
	}

	void PlayerIdle(){
		if (idle) {
			animation.CrossFade (idle.name);
		}
	}

	public void Reload() {
		if (ammo > 0) {
			AudioSource.PlayClipAtPoint(reload,transform.position);
			int bulletsMissing = clipSize-clipBullets;
			if(ammo<bulletsMissing){
				bulletsMissing=ammo;
			}
			ammo-=bulletsMissing;
			clipBullets+=bulletsMissing;
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
			lastShot = Time.time;
			if(clipBullets>0){
				clipBullets--;
				AudioSource.PlayClipAtPoint(gunShot,transform.position);
				muzzleLastShot = Time.time;
				bulletDecals.CreateDecal (transform, damage);
				muzzleFlash.SetActive (true);
				GameObject newBulletShell = (GameObject)Instantiate(spentShell, transform.position, transform.rotation);
				newBulletShell.rigidbody.velocity = transform.TransformDirection(Vector3.left * 5);
				shells.Add(newBulletShell);
			}
			else {
				AudioSource.PlayClipAtPoint(dryFire,transform.position);
			}
		}
	}
}
