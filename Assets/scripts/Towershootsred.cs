using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Towershootsred : MonoBehaviour {

	public GameObject gegner = null;
	public Rigidbody rigidbody;
	public EnemyList DadArray;

	public int health = 200;
	int attack = 5;
	int def = 2;
	float wait = 1;
	float waittime = 3f;
	int walkingpoint;
	bool dead = false;
	float totenwache = 1;


	void Start(){
		GameObject liste = GameObject.FindWithTag ("gegnerliste");
		DadArray = liste.GetComponent<EnemyList> ();
		DadArray.intothearrayred(gameObject);
		DadArray.redtower.Add(gameObject);
		rigidbody = GetComponent<Rigidbody> ();

	}
	

	void Update () {
		rigidbody.angularVelocity = Vector3.zero;
		rigidbody.velocity = Vector3.zero;
		//Check the field and search for closest enemy minions in range
		//get Gameobject[] enemies from Enemylist.cs
		GameObject tMin = null;
		float minDist = Mathf.Infinity;
		Vector3 currentPos = transform.position;
		foreach (GameObject t in DadArray.blue)//blue or red
		{
			float dist = Vector3.Distance(t.transform.position, currentPos);
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
	}

	public void setup (){
		attack = DadArray.towerblueattack;
		def = DadArray.towerbluedef;
		waittime = DadArray.towerbluewait;
	}

	void Attack(float minDist){ //MinionMovementblue enemyscript
		//Debug.Log ("attack wird ausgeführt");
		//Look at target and follow it


		//transform.LookAt(gegner.transform);


		//Debug.Log("atttack wure aufgerufen");
		//Vector3 minionToTarget = enemy.transform.position - transform.position;
		//minionToTarget.y = 0f;
		//Quaternion newRotation = Quaternion.LookRotation (minionToTarget);
		//minionRigidbody.MoveRotation (newRotation);


		if (wait > 0) {
			wait -= Time.deltaTime;
		} else {

			if (minDist < 20) {
				//MinionMovementblue enemyscript = enemy.GetComponent("MinionMovementblue") as MinionMovementblue;
				//MinionMovementblue enemyscript = GameObject.Find("enemy").GetComponent<MinionMovementblue>();
				//Minionmovementblue enemyscript;
				bool spieler = false;
				foreach (GameObject t in DadArray.blueplayer) {
					if (t == gegner) {
						spieler = true;
					}
				}
				if (spieler == true) {
					MovementCharacter enemyscript = gegner.GetComponent<MovementCharacter> ();
					enemyscript.Damage (attack);
				} else {
					MinionMovementblue enemyscript; 
					enemyscript = gegner.GetComponent<MinionMovementblue> ();

					enemyscript.Damage (attack);
				}
				//Hit -> wait
				wait = waittime;
				//play animation
			} 
		}
	}

	public bool Damage(int damage){
		damage = damage - def;
		if (damage > 0) {
			health = health - damage;
		}
		if(health <= 0){
			DadArray.outofthearrayred(gameObject);
			DadArray.redtower.Remove (gameObject);
			dead = true;
			return true;
		}
		return false;
	}



}


