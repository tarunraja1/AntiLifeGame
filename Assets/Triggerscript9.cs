using UnityEngine;
using System.Collections;

public class Triggerscript9 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void print()
	{
		print ("Start trigger9");
		print ("Found the key to enter the front door");
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
