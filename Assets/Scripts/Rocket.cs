using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour {

	public float damage = 100;
	public float speed = 10;
	public float timeout = 5;
	public GameObject explosion;
	private float launchTime = 0;
	private bool exploding = false;

	void Start()
	{
		launchTime = Time.time;
	}

	void Update()
	{
		if (Time.time > launchTime + timeout) {
			if (!exploding) {
				exploding = true;
				Explode();
			}
		}
	}

	public void LaunchProjectile(Vector3 forward, Transform launcher)
	{
		rigidbody.velocity = launcher.TransformDirection(forward * speed);
	}

	void OnCollisionEnter(Collision collision)
	{
		if (!exploding) {
				exploding = true;
				DamageData damageData = new DamageData ();
				damageData.damageAmount = damage;
				damageData.hitPositiion = transform.position;
				collision.gameObject.SendMessage ("ApplyDamage", damageData, SendMessageOptions.DontRequireReceiver);
				Explode ();
		}
	}

	public void Explode(){
		Instantiate(explosion, transform.position, transform.rotation);
		gameObject.renderer.enabled = false;
		rigidbody.velocity = new Vector3(0,0,0);
		Transform smoke = gameObject.transform.Find ("Smoke Trail");
		smoke.particleEmitter.emit = false;
		Destroy (gameObject, 2);
	}

}
