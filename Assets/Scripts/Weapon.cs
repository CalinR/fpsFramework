using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	[SerializeField]
	private bool hasWeapon = false;
	public string id;

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
}
