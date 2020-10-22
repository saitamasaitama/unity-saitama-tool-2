using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[ExecuteInEditMode]
public class HumanouidSensingGenerate : MonoBehaviour
{
  public Animator Animator => GetComponent<Animator>();
  public Rigidbody Rigidbody => GetComponent<Rigidbody>();
  // Start is called before the first frame update
  void Start()
  {
    //まずRigidbodyを制御
    Rigidbody.isKinematic = true;
    //
    //Rigidbody.gameObject

    foreach(HumanBodyBones bone in Enum.GetValues(typeof(HumanBodyBones)))
    {
      AppendSphere(bone);
    }
  }

  public void AppendSphere(HumanBodyBones bone)
  {
    Transform t = Animator.GetBoneTransform(bone);
    if (t)
    {
      SphereCollider sphere= t.gameObject.AddComponent<SphereCollider>();
      sphere.isTrigger = true;
      sphere.radius = 0.1f;

    }

  }

  

}
