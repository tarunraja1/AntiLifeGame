using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class DoorScript : MonoBehaviour {

	private float angle = 90.0f;
	private Vector3 originalRotation,opening;
	private static GameObject help;
	private static Text Help;
	private bool canOpen{get; set;}
	private bool enteredTrigger,finishedBases,pressedF;

	// Use this for initialization
	void Start () {
		help = GameObject.Find("Help");
		Help = help.GetComponent<Text>(); 
		canOpen = false; pressedF =false;
		enteredTrigger = false; finishedBases = false;
		originalRotation = transform.eulerAngles;
		opening = new Vector3 (originalRotation.x, originalRotation.y + angle, originalRotation.z);
	}
	
	// Update is called once per frame
	void Update () {
		if(pressedF &&enteredTrigger)
		{
			//Open door
			transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, opening, Time.deltaTime * 2.0f);
			Help.text="";

		}
		else if (!canOpen&&enteredTrigger) 
		{
			var dialogue = GameObject.Find("Dialogue");
			dialogue.GetComponent<Text>().text = "It's Locked.";
			enteredTrigger = false;
		}
		else
			transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, originalRotation, Time.deltaTime * 2.0f);

		if (!pressedF && Input.GetKeyDown(KeyCode.F))
		{
			pressedF = (canOpen==true)? true:false;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") 
			enteredTrigger = true;
		
		if(this.gameObject.tag=="TunnelDoor")
		{
			if(finishedBases == true)
			{
				canOpen = true;
				Help.text = "Press F to open";
			} 
			else 
				canOpen = false;

			print(canOpen);
		}
		else
		{
			canOpen = true;
			Help.text = "Press F to open";
		}
	}

	void OnTriggerExit(Collider other)
	{ 
		var dialogue = GameObject.Find("Dialogue");
		if (other.gameObject.tag == "Player") 
			enteredTrigger = false;

		Help.text = " ";
		dialogue.GetComponent<Text>().text=" ";
		pressedF=false;
		canOpen = false;
	}


	
}
