using UnityEngine;
using System.Collections;

public class Triggerscript3 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void print3()
	{
		print ("Start trigger3");
		print ("The conversation about seeing the military base in the distance");
	}

	// Update is called once per frame
	void Update () {
		transform.Rotate(0,1.0F,0);
	}

	void OnTriggerEnter(Collider other) {
		Destroy(this.gameObject);
		print3();

		//DialogScript.DialogueStart();

	}

}
