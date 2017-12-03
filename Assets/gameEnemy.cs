using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameEnemy : MonoBehaviour {

	private float Distance;

	public float distancePoursuite = 10;

	public float porteeAttaque = 2.2f;

	public float attaqueRepetee = 1;

	private float tempsAttaque;

	public Transform cible;

	public float degats;

	private UnityEngine.AI.NavMeshAgent agent;

	private Animation animations;

	public float santeEnemi;
	private bool isDead = false;

	// Use this for initialization
	void Start () {
		agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent> ();
		animations = gameObject.GetComponent<Animation> ();
		tempsAttaque = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isDead) {
			cible = GameObject.Find ("Player").transform;

			Distance = Vector3.Distance (cible.position, transform.position);

			if (Distance > distancePoursuite) {

				surPlace ();
			}

			if (Distance < distancePoursuite && Distance > porteeAttaque) {

				poursuivre ();
			}

			if (Distance < porteeAttaque) {

				attaquer ();
			}
		}
	}

	void poursuivre()
	{
		animations.Play ("walk");
		agent.destination = cible.position;
	}

	void attaquer()
	{
		agent.destination = transform.position;
		if (Time.time > tempsAttaque) {

			animations.Play("hit");
			cible.GetComponent<PlayerInventory> ().ApplyDamage (degats);
			Debug.Log (" L'ennemi a envoyé" + degats + "points de dégats");
			tempsAttaque = Time.time + attaqueRepetee;
		}
		
	}

	void surPlace()
	{
		animations.Play ("idle");
	}

	public void ApplyDamage(float LesDegats)
	{
		if (!isDead) 
		{
			santeEnemi = santeEnemi - LesDegats;
			print (gameObject.name + "a subit" + LesDegats + "points de degats.");

			if (santeEnemi <= 0)
			{
				Dead ();
			}
		}
	}

	public void Dead()
	{
		isDead = true;
		animations.Play("die");
		Destroy (transform.gameObject, 5);
	}
		
}
