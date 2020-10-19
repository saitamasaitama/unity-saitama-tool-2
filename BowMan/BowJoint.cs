using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class BowJoint : MonoBehaviour
{
  public List<BowJoint> Joints => this.GetComponentsInChildren<BowJoint>().ToList();


  public BowJoint Parent => this.GetComponentInParent<BowJoint>();
  public GameObject Ball;
  public GameObject Bow;


  [Range(-180, 180)]
  public float yaw = 0;
  [Range(-90, 90)]
  public float pitch = 0;
  [Range(0, 2f)]
  public float length = 0.1f;

  [Range(0, 1f)]
  public float BowScale = 0.05f;

  [Range(-180, 180)]
  public float nextyaw = 0;
  [Range(-90, 90)]
  public float nextpitch = 0;
  [Range(0, 2f)]
  public float nextlength = 0.1f;


  public float LengthFromRoot => 10f;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame



  void Update()
  {
    //根元からの方向を調整する

    if (!Application.isPlaying)
    {
      this.RevicePosition();
      this.DraftDraw();
    }
    



  }

  private void DraftDraw()
  {
    Quaternion q = Quaternion.Euler(pitch, yaw, 0);
    Vector3 direction = q * Vector3.forward * length;




  }

  private void RevicePosition()
  {
    //rootには何もしない
    if (!this.Ball) return;
    Quaternion q = Quaternion.Euler(pitch, yaw, 0);
    Vector3 direction = q * Vector3.forward * length;
    this.transform.localPosition = direction;
    if (this.Ball)
    {

      this.Ball.transform.localScale = Vector3.one * BowScale * 1.2f;
      this.Bow.transform.localScale = new Vector3(
        BowScale,
        length / 2,
        BowScale
      );


      this.Bow.transform.localScale = new Vector3(
        BowScale,
        length / 2,
        BowScale
      );

      this.Bow.transform.localPosition = -direction * 0.5f;
      this.Bow.transform.localRotation = Quaternion.Euler(90 + pitch, yaw, 0);



    }
    

  }

  public BowJoint CreateJoint(float yaw, float pitch, float length)
  {
    var o = new GameObject("Joint");

    //位置調整
    Quaternion q = Quaternion.Euler(pitch, yaw, 0);
    Vector3 direction = q * Vector3.forward * length;

    //ボール
    var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    sphere.transform.SetParent(o.transform);
    sphere.transform.localScale = Vector3.one * BowScale * 1.2f;

    //棒
    var stick = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
    stick.transform.SetParent(o.transform);
    stick.transform.localScale = new Vector3(
      BowScale,
      length / 2,
      BowScale
    );
    stick.transform.localPosition = -direction * 0.5f;
    stick.transform.localRotation = Quaternion.Euler(90+ pitch, yaw, 0);







    o.transform.SetParent(this.transform);
    o.transform.localPosition = direction;

    //jointのパラメータ設定
    BowJoint joint = o.AddComponent<BowJoint>();
    joint.yaw = yaw;
    joint.pitch = pitch;
    joint.length = length;

    joint.BowScale = this.BowScale;
    joint.Ball = sphere;
    joint.Bow = stick;
    return joint;
  }

  //ルートとつなぐ
  public void AddChainJoint()
  {

    Rigidbody r= this.gameObject.AddComponent<Rigidbody>();
    //ヒンジは親に付ける
    if (this.transform.parent.GetComponent<Rigidbody>())
    {
      HingeJoint j = this.transform.parent.gameObject.AddComponent<HingeJoint>();
      j.connectedBody = r;
    }
    
    

  }
}