using UnityEngine;
using System.Collections;

public class Triggerscript8 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void print()
	{
		print ("Start trigger8");
		print ("First converstion with Darwin");
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
