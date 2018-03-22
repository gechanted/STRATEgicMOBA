using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotationBody : MonoBehaviour {

	public int floorMask;
	public Rigidbody playerRigidbody;
	float camRayLength = 100f;
	public Camera cam;
	public int coreMask;
	public bool isMenuActive = false;
	public MovementCharacter anderesScript;

	public bool isMenuScriptActive = false;
	public GameObject canvas;
	public EnemyList Dadarray;
	public GameObject redcore;
	GameObject[] buttonliste = new GameObject[7];
	public List<RectTransform> buttonlist;
	//RectTransform[] mmhh = new RectTransform[7];



	void Awake(){
		buttonliste = GameObject.FindGameObjectsWithTag ("Button");
		foreach (GameObject t in buttonliste) {
			//Debug.Log (t.name);
			//t.SetActive (false);
			RectTransform uiui = t.GetComponent<RectTransform> ();
			//Debug.Log (uiui.sizeDelta);
			buttonlist.Add (uiui);
			uiui.sizeDelta = new Vector2 (0f, 0f);
		}
		//Debug.Log (buttonlist.Count);
		anderesScript = GetComponentInParent<MovementCharacter> ();
		floorMask = LayerMask.GetMask ("Floor");
		playerRigidbody = GetComponent<Rigidbody> ();
		coreMask = LayerMask.GetMask("redbuild");
		canvas = GameObject.FindGameObjectWithTag ("canvas");//GetComponentInChildren<Canvas> ();
		//Change Canvas to Buttons+ etc.
		//canvas.SetActive(false);
		GameObject liste;
		liste = GameObject.FindWithTag ("gegnerliste");
		Dadarray = liste.GetComponent<EnemyList> ();
		redcore = Dadarray.redcore;

	}	

	void Update () {
		playerRigidbody.angularVelocity = Vector3.zero;
		Updateerweitert ();
		Turning ();
		//Debug.Log ("bis hierhin, " + isMenuScriptActive);
	}

	void Updateerweitert(){
		if (isMenuScriptActive == true) {
			UI ();
			//Debug.Log ("und net weiter...");
		}else{
			CoreUI ();
			//Debug.Log("und iwie weiter");
			//Debug.Log(Input.mousePosition);
		}
	}

	void Turning (){
		Ray camRay = cam.ScreenPointToRay (Input.mousePosition);
		RaycastHit floorHit;
		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)){
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;
			//Vector3 lookingpoint = floorHit.point;
			//lookingpoint.y = 1f;
			//transform.LookAt (lookingpoint);
			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
			playerRigidbody.MoveRotation (newRotation);
			if (Input.GetMouseButtonDown (1)) {
				playerToMouse = playerToMouse.normalized;
				anderesScript.Attack (playerToMouse);
			}
		}
	}

	void CoreUI(){
		//Debug.Log ("CoreUI() wird ausgeführt?!");
		Ray camRay = cam.ScreenPointToRay (Input.mousePosition);
		RaycastHit floorHit;
		if (Physics.Raycast (camRay, out floorHit, camRayLength, coreMask)) {
			if (Input.GetMouseButtonDown(0)) {
				isMenuScriptActive = true;
				//Debug.Log ("if ist durch");
			}
		}
	}

	void UI(){
		//Debug.Log ("das ist Bullshit");
		GameObject gme = anderesScript.MeinGameobject ();
		Transform tme = gme.transform;
		float dist = Vector3.Distance (redcore.transform.position, tme.position); //Dadarray.
		Debug.Log (dist);
		if (dist > 15) {
			Debug.Log ("wtf");
			isMenuScriptActive = false;
			foreach (RectTransform t in buttonlist) {
				t.sizeDelta = new Vector2 (0f, 0f);
				Debug.Log (t.sizeDelta);
				//Debug.Log (t.name);
			}
			//Debug.Log ("wut");
			//canvas.SetActiveRecursively (!canvas.activeSelf);	  
			isMenuActive = false;
		} else {
			if (!isMenuActive) {
				isMenuActive = true;
				Debug.Log ("no way");
				//canvas.SetActive(true);
				foreach (RectTransform t in buttonlist) {
					t.sizeDelta = new Vector2 (160f, 30f);
					//Debug.Log (t.name);
				}
			}
		}
	}

	public bool Damage(int damage){
		return anderesScript.Damage (damage);

	}





}


