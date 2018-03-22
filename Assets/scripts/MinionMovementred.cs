using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMovementred : MonoBehaviour {

	public Rigidbody rigidbody;
	public GameObject gegner;
	public EnemyList DadArray;

	int health = 20;
	int attack = 2;
	int def = 0;
	float movespeed = 0.1f;
	float waittime = 3f;
	float wait = 1f;

	public GameObject walkingpoint;
	bool walkingpointreached = false;

	bool dead = false;
	float totenwache = 1f;

	//void Awake(){
	//	DadArray = GetComponent<EnemyList> ();
	//GameObject findmyscript = GameObject.Find("Quad");
		//DadArray = findmyscript.FindObjectOfType(typeof(EnemyList)) as EnemyList;
	//}


	void Start () {
		Debug.Log (walkingpoint.name);
		GameObject liste = GameObject.FindWithTag ("gegnerliste");
		DadArray = liste.GetComponent<EnemyList> ();
		//minionRigidbody = GetComponent<Rigidbody> ();
		DadArray.intothearrayred (gameObject);	
		setup ();
		rigidbody = GetComponent<Rigidbody> ();

	}

	void setup (){
		health = DadArray.minionredhealth;
		attack = DadArray.minionredattack;
		def = DadArray.minionreddef;
		waittime = DadArray.minionredwait;
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
		//int pos = -1;
		//int bestpos;
		GameObject tMin = null; 
		float minDist = Mathf.Infinity; 
		Vector3 currentPos = transform.position;

		foreach(GameObject t in DadArray.blue)
		{
			//pos++;
			float dist = Vector3.Distance(t.transform.position, currentPos);
			if (dist < minDist)
			{
				//bestpos = pos;
				tMin = t;
				minDist = dist;
			}
		}
		gegner = tMin; 
		// MinionMovementblue script = DadArray.bluescript[bestpos];

		if (minDist < 20) {
			Attack (minDist); //, script
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
			transform.LookAt (DadArray.bluecore.transform);
		}

		move ();
		//transform.Translate ("new Vector3 (0, 0, 1)" * Time.deltaTime * moveSpeed/2.5);
		//move forward in direction
	}





	void Attack(float minDist){ //MinionMovementblue enemyscript
		//Debug.Log ("attack wird ausgeführt");
			//Look at target and follow it
		transform.LookAt(gegner.transform);
		//Debug.Log("atttack wure aufgerufen");
		//Vector3 minionToTarget = enemy.transform.position - transform.position;
			//minionToTarget.y = 0f;
			//Quaternion newRotation = Quaternion.LookRotation (minionToTarget);
			//minionRigidbody.MoveRotation (newRotation);


		if (wait > 0) {
			wait -= Time.deltaTime;
		} else {

			if (minDist < 3) {
				bool turm = false;
				bool spieler = false;
				//MinionMovementred enemyscript = enemy.GetComponent("MinionMovementred") as MinionMovementred;
				//MinionMovementblue enemyscript = GameObject.Find("enemy").GetComponent<MinionMovementblue>();
				foreach (GameObject t in DadArray.blueplayer) {
					if (t == gegner) {
						spieler = true;
					}
				}
				foreach (GameObject t in DadArray.bluetower) {
					if (t == gegner) {
						turm = true;
					}
				}
				if (gegner == DadArray.bluecore) {
					CoreScriptblue enemyscript;
					enemyscript = gegner.GetComponent<CoreScriptblue> ();
					enemyscript.Damage (attack);
				} else if (spieler == true){
					MovementCharacter enemyscript = gegner.GetComponent<MovementCharacter> ();
					enemyscript.Damage (attack);
				}else {
						if (turm == true) {
						Towershootsblue enemyscript; 
						enemyscript = gegner.GetComponent<Towershootsblue> ();
						enemyscript.Damage (attack);
					} else {
						MinionMovementblue enemyscript; 
						enemyscript = gegner.GetComponent<MinionMovementblue> ();
						bool unwichtig = enemyscript.Damage (attack);
					}
				}
				wait = waittime;
				//play animation
			} else {
				move ();
			}
		}
	}

	void move(){
		//float y = transform.eulerAngles.y * Mathf.Deg2Rad;
		//float z = Mathf.Sin (y * Mathf.PI / 180) * Time.deltaTime;
		//float x = Mathf.Sin (y * Mathf.PI / 180 + 45 / 2 * Mathf.PI) * Time.deltaTime;
		//Vector3 direction = new Vector3 (x, 0, z);
		//transform.position = Vector3.Lerp (transform.position, direction, 10f);
		//Debug.Log("red" + direction);
		transform.Translate(0,0,movespeed);
	}






	public bool Damage(int damage){
		Debug.Log("red damage");
		damage = damage - def;
		if (damage > 0) {
			health = health - damage;
		}
		if(health <= 0){
			DadArray.outofthearrayred(gameObject);
			dead = true;
			return true;
		}
		return false;
	}


		


}
