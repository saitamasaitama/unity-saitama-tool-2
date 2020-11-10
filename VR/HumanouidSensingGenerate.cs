using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEditor.UIElements;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[ExecuteInEditMode]
public class HumanouidSensingGenerate : MonoBehaviour
{
  const float INCH = 0.0251f;

  public Animator Animator => GetComponent<Animator>();
  public Rigidbody Rigidbody => GetComponent<Rigidbody>();
  // Start is called before the first frame update
  void Start()
  {
    //まずRigidbodyを制御
    Rigidbody.isKinematic = true;
    //
    //Rigidbody.gameObject

    foreach (HumanBodyBones bone in Enum.GetValues(typeof(HumanBodyBones)))
    {
      AppendSphere(bone);
    }

    //センサーを足して自身は破棄
    var sense= this.gameObject.AddComponent<HumanoidSense>();


    GameObject.DestroyImmediate(this);
  }

  public void AppendSphere(HumanBodyBones bone)
  {
    if (!Animator.isHuman)
    {
      Debug.LogError("Not Human Bone!");
    }

    if (bone == HumanBodyBones.LastBone)
    {
      Debug.Log($"LAST Bone {bone}");
      return;
    }

    Transform t = Animator.GetBoneTransform(bone);
    if (t)
    {

      float scale = getBoneScale(bone);
      SphereCollider sphere = t.gameObject.AddComponent<SphereCollider>();
      sphere.isTrigger = true;
      sphere.radius = scale;
      //
      t.gameObject.layer = LayerMask.NameToLayer("VRUI");
      //分かりやすいタグを入れる   
    }
  }

  private float getBoneScale(HumanBodyBones bone)
  {
    switch (bone)
    {
      case HumanBodyBones.LeftEye:
      case HumanBodyBones.RightEye:
      case HumanBodyBones.Jaw: return INCH;

      case HumanBodyBones.LeftHand:
      case HumanBodyBones.RightHand: return 2 * INCH;
      //Handここまで
      case HumanBodyBones.LeftIndexDistal:
      case HumanBodyBones.LeftIndexIntermediate:
      case HumanBodyBones.LeftIndexProximal:
      case HumanBodyBones.LeftLittleDistal:
      case HumanBodyBones.LeftLittleIntermediate:
      case HumanBodyBones.LeftLittleProximal:
      case HumanBodyBones.LeftMiddleDistal:
      case HumanBodyBones.LeftMiddleIntermediate:
      case HumanBodyBones.LeftMiddleProximal:
      case HumanBodyBones.LeftRingDistal:
      case HumanBodyBones.LeftRingIntermediate:
      case HumanBodyBones.LeftRingProximal:
      case HumanBodyBones.LeftThumbDistal:
      case HumanBodyBones.LeftThumbIntermediate:
      case HumanBodyBones.LeftThumbProximal:
      case HumanBodyBones.RightIndexDistal:
      case HumanBodyBones.RightIndexIntermediate:
      case HumanBodyBones.RightIndexProximal:
      case HumanBodyBones.RightLittleDistal:
      case HumanBodyBones.RightLittleIntermediate:
      case HumanBodyBones.RightLittleProximal:
      case HumanBodyBones.RightMiddleDistal:
      case HumanBodyBones.RightMiddleIntermediate:
      case HumanBodyBones.RightMiddleProximal:
      case HumanBodyBones.RightRingDistal:
      case HumanBodyBones.RightRingIntermediate:
      case HumanBodyBones.RightRingProximal:
      case HumanBodyBones.RightThumbDistal:
      case HumanBodyBones.RightThumbIntermediate:
      case HumanBodyBones.RightThumbProximal: return INCH;

      default: return 0.1f;


    }
  }


}
