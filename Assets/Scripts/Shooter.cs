using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {
  Transform StartPosition { get; set; }
  Transform TargetPosition { get; set; }
  double Speed { get; set; }
  Vector3 Target { get; set; }
  public int Hits;
  public AudioClip gunfire;
  public AudioSource audio;
  public ParticleSystem bullet, muzzleFlash;
  /*
    This function is made to enable shooting from multiple game characters 
    start is the position you want the bullet to start at,
    target is the position that the character is aiming at
    accuracy is a float between .1 and 1 to affect the angle of the acctual hit point,
    1 is perfect accuracy
   */
  public void Shoot(Vector3 start, Vector3 target, float accuracy)
  {

    var dirVector = GenerateAccuracyModifiedTarget(start, target, accuracy); 
    //get hit object
    Debug.DrawRay(start, dirVector, Color.yellow, 5f);
    bullet.Emit(start, dirVector, 10, 10, Color.yellow);
    RaycastHit hit;
    if(Physics.Raycast(start, dirVector, out hit, 10000f))
    {
      Debug.Log("Hit");
      //shoot with target hitanim
      if(hit.collider.tag == ("Shooter"))
      {
        //add hit to hit counter
        hit.collider.gameObject.GetComponent<Shooter>().Hits += 1;
      }
      ShootAnimation(start, dirVector, hit.distance);
      HitAnim(hit, dirVector);
    }
    else
    {
      Debug.Log("Miss");
      ShootAnimation(start, dirVector, 10000f);
    }
  }

  /*
    This returns a unit vector based on the start and target positions modified
    by the accuracy variable
   */
  Vector3 GenerateAccuracyModifiedTarget(Vector3 start, Vector3 target, float accuracy)
  {
    var mag = (target - start).magnitude;
    var yF = target.y + mag * Random.Range(-1f, 1.0f) * (1-accuracy);
    var xF = target.x + mag * Random.Range(-1f, 1.0f) * (1-accuracy);
    var newTarget = new Vector3(xF, yF, target.z);
    var dirVector = newTarget - start;
    dirVector = dirVector/dirVector.magnitude;
    return dirVector;
  }
  void ShootAnimation(Vector3 start, Vector3 dirVector, float distance)
  {
    audio.PlayOneShot(gunfire);
    Destroy(Instantiate(Resources.Load("Gunfire"), start, Quaternion.LookRotation(dirVector, Vector3.up)), 0.2f);
    var end = start + dirVector *distance;
    Debug.DrawLine(start, end, Color.red, 5f);
    
  }
  void HitAnim(RaycastHit hit, Vector3 impactVector)
  {
    var smoke = Instantiate(Resources.Load("BulletHit"), hit.point, Quaternion.LookRotation(-impactVector));
    Destroy(smoke, 0.2f);
  }
}
