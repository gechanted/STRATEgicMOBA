using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreScriptblue : MonoBehaviour {

	public GameObject gegner = null;
	public Rigidbody rigidbody;
	public EnemyList DadArray;
	public GameObject Minion;

	public int health = 200;
	int attack = 2;
	int def = 2;
	float wait = 1;
	float waittime = 3f;
	bool dead = false;
	float totenwache = 1;
	public float minionspawnwaittime = 5f;

	public GameObject walkm;
	public GameObject walkr;
	public GameObject walkl;

	public GameObject br1;
	public GameObject br2;
	public GameObject br3;
	public GameObject bm1;
	public GameObject bm2;
	public GameObject bm3;
	public GameObject bl1;	
	public GameObject bl2;
	public GameObject bl3;


	void Start(){
		GameObject liste = GameObject.FindWithTag ("gegnerliste");
		DadArray = liste.GetComponent<EnemyList> ();
		DadArray.intothearrayblue(gameObject);
		DadArray.bluecore = gameObject;
		rigidbody = GetComponent<Rigidbody> ();


		walkl = GameObject.Find ("walkingpointl");
		walkr = GameObject.Find ("walkingpointr");
		walkm = GameObject.Find ("walkingpointm");


		br1 = GameObject.Find ("br1");
		br2 = GameObject.Find ("br2");
		br3 = GameObject.Find ("br3");
		bm1 = GameObject.Find ("bm1");
		bm2 = GameObject.Find ("bm2");
		bm3 = GameObject.Find ("bm3");
		bl1 = GameObject.Find ("bl1");
		bl2 = GameObject.Find ("bl2");
		bl3 = GameObject.Find ("bl3");

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
		foreach (GameObject t in DadArray.red)//blue or red
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
				MinionMovementred enemyscript; 
				enemyscript = gegner.GetComponent<MinionMovementred>();

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
		GameObject nr1 = Instantiate (Minion, br1.transform.position, br1.transform.rotation);
		GameObject nr2 = Instantiate (Minion, br2.transform.position, br2.transform.rotation);
		GameObject nr3 = Instantiate (Minion, br3.transform.position, br3.transform.rotation);

		GameObject nr4 = Instantiate (Minion, bm1.transform.position, bm1.transform.rotation);
		GameObject nr5 = Instantiate (Minion, bm2.transform.position, bm2.transform.rotation);
		GameObject nr6 = Instantiate (Minion, bm3.transform.position, bm3.transform.rotation);

		GameObject nr7 = Instantiate (Minion, bl1.transform.position, bl1.transform.rotation);
		GameObject nr8 = Instantiate (Minion, bl2.transform.position, bl2.transform.rotation);
		GameObject nr9 = Instantiate (Minion, bl3.transform.position, bl3.transform.rotation);

		MinionMovementblue no1 = nr1.GetComponent<MinionMovementblue> ();
		MinionMovementblue no2 = nr2.GetComponent<MinionMovementblue> ();
		MinionMovementblue no3 = nr3.GetComponent<MinionMovementblue> ();

		MinionMovementblue no4 = nr4.GetComponent<MinionMovementblue> ();
		MinionMovementblue no5 = nr5.GetComponent<MinionMovementblue> ();
		MinionMovementblue no6 = nr6.GetComponent<MinionMovementblue> ();

		MinionMovementblue no7 = nr7.GetComponent<MinionMovementblue> ();
		MinionMovementblue no8 = nr8.GetComponent<MinionMovementblue> ();
		MinionMovementblue no9 = nr9.GetComponent<MinionMovementblue> ();

		no1.walkingpoint = walkr;
		no2.walkingpoint = walkr;
		no3.walkingpoint = walkr;

		no4.walkingpoint = walkm;
		no5.walkingpoint = walkm;
		no6.walkingpoint = walkm;

		no7.walkingpoint = walkl;
		no8.walkingpoint = walkl;
		no9.walkingpoint = walkl;
	}

}
