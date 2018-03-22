using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyList : MonoBehaviour {

	public List<GameObject> red;
	public List<GameObject> blue;
	public List<GameObject> redtower;
	public List<GameObject> bluetower;
	public List<GameObject> redplayer;
	public List<GameObject> blueplayer;
	public GameObject redcore;
	public GameObject bluecore;

	public int minionredhealth = 10;
	public int minionredattack = 2;
	public int minionreddef = 0;
	public float minionredwait = 3f;

	public int minionbluehealth = 10;
	public int minionblueattack = 2;
	public int minionbluedef = 0;
	public float minionbluewait = 3f;

	public int towerredhealth = 200;
	public int towerredattack = 5;
	public int towerreddef = 2;
	public float towerredwait = 3f;

	public int towerbluehealth = 200;
	public int towerblueattack = 5;
	public int towerbluedef = 2;
	public float towerbluewait = 3f;
	//public List<MinionMovementred> redscript;
	//public List<MinionMovementblue> bluesrcript;
	//public Dictionary<GameObject, MinionMovementred> redsth;


	public void intothearrayred(GameObject objekt){ //, MinionMovementred script
		red.Add(objekt);
		//redscript.Add(script);

	}

	public void outofthearrayred(GameObject objekt){ //, MinionMovementred script
		red.Remove(objekt);
		//redscript.Remove (script);

	}

	public void intothearrayblue(GameObject objekt){ //, MinionMovementblue script
		blue.Add(objekt);
		//bluesrcript.Add (script);
	}

	public void outofthearrayblue(GameObject objekt){ //, MinionMovementblue script
		blue.Remove(objekt);
		//bluesrcript.Remove (script);
	}

	public void intothetowerarrayred(GameObject objekt){
		redtower.Add (objekt);
	}

	public void intothetowerarrayblue(GameObject objekt){
		bluetower.Add (objekt);
	}

	public void redcoreGO(GameObject objekt){
		redcore = objekt;
	}

	public void bluecoreGO(GameObject objekt){
		bluecore = objekt;
	}

	public void setup (){
		foreach (GameObject t in redtower) {
			Towershootsred script = t.GetComponent<Towershootsred> ();
			script.setup ();
		}
		CoreScriptred rscript = redcore.GetComponent<CoreScriptred> ();
		rscript.setup ();
		foreach (GameObject t in bluetower) {
			Towershootsblue script = t.GetComponent<Towershootsblue> ();
			script.setup ();
		}
		CoreScriptblue bscript = bluecore.GetComponent<CoreScriptblue> ();
		rscript.setup ();
	}

	//public void balala (GameObject objekt, MinionMovementred script){
	//	redsth.Add (objekt, script);
	//}

}

