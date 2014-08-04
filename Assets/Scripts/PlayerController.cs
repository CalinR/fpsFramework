using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	private int activeWeapon = -1;

	private int numOfWeaponsInInventory = 0;
	public Weapon[] weaponList;

	public AudioClip[] walking;
	private CharacterController controller;
	private float lastWalk = 0;
	private int lastWalkSound = 0;
	private Camera[] cameras;
	
	private int normal = 60;
	private float smooth = 5;
	private bool isZoomed = false;

	void Awake()
	{
		controller = GetComponent<CharacterController>();
		cameras = GetComponentsInChildren<Camera>();

		Weapon weapon;
		for(int i=0; i<weaponList.Length;i++)
		{
			weapon = weaponList[i];
			weapon.gameObject.SetActive(false);
			if(weapon.HasWeapon)
			{
				ActiveWeaponByIndex(i);
			}
		}

	}

	void OnGUI ( )
	{
		GUIStyle ammoGuiStyle = new GUIStyle();
		ammoGuiStyle.fontSize = 14;

		if (activeWeapon >= 0) {
			GUI.Label (new Rect (Screen.width - 60f, Screen.height - 30f, 30f, 20f), weaponList [activeWeapon].clipBullets.ToString (), ammoGuiStyle);
			GUI.Label (new Rect (Screen.width - 30f, Screen.height - 30f, 30f, 20f), weaponList [activeWeapon].ammo.ToString (), ammoGuiStyle);
		}
	}
	
	void Update()
	{
		Weapon weapon;
		int weaponIndex;
		//WEAPON HOTKEY
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			ActiveWeaponByIndex(0);
		}

		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			ActiveWeaponByIndex(1);
		}

		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			ActiveWeaponByIndex(2);
		}

		if (Input.GetKeyDown (KeyCode.Alpha4)) {
			ActiveWeaponByIndex(3);
		}

		//GOTO NEXT WEAPON IN WEAPON LIST
		if (Input.GetKeyDown (KeyCode.E)) 
		{
			if(numOfWeaponsInInventory>0)
			{	
				weaponIndex = activeWeapon;
				do 
				{
					weaponIndex++;
					if(weaponIndex >= weaponList.Length)
					{
						weaponIndex = 0;
					}

					weapon = ActiveWeaponByIndex(weaponIndex);

				}while(weapon==null);
			}
		}

		//GOTO PREVIOUS WEAPON IN WEAPON LIST
		if (Input.GetKeyDown (KeyCode.Q)) 
		{
			if(numOfWeaponsInInventory>0)
			{	
				weaponIndex = activeWeapon;
				do 
				{
					weaponIndex--;
					if(weaponIndex < 0)
					{
						weaponIndex = weaponList.Length-1;
					}
					
					weapon = ActiveWeaponByIndex(weaponIndex);
					
				}while(weapon==null);
			}
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			if(activeWeapon>=0){
				weapon = weaponList[activeWeapon];
				weapon.Reload();
			}
		}

		if (Input.GetMouseButton (0))
		{
			if(activeWeapon>=0){
				weapon = weaponList[activeWeapon];
				weapon.Shoot();
			}

		}


		//ZOOM WEAPON
		if (Input.GetKeyDown (KeyCode.Z)) {
			isZoomed = true;
		}
		if (Input.GetKeyUp (KeyCode.Z)) {
			isZoomed = false;
		}
	
		if (isZoomed) {
			if(activeWeapon>=0){
				weapon = weaponList[activeWeapon];
				int weaponZoom = normal - weapon.zoomLevel;
				weapon.Zoomed(true, cameras[0].transform);
				for(int i = 0; i<cameras.Length; i++){
					cameras[i].fieldOfView = Mathf.Lerp(cameras[i].fieldOfView,weaponZoom,Time.deltaTime*smooth);
				}
			}
		}
		else {
			if(activeWeapon>=0){
				weapon = weaponList[activeWeapon];
				weapon.Zoomed(false, cameras[0].transform);
			}
			for(int i = 0; i<cameras.Length; i++){
				cameras[i].fieldOfView = Mathf.Lerp(cameras[i].fieldOfView,normal,Time.deltaTime*smooth);
			}
		}


		//PLAY WALKING SOUNDS
		if(controller.isGrounded && controller.velocity.magnitude > 0.3){
			if (Time.time >= lastWalk+0.6){
				lastWalk=Time.time;
				AudioSource.PlayClipAtPoint(walking[lastWalkSound], new Vector3(transform.position.x, transform.position.y-1, transform.position.z));
				lastWalkSound++;
				if(lastWalkSound>walking.Length-1){
					lastWalkSound=0;
				}
			}
			BroadcastMessage("PlayerMoving", SendMessageOptions.DontRequireReceiver);
		}
		else {
			BroadcastMessage("PlayerIdle", SendMessageOptions.DontRequireReceiver);
		}
	}



	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Pickup"){
			other.gameObject.SendMessage("Pickup");
			string weaponId = other.gameObject.name;
			Weapon weapon;
			for(int i = 0; i<weaponList.Length; i++)
			{
				weapon = weaponList[i];
				if((weapon.id+"Pickup") == weaponId)
				{
					if(!weapon.HasWeapon)
					{
						numOfWeaponsInInventory++;
						weapon.HasWeapon = true;
						if(activeWeapon<0)
						{
							ActiveWeaponByIndex(i);
						}
					}
				}
			}
		}
	}

	void OnControllerColliderHit(ControllerColliderHit hit) {
		if(hit.gameObject.tag == "Ammo"){
			Ammo ammoScript = hit.gameObject.GetComponent<Ammo>();
			if(ammoScript.ammoEnabled){
				int collectedAmmo = ammoScript.ammunition;
				string pickedUpId = ammoScript.WeaponId;
				ammoScript.Pickup();
				updateAmmo(collectedAmmo,pickedUpId);
			}
		}
	}

	void updateAmmo(int collectedAmmo, string id){
		Weapon weapon;
		for(int i = 0; i<weaponList.Length; i++)
		{
			weapon = weaponList[i];
			if((weapon.id) == id)
			{
				weapon.ammo += collectedAmmo;
			}
		}
	}

	Weapon ActiveWeaponByIndex(int index)
	{
		if (weaponList[index].HasWeapon) 
		{
			if(activeWeapon>=0){
				if(activeWeapon!=index){
					weaponList[activeWeapon].gameObject.SetActive(false);
				}
			}
			activeWeapon = index;
			weaponList[index].gameObject.SetActive(true);

			return weaponList[index];
		} 
		else 
		{
			return null;
		}
	}


}
