using UnityEngine;
using System.Collections;

public class Triggerscript10 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void print()
	{
		print ("Start trigger10");
		print ("Placing the EMP here would blow up the entire network of Darwin");
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
