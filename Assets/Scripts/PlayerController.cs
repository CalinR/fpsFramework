using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	private int activeWeapon = -1;

	private int numOfWeaponsInInventory = 0;
	public Weapon[] weaponList;
	

	void Awake()
	{
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

		if (Input.GetMouseButton (0))
		{
			if(activeWeapon>=0){
				weapon = weaponList[activeWeapon];
				weapon.Shoot();
			}

		}
	}



	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Pickup"){
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
	

	Weapon ActiveWeaponByIndex(int index)
	{
		if (weaponList[index].HasWeapon) 
		{
			if(activeWeapon>=0){
				weaponList[activeWeapon].gameObject.SetActive(false);
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
