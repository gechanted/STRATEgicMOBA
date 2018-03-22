using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MovementCharacter : MonoBehaviour {


	public Text texp;
	public Text tcoins;
	public EnemyList Dadarray;

	int level = 1;
	int coins = 0;
	int exp = 0;

	bool teamrot = true;
	public GameObject Projektil;

	public int health = 20;
	public int attack = 2;
	int def = 0;
	float moveSpeed = 5f;
	float waittime = 3f;
	int waittimecounter = 0;
	float respawncounter = 0f;
	bool dead = false;



	void Start(){
		GameObject liste;
		liste = GameObject.FindWithTag ("gegnerliste");
		Dadarray = liste.GetComponent<EnemyList> ();
		GameObject tempx = GameObject.Find ("xp");
		GameObject tempc = GameObject.Find ("coins");
		texp = tempx.GetComponent<Text> ();
		tcoins = tempc.GetComponent<Text> ();
		if(teamrot == true){
			Dadarray.intothearrayred(gameObject);
			Dadarray.redplayer.Add (gameObject);
		}else{
			Dadarray.intothearrayblue(gameObject);
			Dadarray.blueplayer.Add (gameObject);
		}
	}



	void Update () {
		if (dead == true) {
			if (respawncounter < 0) {
				dead = false;
			}
			respawncounter -= Time.deltaTime;
			return;
		}
		UIupdate ();
		waittime -= Time.deltaTime;
		if (Input.GetKey (KeyCode.W)) {
			transform.Translate (new Vector3 (0, 0, 1) * Time.deltaTime * moveSpeed);
		}

		if (Input.GetKey (KeyCode.S)) {
			transform.Translate (new Vector3 (0, 0, -1) * Time.deltaTime * moveSpeed);
		}

		if (Input.GetKey (KeyCode.D)) {
			transform.Translate (new Vector3 (1, 0, 0) * Time.deltaTime * moveSpeed);
		}

		if (Input.GetKey (KeyCode.A)) {
			transform.Translate (new Vector3 (-1, 0, 0) * Time.deltaTime * moveSpeed);
		}

					

	}

	public GameObject MeinGameobject(){
		return gameObject;
	}


	public bool Damage(int damage){
		damage = damage - def;
		if (damage > 0) {
			health = health - damage;
		}
		if(health <= 0){
			//die...
			respawncounter = 30f;
			dead = true;
			if (teamrot == true) {
				transform.position = new Vector3 (-70, 0, -70);
			} else {
				transform.position = new Vector3 (70, 0, 70);
			}
			return true;
		}
		return false;
	}

	public void Attack(Vector3 richtung){
		if (waittime < 0) {
			GameObject projectile = Instantiate (Projektil, gameObject.transform.position, gameObject.transform.rotation);
			playerProjektileScript script = projectile.GetComponent<playerProjektileScript> ();
			script.inhaber (this);
			script.richtung (richtung);
			script.teamrot = teamrot;
			waittime = 3f;
		}
	}

	public void kill(int dollars, int xp){
		exp += xp;
		coins += dollars;
		if (level * 100 + 50 < xp) {
			xp -= level * 100 + 50;
			if (level < 15) {
				level++;
				attack += 2;
				def += 1;
				health += 5;
			}
		}
	}

	void UIupdate(){
		
		texp.text = "xp: " + exp.ToString ();
		tcoins.text = "coins: " + coins.ToString ();


	}


	public void Matk(){
		if (coins > 100) {
			coins -= 100;
			if (teamrot == true) {
				Dadarray.minionredattack += 2;
			} else {
				Dadarray.minionblueattack += 2;
			}
		}
	}

	public void Mdef(){
		if (coins > 100) {
			coins -= 100;
			if (teamrot == true) {
				Dadarray.minionreddef += 1;
			} else {
				Dadarray.minionbluedef += 1;
			}
		}
	}

	public void Mheal(){
		if (coins > 100) {
			coins -= 100;
			if (teamrot == true) {
				Dadarray.minionredhealth += 2;
			} else {
				Dadarray.minionbluehealth += 2;
			}
		}
	}

	public void Mvatk(){
		if (coins > 100) {
			if (waittimecounter < 14) {
				coins -= 100;
				waittimecounter++;
				if (teamrot == true) {
					Dadarray.minionredwait -= 0.2f;
				} else {
					Dadarray.minionbluewait -= 0.2f;
				}
			}
		}
	}









	public void Tatk(){
		if (coins > 100) {
			coins -= 100;
			if (teamrot == true) {
				Dadarray.towerredattack += 5;
			} else {
				Dadarray.towerblueattack += 5;
			}
		}
		Dadarray.setup ();
	}

	public void Tdef(){
		if (coins > 100) {
			coins -= 100;
			if (teamrot == true) {
				Dadarray.towerreddef += 2;
			} else {
				Dadarray.towerbluedef += 2;
			}
		}
		Dadarray.setup ();
	}

	public void Theal(){
		if (coins > 100) {
			coins -= 100;
			if (teamrot == true) {
				Dadarray.towerredhealth += 50;
				Dadarray.towerbluehealth += 50;
				CoreScriptred rscript = Dadarray.bluecore.GetComponent<CoreScriptred> ();
				rscript.health += 50;
				foreach (GameObject t in Dadarray.bluetower) {
					Towershootsblue script = t.GetComponent<Towershootsblue> ();
					script.health += 50;
				}
			} else {
				Dadarray.towerbluehealth += 50;
				CoreScriptblue bscript = Dadarray.bluecore.GetComponent<CoreScriptblue> ();
				bscript.health += 50;
				foreach (GameObject t in Dadarray.bluetower) {
					Towershootsblue script = t.GetComponent<Towershootsblue> ();
					script.health += 50;
				}
			}
		}
	}

	public void Tvatk(){
		if (coins > 100) {
			if (waittimecounter < 14) {
				coins -= 100;
				waittimecounter++;
				if (teamrot == true) {
					Dadarray.towerredwait -= 0.2f;
				} else {
					Dadarray.towerbluewait -= 0.2f;
				}
			}
		}
		Dadarray.setup ();
	}










}

