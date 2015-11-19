  using UnityEngine;
  using System.Collections;
  
  public class IKPlayerController : MonoBehaviour {
    public static bool ikActive;
    public Transform rightHandObj = null, leftHandObj = null;
    public Transform lookObj = null;
    public GameObject gun = null;
    public Animator animator;
    // Use this for initialization
    void Start () {
      animator = GetComponent<Animator>();
      ikActive = false;
    }
  	
    // Update is called once per frame
    void Update () {
            
    }
    void OnAnimatorIK()
    {
      if(animator)
      {
        if(ikActive)
        {
          if(lookObj!= null)
          {
            animator.SetLookAtWeight(1);
            animator.SetLookAtPosition(lookObj.position);

            //gun.transform.LookAt(lookObj.position);
            
          }
          if(rightHandObj != null)
          {
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
            animator.SetIKPosition(AvatarIKGoal.RightHand,rightHandObj.position);
            animator.SetIKRotation(AvatarIKGoal.RightHand,rightHandObj.rotation);
          }
          if(leftHandObj != null)
          {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
            animator.SetIKPosition(AvatarIKGoal.LeftHand,leftHandObj.position);
            animator.SetIKRotation(AvatarIKGoal.LeftHand,leftHandObj.rotation);
          }
  
        }
      }
    }
  }
