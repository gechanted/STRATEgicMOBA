using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMovementblue : MonoBehaviour {

	public Rigidbody rigidbody;
	public EnemyList DadArray;
	public GameObject gegner;

	public int health = 20;
	int attack = 2;
	int def = 0;
	float movespeed = 0.1f;
	float waittime = 3f;
	float wait = 1f;

	public GameObject walkingpoint;
	bool walkingpointreached = false;

	bool dead = false;
	float totenwache = 1f;

	void Start () {
		rigidbody = GetComponent<Rigidbody> ();
		GameObject liste = GameObject.FindWithTag ("gegnerliste");
		DadArray = liste.GetComponent<EnemyList> ();
		//DadArray = GetComponent<EnemyList>;
		DadArray.intothearrayblue (gameObject);
		setup ();
	}
	void setup (){
		health = DadArray.minionbluehealth;
		attack = DadArray.minionblueattack;
		def = DadArray.minionbluedef;
		waittime = DadArray.minionbluewait;
	}

	void Update () {
		rigidbody.angularVelocity = Vector3.zero;
		rigidbody.velocity = Vector3.zero;
		if (dead){
			totenwache -= Time.deltaTime;
			if (totenwache < 0) {
				Destroy (gameObject);
			}
		}
		GameObject tMin = null;
		float minDist = Mathf.Infinity;
		Vector3 currentPos = transform.position;
		foreach (GameObject t in DadArray.red)
		{
			
			float dist = Vector3.Distance(t.transform.position, currentPos);
			//Debug.Log ("Distanz: " + dist + ", currentPos: " + currentPos + ", Tmin: " + tMin + " t.position " + t.transform.position);
			if (dist < minDist)
			{
				tMin = t;
				minDist = dist;
				
			}
		}

			gegner = tMin;
		if (minDist < 20) {
			Attack (minDist);
		}
		else {
			Idle ();
		}
	}




	void Idle (){
		if (walkingpointreached != true) {
			transform.LookAt(walkingpoint.transform);
			if (Vector3.Distance (walkingpoint.transform.position, gameObject.transform.position) < 1f) {
				walkingpointreached = true;
			}
		}
		else {
			transform.LookAt (DadArray.redcore.transform);
		}
		move ();
		//transform.Translate ("new Vector3 (0, 0, 1)" * Time.deltaTime * moveSpeed/2.5);
		//move forward in direction
	}





	void Attack(float minDist){
		//Look at target and follow it
		transform.LookAt(gegner.transform);
		if (wait > 0) {
			wait -= Time.deltaTime;
		} else {

			if (minDist < 3) {
				bool turm = false;
				bool spieler = false;
				//MinionMovementred enemyscript = enemy.GetComponent("MinionMovementred") as MinionMovementred;
				//MinionMovementblue enemyscript = GameObject.Find("enemy").GetComponent<MinionMovementblue>();
				foreach (GameObject t in DadArray.redplayer) {
					if (t == gegner) {
						spieler = true;
					}
				}
				foreach (GameObject t in DadArray.redtower) {
					if (t == gegner) {
						turm = true;
					}
				}
				if (gegner == DadArray.redcore) {
					CoreScriptred enemyscript;
					enemyscript = gegner.GetComponent<CoreScriptred> ();
					enemyscript.Damage (attack);
				} else if (spieler == true){
					MovementCharacter enemyscript = gegner.GetComponent<MovementCharacter> ();
					enemyscript.Damage (attack);
				}else{
					if (turm == true) {
						Towershootsred enemyscript; 
						enemyscript = gegner.GetComponent<Towershootsred> ();
						enemyscript.Damage (attack);
					} else {
						MinionMovementred enemyscript; 
						enemyscript = gegner.GetComponent<MinionMovementred> ();
						bool unvẃichtig = enemyscript.Damage (attack);
					}
				}
				//Hit -> wait
				wait = waittime;
				//play animation


			} else {
				move ();
			}
		}
	}

	void move(){
		//Debug.Log ("blue move wird ausgeführt");
		//float y = transform.eulerAngles.y * Mathf.Deg2Rad;
		//float z = Mathf.Sin (y * Mathf.PI / 180) * Time.deltaTime;
		//float x = Mathf.Sin (y * Mathf.PI / 180 + 45 / 2 * Mathf.PI) * Time.deltaTime;
		//Vector3 direction = new Vector3 (x, 0, z);
		//transform.position = Vector3.Lerp (transform.position, direction, 10f);
		//		Debug.Log("blue" + direction);
		transform.Translate(0,0,movespeed);
	}





	public bool Damage(int damage){
		damage = damage - def;
		if (damage > 0) {
			health = health - damage;
		}
		if(health <= 0){
			DadArray.outofthearrayblue(gameObject);
			dead = true;
			return true;
		}
		return false;
	}


	






}