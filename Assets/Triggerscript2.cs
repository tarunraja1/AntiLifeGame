using UnityEngine;
using System.Collections;

public class Triggerscript2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void print2()
	{
		print ("Start trigger2");
		print ("First fight takes place here after meeting npc");
	}

	// Update is called once per frame
	void Update () {
		transform.Rotate(0,1.0F,0);
	}

	void OnTriggerEnter(Collider other) {
		Destroy(this.gameObject);
		print2();

		//DialogScript.DialogueStart();

	}

}
