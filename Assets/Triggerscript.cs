using UnityEngine;
using System.Collections;

public class Triggerscript : MonoBehaviour {
  bool active;
  public static int i;
  public static GameObject[] triggers = new GameObject[13];
  // Use this for initialization
  void Start () {
    active = true;
  }

  void print1()
  {
    active = false;
    i++;
    StartCoroutine(DialogScript.DialogueStart(i.ToString(), gameObject));

  }

  // Update is called once per frame
  void Update () {
    var rot = Quaternion.identity;
    GameMenu.RotateCompass(rot);
    transform.Rotate(0,1.0F,0);
    
  }

  void OnTriggerEnter(Collider other) {
    if(gameObject.Equals(triggers[i]))
      print1();
    
    //DialogScript.DialogueStart();

  }

}
