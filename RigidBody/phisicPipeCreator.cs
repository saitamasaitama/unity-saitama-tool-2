using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PhisicPipeCreator : MonoBehaviour
{
  [Range(3,36)]
  public int count=10;
  [Range(0.01f,1.0f)]
  public float radius;
  [Range(0.01f, 100.0f)]
  public float weight;
  //回転どうしよう

  public void Create()
  {



    //CreateRing();

    CreateSphere();
  }


  private void CreateSphere()
  {
    //円状に
    GameObject o = new GameObject("PhysicSphere");



  }

  private void CreateRing()
  {
    GameObject o = new GameObject("PhysicRing");
    RingList<SpringJoint> joints = new RingList<SpringJoint>();

    for (int i = 0; i < count; i++)
    {
      float round = (float)(360.0 / count * i);
      var bit = new GameObject($"Ring{i}");
      var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
      sphere.transform.localScale = Vector3.one * radius / count * Mathf.PI;
      sphere.transform.SetParent(bit.transform);
      bit.transform.SetParent(o.transform);
      bit.transform.localPosition = Quaternion.Euler(0, round, 0) * Vector3.forward * radius / 2.0f;


      var rigid = bit.AddComponent<Rigidbody>();
      var spring = bit.AddComponent<SpringJoint>();


      joints += spring;

    }

    //連結していく
    foreach (var item in joints)
    {
      Debug.Log($"Joint+[ {item.Value.name}  <==> {item.Next.Value.name} ]");
      item.Value.connectedBody = item.Next.Value.gameObject.GetComponent<Rigidbody>();
    }

  }

}
