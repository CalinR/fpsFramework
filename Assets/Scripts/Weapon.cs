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
	protected float lastShot = 0;
	private float muzzleTimeSince = 0;
	private float muzzleLastShot = 0;
	private float muzzleShowTime = 0.05f;
	public GameObject spentShell;
	public float damage = 10;
	public AudioClip gunShot;
	public AudioClip dryFire;
	public AudioClip reload;
	public AnimationClip idle;
	public AnimationClip walking;
	public AnimationClip reloading;
	public int clipSize = 6;
	public int ammo = 100;
	public int clipBullets;
	protected bool isReloading = false;
	private float reloadStartTime = 0f;



	void Awake(){
		bulletDecals = FindObjectOfType<Edelweiss.DecalSystem.Example.BulletDecals> ();
		if (idle) {
			Animation anim = gameObject.AddComponent<Animation> ();
			anim.AddClip (idle, idle.name);
			anim.AddClip (walking, walking.name);
			anim.AddClip (reloading, reloading.name);
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


		/*FIX THIS, THIS SHOULD BE HANDLED IN A FUNCTION THAT CONTROLS ALL ANIMATIONS FOR THIS WEAPON*/
		if (isReloading) {
			float reloadLength = animation[reloading.name].length;
			animation.CrossFade (reloading.name);
			if(Time.time >= reloadStartTime+reloadLength){
				isReloading = false;
			}
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

	public void Reload() {
		if (ammo > 0 && clipBullets != clipSize) {
			isReloading = true;
			reloadStartTime = Time.time;
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

	public virtual void Shoot() {
		if (Time.time >= lastShot + fireRate && !isReloading) {
			lastShot = Time.time;
			if (clipBullets > 0) {
				clipBullets--;
				AudioSource.PlayClipAtPoint (gunShot, transform.position);
				muzzleLastShot = Time.time;
				muzzleFlash.SetActive (true);
				bulletDecals.CreateDecal (transform, damage);
				GameObject newBulletShell = (GameObject)Instantiate (spentShell, transform.position, transform.rotation);
				newBulletShell.rigidbody.velocity = transform.TransformDirection (Vector3.left * 5);
				Destroy(newBulletShell.gameObject, 0.5f);
			} else {
				AudioSource.PlayClipAtPoint (dryFire, transform.position);
			}
		}
	}
}
