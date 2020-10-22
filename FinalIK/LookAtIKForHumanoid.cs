using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[ExecuteInEditMode]
public class LookAtIKForHumanoid : MonoBehaviour
{
  public Animator Animator => GetComponent<Animator>();
  public Avatar Avatar => Animator.avatar;

  // Start is called before the first frame update
  void Start()
  {
    
    Debug.Log("Init Execute Look");
    LookAtIK ik= this.gameObject.AddComponent<RootMotion.FinalIK.LookAtIK>();
    ik.solver.head.transform = Animator.GetBoneTransform(HumanBodyBones.Head);

    ik.solver.eyes = new IKSolverLookAt.LookAtBone[]
    {
      new IKSolverLookAt.LookAtBone(){transform=Animator.GetBoneTransform(HumanBodyBones.LeftEye)},
      new IKSolverLookAt.LookAtBone(){transform=Animator.GetBoneTransform(HumanBodyBones.RightEye)}

    };

    ik.solver.spine = new IKSolverLookAt.LookAtBone[]
    {

      new IKSolverLookAt.LookAtBone(){transform=Animator.GetBoneTransform(HumanBodyBones.Neck)},
      new IKSolverLookAt.LookAtBone(){transform=Animator.GetBoneTransform(HumanBodyBones.UpperChest)},
      new IKSolverLookAt.LookAtBone(){transform=Animator.GetBoneTransform(HumanBodyBones.Chest)},

    };

    ik.solver.target = Camera.main.transform;

    ik.solver.headWeight = 0.3f;
    ik.solver.bodyWeight = 0.1f;
    ik.solver.eyesWeight = 0.5f;



    GameObject.DestroyImmediate(this);

  }

  // Update is called once per frame
  void Update()
  {

  }
}
