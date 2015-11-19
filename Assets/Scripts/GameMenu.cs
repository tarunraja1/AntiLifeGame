using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameMenu : MonoBehaviour {
	
  public GameObject MPanel;
  public GameObject OPanel;
  public GameObject CPanel;
  public GameObject MOPanel;
  public GameObject thePanel;
  public GameObject mButton;
  public static float YSensitiviy=0,XSensitivity=0;
  public Slider Yslider,Xslider;
  public GameObject Yslid,Xslid;//,textY,textX;
  private float speed = 5f;
  public Text textX, textY;
  public static int invertValue;
  public static bool pause;
  //private int magAmmo,totalAmmo;
  // Use this for initialization
  void Start () {

    invertValue = -1;
  }
	
  // Update is called once per frame
  void Update () {
    if(this.gameObject.name == "robotbody")
    {
      transform.Rotate(Vector3.left, speed *Time.deltaTime);
    }
    if(this.gameObject.tag == "Core") 
    { 
      transform.Rotate(Vector3.up, speed *Time.deltaTime);
    }
                
    textX.text= Xslider.value.ToString();
    textY.text= Yslider.value.ToString();

    XSensitivity = Xslider.value;
    YSensitiviy = Yslider.value; 
                
    print (Yslider.value + " y " + Xslider.value + "x");

  }
  public static void RotateCompass(Quaternion q)
  {
    
  }
  public void invert()
  {
    invertValue *= -1;
  }
  public void pauseGame () 
  {
    pause = true;
    Time.timeScale = 0.0f;
    var targetPosition = thePanel.transform.position;
    MPanel.transform.position = targetPosition;
		
  } 
  public void resumeGame () 
  {
    Time.timeScale = 1.0f;
    var targetPosition = OPanel.transform.position;
    MPanel.transform.position = targetPosition;
    pause = false;
  }
	
  public void options () 
  {
    var targetPosition = OPanel.transform.position;
    MPanel.transform.position = targetPosition;
    OPanel.transform.position = thePanel.transform.position;
		
  }
	
  public void missionObjective () 
  {
    var targetPosition = MOPanel.transform.position;
    MPanel.transform.position = targetPosition;
    MOPanel.transform.position = thePanel.transform.position;
		
  }
	
  public void controls () 
  {
    var targetPosition = CPanel.transform.position;
    MPanel.transform.position = targetPosition;
    CPanel.transform.position = thePanel.transform.position;
    OPanel.transform.position = targetPosition;
		
  }
  public void back1 () 
  {
    var targetPosition = CPanel.transform.position;
    MPanel.transform.position = thePanel.transform.position;
    OPanel.transform.position = targetPosition;
    MOPanel.transform.position = targetPosition;
		
  }
  public void back2 () 
  {
    var targetPosition = MPanel.transform.position;
    OPanel.transform.position = thePanel.transform.position;
    CPanel.transform.position = targetPosition;
		
  }
  public void saveExit () 
  {
    Application.LoadLevel ("Main Menu");
		
  }
	
}
