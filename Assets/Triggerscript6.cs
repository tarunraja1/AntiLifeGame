using UnityEngine;
using System.Collections;

public class Triggerscript6 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void print6()
	{
		print ("Start trigger6");
		//print ("After killing robots here, sighs");
		print ("Hero presses the switch and sees the Robot base light up in the distance");
		//print ("Hero talks about no survivors but to be careful of patrolling robots");
	}

	// Update is called once per frame
	void Update () {
		transform.Rotate(0,1.0F,0);
	}

	void OnTriggerEnter(Collider other) {
		Destroy(this.gameObject);
		print6();

		//DialogScript.DialogueStart();

	}

}
