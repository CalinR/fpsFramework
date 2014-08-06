using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	[SerializeField]
	private bool hasWeapon = false;
	public string id;
	public float fireRate = 1;
	protected float lastShot = 0;
	public float zoomLevel = 0;
	protected bool isZoomed = false;
	protected Transform zoomFrom;
	public int clipSize = 6;
	public int ammo = 100;
	public int clipBullets;
	public Texture2D weaponIcon;

	void Start(){
		if (clipBullets == 0) {
			clipBullets = clipSize;
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

	public void Zoomed(bool zoomed, Transform zoomCamera){
		isZoomed = zoomed;
		zoomFrom = zoomCamera;
	}

	public virtual void Reload() {
		
	}

	public virtual void Shoot() {
		
	}
}
