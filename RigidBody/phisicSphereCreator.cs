using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class phisicSphereCreator : MonoBehaviour
{
  [Range(0.01f, 10.0f)]
  public float size = 0.1f;
  [Range(0.01f, 10.0f)]
  public float radius;
  [Range(0.01f, 100.0f)]
  public float weight;
  //回転どうしよう


  public int Count
  {
    get {
      int result = 0;
      int ycounts = (int)(Mathf.PI * radius / 4.0f / size);
      for(int y = 0; y < ycounts; y++)
      {
        float tilt = 90.0f / ycounts * y;
        float calcedRadius = (Quaternion.Euler(tilt, 0, 0) * Vector3.forward * radius).z;
        int xcounts = (int)(Mathf.PI * calcedRadius / size);

        result += xcounts;
      }
      return result;
    }
  }


  //渦巻型にデータを生成していく
  public void Create()
  {
    Debug.Log($"Count={Count}");
    
    GameObject o = new GameObject("PhysicSphere");
    Rigidbody r= o.AddComponent<Rigidbody>();
    //sizeでradiusをいくつ埋められるのかを計算
    RingList<SpringJoint> joints = new RingList<SpringJoint>();

    int ycounts = (int)(Mathf.PI * radius / 4.0f / size);
    for (int y = 0; y < ycounts; y++)
    {

      float tilt = 90.0f / ycounts * y;
      float calcedRadius = (Quaternion.Euler(tilt, 0, 0) * Vector3.forward * radius).z;
      int xcounts = (int)(Mathf.PI * calcedRadius / size);
      Debug.Log($"Y={y};X={xcounts}");

      for (int x = 0; x < xcounts; x++)
      {
        float pan = 360.0f / xcounts * x;
        SpringJoint j = genSpring(pan, tilt);
        j.transform.SetParent(o.transform);
        joints += j;
      }
    }

    //連結していく
    foreach (var item in joints)
    {

      item.Value.connectedBody = r;
    }
  }

  private SpringJoint genSpring(float pan,float tilt)
  {
    var bit = new GameObject($"Ring({pan}-{tilt})");
    var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    sphere.transform.localScale = Vector3.one * size;
    sphere.transform.SetParent(bit.transform);


    bit.transform.localPosition = Quaternion.Euler(tilt,pan, 0) * Vector3.forward * radius / 2.0f;
    var rigid = bit.AddComponent<Rigidbody>();
    rigid.mass = weight;
    var spring = bit.AddComponent<SpringJoint>();
    return spring;
  }
}
