using UnityEngine;
using System.Collections;

public class Animations : MonoBehaviour {
    
  public Animator Animator {get; set;}
  public float V {get; set;}
  public float H {get; set;}
  public float Sprint {get; set;}
  public bool Aiming  {get; set;}
    // Use this for initialization
    void Start () {
	Animator = GetComponent<Animator>();
        
    }
	
    // Update is called once per frame
    void Update () {
	
    }
    
    void FixedUpdate()
    {
        Sprinting();
        V = Input.GetAxis("Vertical") * 0.75f;
        H = Input.GetAxis("Horizontal");
        V += Sprint;
        Aiming = gameObject.GetComponent<PlayerController>().shooting;
        Animator.SetFloat ("Walk", V);
        Animator.SetFloat ("Turn", H);
        Animator.SetFloat("Sprint", Sprint);
        Animator.SetBool("Aiming", Aiming);
        
    }
    void Sprinting () {
        if(Input.GetButton("Jump")) {
            Sprint = 1F;
        }
        else 
        {    
            Sprint = 0.0F;
        }
    }
}

