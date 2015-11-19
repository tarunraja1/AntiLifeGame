using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 *	This script extends Shooter
 * 	
 * The behavior for the drones will proceed as such
 * 	Drones will be on patrol routes (check method PatrolPath for more info)
 * 		when idle they will patrol
 * 		if alerted they will enter cautious
 * 			on cautious, they will approach the last known position of the player
 * 		if while idle or cautious, the player enters their line of sight within a certain field of view
 * 		they will enter an attack state
 * 			upon entering attack, they will no longer move, and will send all units within a certain radius into a cautious state
 * 			they will also relay the current position of the player to said enemies
 * 			if the player is a certain distance from the drone, they will approach the player
 * */
public class DroneController: Shooter {

	public bool idle, cautious, attack;

	public Transform startPosition;
	public Transform firstPatrol;
	public Transform secondPatrol;
	public Transform lastPatrol;

	Vector3 myForward, tempPlayerPosition, toOther;

	RaycastHit checkPlayer;
	ParticleSystem explode;
	Rigidbody myObject;

	NavMeshAgent agent;
	GameObject spawn;
	public GameObject player;

	// Use this for initialization
	void Start () {
		explode = GetComponent<ParticleSystem> ();
		spawn = GameObject.Find ("Spawn");

		myObject = GetComponent<Rigidbody> ();
		agent = GetComponent<NavMeshAgent> ();

		cautious = attack = false;

		beginIdle ();
	}
	
	// Update is called once per frame
	void Update () {
		if (idle || cautious) {
			if (Vector3.Distance (transform.position, player.transform.position) < 10) {

				myForward = transform.TransformDirection (Vector3.forward);
				toOther = spawn.transform.position - transform.position;

				if (Vector3.Dot (myForward, toOther) > .5) {
					if (Physics.Raycast (spawn.transform.position, myForward, out checkPlayer)) {
						if (checkPlayer.collider.tag == player.gameObject.tag.ToString())
							beginAttack ();
					} 
				}
			}
		}

		if (attack) {
			assault ();
			checkDistance();
		}

		if (Hits == 3) {
			Debug.Log("Should be destroyd");
			hitsAchieved ();
			Destroy(this.gameObject.GetComponent<DroneController>());
		}
	}
	
	void beginIdle(){
		idle = true;
		cautious = false;
		attack = false;

		agent.stoppingDistance = 1;
		StartCoroutine ("PatrolPath");
	}

	void beginCautious(Vector3 temp){
		StopCoroutine ("PatrolPath");

		if (attack == false) {
			idle = false;
			cautious = true;
			attack = false;

			agent.stoppingDistance = 5;
			agent.SetDestination (temp);
		}
	}

	void beginAttack(){
		StopCoroutine ("PatrolPath");

		idle = false;
		cautious = false;
		attack = true;

		agent.stoppingDistance = 10;
		agent.SetDestination (player.transform.position);
		sendMessage ();
	}

	void sendMessage(){
		Collider[] objectsHit = Physics.OverlapSphere (transform.position, 20f);
		int i = 0;
		while (i < objectsHit.Length) {
			if(objectsHit[i].gameObject.tag == "Enemy")
				objectsHit[i].BroadcastMessage("beginCautious", player.transform.position);
			i++;
		}
	}

	void assault(){

		Vector3 whereToLook;
		whereToLook.x = player.transform.position.x;
		whereToLook.y = this.transform.position.y;
		whereToLook.z = player.transform.position.z;
		transform.LookAt (whereToLook);

		sendMessage ();

		int checkShoot = Random.Range (0, 100);
		if (checkShoot == 0)
			Shoot (spawn.transform.position, player.transform.position, 60f);
	}

	void checkDistance(){
		if(Vector3.Distance(transform.position, player.transform.position) >  10){
			agent.SetDestination(player.transform.position);
		}
		else {
			agent.SetDestination(transform.position);
		}
	}

	void hitsAchieved(){
		idle = cautious = attack = false;
		Destroy (this.gameObject.GetComponent<Animator>());
		explode.Emit (1);
		Destroy (this.gameObject.GetComponent<NavMeshAgent>());
		myObject.AddForce (Vector3.down*5);
	}

	/*
	 * Patrol Path will utilize a maximum of 4 patrol positions and will wait a variabled amount of seconds (this can be adjusted as you see fit)
	 * sorry, i couldnt figure out a more efficient method for implementing a patrol path
	 * */
	IEnumerator PatrolPath(){
		for (int path = 0; path < 5; path++) {
			switch (path){
			case 0:
				PatrolStart();
				yield return new WaitForSeconds (10);
				break;
			case 1:
				PatrolFirst();
				yield return new WaitForSeconds (10);
				break;
			case 2:
				PatrolSecond();
				yield return new WaitForSeconds (10);
				break;
			case 3:
				PatrolFinal();
				yield return new WaitForSeconds (10);
				break;
			case 4:
				path = -1;
				yield return new WaitForSeconds (1);
				break;
			}
		}
	}

	public void PatrolStart(){
		agent.SetDestination (startPosition.position);
	}

	public void PatrolFirst(){
		agent.SetDestination (firstPatrol.position);
	}

	public void PatrolSecond(){
		agent.SetDestination (secondPatrol.position);
	}

	public void PatrolFinal(){
		agent.SetDestination (lastPatrol.position);
	}
}
