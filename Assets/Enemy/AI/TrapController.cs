using UnityEngine;
using System.Collections;

public class TrapController : Shooter {
	/*
	 * 	This script extends Shooter
	 * 
	 * The behavior for the trapbot will proceed as such
	 * 	they will work in tandum with the drones
	 * 	while they are idle, they will remain near the drones patrol paths, so they can be alerted if need be
	 * 		once cautious they will approach the player's position, as noted by the drone
	 * 		whether while idle or cautious, if the player enters their line of sight, they will enter into an attack state
	 * 			in the attack state, they will have higher durability
	 * 			they will also shoot more aggressively
	 * 				if time permits, i will have them change directions if shot or have evasive maneuvers
	 * 			if while in an attack state, and the player hides for a certain time, they will leave the attack state and head back to their original position
	 * 	
	 * 	
	 * */

	public bool idle, cautious, attack;
	Vector3 myForward, tempPlayerPosition, toOther, original;
	int hiding;
	ParticleSystem explode;
	RaycastHit checkPlayer;
	NavMeshAgent agent;
	public GameObject player;
	GameObject spawn;
	
	// Use this for initialization
	void Start () {
		original = this.gameObject.transform.position;
		agent = GetComponent<NavMeshAgent> ();
		explode = GetComponent<ParticleSystem> ();
		spawn = GameObject.Find ("Spawn");
		
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
						if (checkPlayer.collider.tag == "Player")
							beginAttack ();
					} 
				}
			}
		}
		
		if (attack) {
			assault ();
			checkDistance();
		}

		if (Hits == 5) {
			hitsAchieved();
			Destroy(this.gameObject.GetComponent<TrapController>());
		}
	}

	void FixedUpdate () {
		if (attack)
			checkHiding ();
	}
	
	public void beginIdle(){
		idle = true;
		cautious = false;
		attack = false;

		agent.SetDestination (original);
		hiding = 0;
	}
	
	void beginCautious(Vector3 temp){
		
		if (attack == false) {
			idle = false;
			cautious = true;
			attack = false;

			agent.stoppingDistance = 5;
			agent.SetDestination (temp);
		}
	}
	
	void beginAttack(){
		
		idle = false;
		cautious = false;
		attack = true;

		agent.stoppingDistance = 10;
		agent.SetDestination (player.transform.position);
	}
	
	void assault(){
		
		Vector3 whereToLook;
		whereToLook.x = player.transform.position.x;
		whereToLook.y = this.transform.position.y;
		whereToLook.z = player.transform.position.z;
		transform.LookAt (whereToLook);

		
		int checkShoot = Random.Range (0, 300);
		if (checkShoot == 0)
			Shoot(spawn.transform.position, player.transform.position, 80);
	}
	
	void checkDistance(){
		if(Vector3.Distance(transform.position, player.transform.position) >  10){
			agent.SetDestination(player.transform.position);
		}
		else {
			agent.SetDestination(transform.position);
		}
	}

	void checkHiding(){
		Physics.Raycast (transform.position, transform.forward, out checkPlayer);
		if (checkPlayer.collider.tag != player.gameObject.tag.ToString()) {
			hiding++;
		}
		else {
			hiding = 0;
		}
		if (hiding == 500)
			restart ();

	}

	void restart(){
		Collider[] objectsHit = Physics.OverlapSphere (transform.position, 20f);
		int i = 0;
		while (i < objectsHit.Length) {
			if(objectsHit[i].gameObject.tag == "Enemy")
				objectsHit[i].BroadcastMessage("beginIdle");
			i++;
		}

		beginIdle ();
	}

	void hitsAchieved(){
		idle = cautious = attack = false;
		Destroy (this.gameObject.GetComponent<Animator>());
		Destroy (agent);
		explode.Emit (1);
	}
}
