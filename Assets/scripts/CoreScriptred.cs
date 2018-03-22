using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreScriptred : MonoBehaviour {

	public GameObject gegner = null;
	public Rigidbody rigidbody;
	public EnemyList DadArray;
	public GameObject Minion;

	public int health = 200;
	int attack = 5;
	int def = 2;
	float wait = 1;
	float waittime = 3f;
	bool dead = false;
	float totenwache = 1;
	public float minionspawnwaittime = 5f;

	public GameObject walkm;
	public GameObject walkr;
	public GameObject walkl;

	public GameObject rr1;
	public GameObject rr2;
	public GameObject rr3;
	public GameObject rm1;
	public GameObject rm2;
	public GameObject rm3;
	public GameObject rl1;	
	public GameObject rl2;
	public GameObject rl3;


	void Start(){
		GameObject liste = GameObject.FindWithTag ("gegnerliste");
		DadArray = liste.GetComponent<EnemyList> ();
		DadArray.intothearrayred(gameObject);
		DadArray.redcore = gameObject;
		rigidbody = GetComponent<Rigidbody> ();


		walkl = GameObject.Find ("walkingpointl");
		walkr = GameObject.Find ("walkingpointr");
		walkm = GameObject.Find ("walkingpointm");


		rr1 = GameObject.Find ("rr1");
		rr2 = GameObject.Find ("rr2");
		rr3 = GameObject.Find ("rr3");
		rm1 = GameObject.Find ("rm1");
		rm2 = GameObject.Find ("rm2");
		rm3 = GameObject.Find ("rm3");
		rl1 = GameObject.Find ("rl1");
		rl2 = GameObject.Find ("rl2");
		rl3 = GameObject.Find ("rl3");

	}


	void Update () {
		minionspawnwaittime -= Time.deltaTime;
		if (minionspawnwaittime < 0) {
			minionspawnwaittime = 60f;
			spawn ();
		}
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
				MinionMovementblue enemyscript; 
				enemyscript = gegner.GetComponent<MinionMovementblue>();

				enemyscript.Damage(attack);
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

	void spawn(){
		GameObject nr1 = Instantiate (Minion, rr1.transform.position, rr1.transform.rotation);
		GameObject nr2 = Instantiate (Minion, rr2.transform.position, rr2.transform.rotation);
		GameObject nr3 = Instantiate (Minion, rr3.transform.position, rr3.transform.rotation);

		GameObject nr4 = Instantiate (Minion, rm1.transform.position, rm1.transform.rotation);
		GameObject nr5 = Instantiate (Minion, rm2.transform.position, rm2.transform.rotation);
		GameObject nr6 = Instantiate (Minion, rm3.transform.position, rm3.transform.rotation);

		GameObject nr7 = Instantiate (Minion, rl1.transform.position, rl1.transform.rotation);
		GameObject nr8 = Instantiate (Minion, rl2.transform.position, rl2.transform.rotation);
		GameObject nr9 = Instantiate (Minion, rl3.transform.position, rl3.transform.rotation);


		MinionMovementred no1 = nr1.GetComponent<MinionMovementred> ();
		MinionMovementred no2 = nr2.GetComponent<MinionMovementred> ();
		MinionMovementred no3 = nr3.GetComponent<MinionMovementred> ();

		MinionMovementred no4 = nr4.GetComponent<MinionMovementred> ();
		MinionMovementred no5 = nr5.GetComponent<MinionMovementred> ();
		MinionMovementred no6 = nr6.GetComponent<MinionMovementred> ();

		MinionMovementred no7 = nr7.GetComponent<MinionMovementred> ();
		MinionMovementred no8 = nr8.GetComponent<MinionMovementred> ();
		MinionMovementred no9 = nr9.GetComponent<MinionMovementred> ();

		no1.walkingpoint = walkr;
		no2.walkingpoint = walkr;
		no3.walkingpoint = walkr;

		no4.walkingpoint = walkm;
		no5.walkingpoint = walkm;
		no6.walkingpoint = walkm;

		no7.walkingpoint = walkl;
		no8.walkingpoint = walkl;
		no9.walkingpoint = walkl;
		Debug.Log("isda  " + walkl.transform.position);
	}

}
