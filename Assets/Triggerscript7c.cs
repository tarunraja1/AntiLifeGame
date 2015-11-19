using UnityEngine;
using System.Collections;

public class Triggerscript7c : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void print()
	{
		print ("Start trigger7c");
		print ("EMP third part has been picked up");
		print ("If player has all three parts, add EMP to inventory");
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
