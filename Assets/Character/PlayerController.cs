using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : Shooter {
  
  Transform cameraTransform, playerTransform;
  public float behind = 7f;
  public float up = 3f;
  public float smoothUp = 0.2f ;
  public float smoothHoriz = 0.3f;
  public float maxSpeed = 720f;
  public float HeadOffset { get; set; }
  public float CameraStop { get; set; }
  public bool shooting = false;
  public GameObject _chest = null;
  public GameObject _lookAt = null;
  public GameObject aimPoint;
  public float playerAccuracy = 1;
  public float lastShot = 10;
  bool snap = false;
  public int lastHit;
  public Vector3 cameraPosition;
  public float x, y, z;
  public bool zoomed = false;
  Vector3 centerOffset = Vector3.zero;
  Camera cameraMain{ get; set; }
  GameObject player, spawn, gun, cameraCenter;
  List<GameObject> bullets;
  Transform oldCameraTransform;
  public IKPlayerController IkController{ get; set; }
  // Use this for initialization
  void Start () {
    bullets = new List<GameObject>();
    cameraMain = Camera.main;
    spawn = GameObject.Find("Spawn");
    cameraCenter = GameObject.Find("CameraCenter");
    player = gameObject;
    gun = GameObject.Find("Gun");
    cameraTransform = cameraMain.transform;
    centerOffset = player.GetComponent<Collider>().bounds.center - cameraTransform.position;
    HeadOffset = player.GetComponent<Collider>().bounds.max.y - cameraTransform.position.y;
    playerTransform = player.transform;
    oldCameraTransform = cameraTransform;
    IkController = player.GetComponent<IKPlayerController>();
    shooting = false;
    lastShot = 10;

  }
	
  // Update is called once per frame
  void Update () {
    if(!GameMenu.pause)
    {
      MoveCamera(cameraTransform, centerOffset);
      AimIn();
      CheckForShootingAnimation();
      PlayerShoot();
      PlayerHealth();
    }
  }

  void PlayerHealth()
  {
    if(Hits > 0 && lastHit > 5)
      Hits -= 1;
    if(Hits > 4)
      GameOver();
  }

  void GameOver()
  {
    var rBodies = player.GetComponentsInChildren<Rigidbody>();
    
    foreach( var r in rBodies)
      r.isKinematic = true;
  }
  
  void PlayerShoot()
  {
    if(Input.GetButtonDown("Fire1"))
    {
      shooting = true;
      IKPlayerController.ikActive = shooting;
      RaycastHit hit;
      Vector3 point;
      if(Physics.Raycast(
                      cameraMain.transform.position+ cameraTransform.forward,
                      cameraMain.transform.forward,
                      out hit,
                      10000f
                         ))
        point = hit.point;
      else
        point = cameraTransform.position + cameraTransform.forward * 1000;
      lastShot = 0;
      Shoot(spawn.transform.position, point, playerAccuracy);
    }                
  }

  void CheckForShootingAnimation()
  {
    lastShot += Time.deltaTime;
    shooting = zoomed || lastShot < 2;
  }
  
  void AimIn()
  {
    var smooth = 5f;
    if(Input.GetButtonDown("Fire2"))
      zoomed = true;
    if (shooting)
    {
      RaycastHit hit;
      Vector3 point;
      if(Physics.Raycast(
                      cameraMain.transform.position+ cameraTransform.forward,
                      cameraMain.transform.forward,
                      out hit,
                      10000f
                         ) && hit.distance > 5)
        point = hit.point;
      else
        point = cameraTransform.position + cameraTransform.forward * 1000;
      
      aimPoint.transform.LookAt(point);
      gun.transform.LookAt(point);
      //aimPoint.transform.LookAt(point);
      var rotateTo =cameraCenter.transform.rotation;
      player.transform.rotation = rotateTo;
      cameraCenter.transform.rotation = rotateTo;

      
      
    }
    zoomed &= !Input.GetButtonUp("Fire2");
    var zoom = zoomed ? 60 : 100;
    IKPlayerController.ikActive = shooting;
    
    
      
    cameraMain.fieldOfView = Mathf.Lerp(cameraMain.fieldOfView, zoom, Time.deltaTime * smooth);
  }

  void MoveCamera(Transform target, Vector3 center)
  {
    cameraCenter.transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * GameMenu.XSensitivity);
    
    if((cameraTransform.localPosition.y <= -0.75f && Input.GetAxis("Mouse Y") * GameMenu.invertValue >= 0) || cameraTransform.localPosition.y >= 0.75f && Input.GetAxis("Mouse Y") * GameMenu.invertValue <= 0 || Mathf.Abs(cameraTransform.localPosition.y) < 0.75f)//(Mathf.Abs(Input.GetAxis("Mouse Y")) > 0.1f)
    {
      x = cameraTransform.localPosition.x;
      y = cameraTransform.localPosition.y;
      z = cameraTransform.localPosition.z;
     
      cameraTransform.RotateAround(cameraCenter.transform.position, cameraCenter.transform.right * Input.GetAxis("Mouse Y") * GameMenu.YSensitiviy * GameMenu.invertValue, 1f);
    }
  }

  
  
  
   public Texture2D crosshairTexture;
     public float crosshairScale = 1;
     void OnGUI()
     {
       //if not paused
         if(Time.timeScale != 0)
         {
             if(crosshairTexture!=null)
               GUI.DrawTexture(new Rect(
                                        (Screen.width-crosshairTexture.width*crosshairScale)/2 ,
                                        (Screen.height-crosshairTexture.height*crosshairScale)/2, 
                                        crosshairTexture.width*crosshairScale,
                                        crosshairTexture.height*crosshairScale),
                               crosshairTexture);
             else
                 Debug.Log("No crosshair texture set in the Inspector");
         }
     }
     
}



