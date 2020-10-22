using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * 対象のTransformからぶら下がる
 */
public class AnchorFollow : MonoBehaviour
{
  public Transform Target=>Camera.main.transform;

  //首の向きと逆方向に棒を伸ばして、そこから垂直に下げる
  public float NeckLength = 0.1f;
  public Vector3 Anchor=Vector3.down;

  // Start is called before the first frame update
  void Start()
  {
        
  }

  // Update is called once per frame
  void Update()
  {
    if (Target)
    {
      Vector3 neckPos = Target.transform.position+(Target.transform.forward * -1 * NeckLength);
      this.transform.position = neckPos + Anchor;
      //Y回転のみ追従
      this.transform.localRotation = Quaternion.Euler(
        0,
        Target.transform.localEulerAngles.y,
        0
        );
    }
        
  }

  
}
