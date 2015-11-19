using UnityEngine;
using System.Collections;

public class Triggerscript11 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void print()
	{
		print ("Start trigger11");
		print ("Blow up the EMP here resulting in final darwin dialogue and witnessing explosion effect of robot base");
	}

	// Update is called once per frame
	void Update () {
		transform.Rotate(0,1.0F,0);
	}

	void OnTriggerEnter(Collider other) {
		Destroy(this.gameObject);
		print();

		//DialogScript.DialogueStart();

	}

}
