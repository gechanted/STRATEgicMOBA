using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerProjektileScript : MonoBehaviour {

	Vector3 direction;
	MovementCharacter absender;
	public float speed = 5f;
	float wait = 5f;
	public bool teamrot;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		wait -= Time.deltaTime;
		if(wait < 0){Destroy (gameObject);}
		transform.Translate (direction * speed * Time.deltaTime);
	}

	public void richtung(Vector3 rich){
		direction = rich;
	}

	public void inhaber(MovementCharacter sender){
		absender = sender;
	}

	void OnCollisionEnter (Collision col){
		if (teamrot == true) {
			if (col.gameObject.CompareTag("blue") == true) {
					MinionMovementblue script = col.gameObject.GetComponent<MinionMovementblue> ();

				//+  + 
				if (script.Damage (absender.attack) == true) {
						absender.kill (20, 20);
					}
				Destroy (gameObject);
				}
			if (col.gameObject.CompareTag("bluet")) {
					Towershootsblue script = col.gameObject.GetComponent<Towershootsblue> ();
				if (script.Damage (absender.attack)) {
						absender.kill (100, 100);
					}
				Destroy (gameObject);
				}
			foreach (GameObject t in absender.Dadarray.blueplayer) {
				if (t == col.gameObject) {
					MovementCharacter script = col.gameObject.GetComponent<MovementCharacter> ();
					script.Damage (absender.attack);
					if (script.Damage (absender.attack)) {
						absender.kill (100, 100);
					}
					Destroy (gameObject);
				}
			}
			if (col.gameObject == absender.Dadarray.bluecore) {
				CoreScriptblue script = col.gameObject.GetComponent<CoreScriptblue> ();
				script.Damage (absender.attack);
				Destroy (gameObject);
			}
			} else {
			if (col.gameObject.CompareTag("red") == true) {
					MinionMovementred script = col.gameObject.GetComponent<MinionMovementred> ();
				if (script.Damage (absender.attack)) {
						absender.kill (20, 20);
					}
				Destroy (gameObject);
				}
			if (col.gameObject.CompareTag("redt") == true) {
					Towershootsred script = col.gameObject.GetComponent<Towershootsred> ();
				if (script.Damage (absender.attack)) {
						absender.kill (100, 100);
					}
				Destroy (gameObject);
				}
			foreach (GameObject t in absender.Dadarray.redplayer) {
				if (t == col.gameObject) {
					MovementCharacter script = col.gameObject.GetComponent<MovementCharacter> ();
					script.Damage (absender.attack);
					if (script.Damage (absender.attack)) {
						absender.kill (100, 100);
					}
					Destroy (gameObject);
				}
			}
			if (col.gameObject == absender.Dadarray.redcore) {
				CoreScriptred script = col.gameObject.GetComponent<CoreScriptred> ();
				script.Damage (absender.attack);
				Destroy (gameObject);
			}

			}


	}
}
