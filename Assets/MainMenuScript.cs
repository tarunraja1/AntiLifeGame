using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuScript : MonoBehaviour {

	public GameObject BPanel;
	public GameObject NPanel;
	public GameObject SPanel;
	public GameObject LPanel;
	public GameObject thePanel;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	} 

	public void loadgame () {
		var currentl = LPanel.transform.position;
		BPanel.transform.position = currentl;
		LPanel.transform.position = thePanel.transform.position;
				
	}

	public void newgame () {
		var currentn = NPanel.transform.position;
		NPanel.transform.position = thePanel.transform.position;
		BPanel.transform.position = currentn;
	}

	public void settings () {
		var currents = SPanel.transform.position;
		BPanel.transform.position = currents;
		SPanel.transform.position = thePanel.transform.position;
	}

	public void beginner () {
		Application.LoadLevel ("antilife");
	}

	public void back () {

		var currentb = BPanel.transform.position;
		NPanel.transform.position = currentb;
		BPanel.transform.position = thePanel.transform.position;
		LPanel.transform.position = currentb;
		SPanel.transform.position = currentb;
	}
	public void endgame () {
		Application.Quit();
	}
}
