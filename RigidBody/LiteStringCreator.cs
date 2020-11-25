using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LiteStringCreator : MonoBehaviour
{
  [Range(0.01f, 10f)]
  public float length=1.0f;
  [Range(0.001f,0.5f)]
  public float thickness=0.01f;//太さ

  public void Create()
  {
    //鎖状の何か

    var o = new GameObject("LiteString");

    LineRenderer line= o.AddComponent<LineRenderer>();
    LiteStringRenderUpdate up = o.AddComponent<LiteStringRenderUpdate>();

    line.positionCount =(int)((float)length/thickness)+1;
    line.startWidth = thickness;
    line.endWidth = thickness;
    //lineの子に物理演算紐を作る


    SpringJoint j=null;
    //zeroを作成する
    var zero = new GameObject($"WIRE-zero");
    zero.transform.SetParent(o.transform);
    zero.transform.localPosition = Vector3.zero;
    var zero_rigid= zero.AddComponent<Rigidbody>();
    zero_rigid.isKinematic = true;


    for (int i = 0; i < line.positionCount; i++)
    {
      var h=new GameObject($"WIRE-{i}");
      //前と後にspringJoint追加
      h.transform.localPosition = h.transform.forward * thickness*i;
      var next= h.AddComponent<SpringJoint>();
      var rigid = next.GetComponent<Rigidbody>();

      var collider= h.AddComponent<SphereCollider>();

      collider.radius = thickness / 2f;

      rigid.mass = 0.001f;
      rigid.drag = 100f;
      rigid.interpolation = RigidbodyInterpolation.Extrapolate;
      rigid.useGravity = true;
      rigid.isKinematic = false;

      next.maxDistance = 0.001f;
      next.tolerance = 0.001f;
      next.breakForce = 0.001f;

      if (j != null)
      {
        //next と j　をくっつける
        next.connectedBody = j.gameObject.GetComponent<Rigidbody>();
        
      }
      else
      {
        next.connectedBody = zero.GetComponent<Rigidbody>();
      }
      j = next;
            

      h.transform.SetParent(o.transform);
    }
  }
}
