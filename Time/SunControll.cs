using UnityEngine;
using System.Collections;
using System;

public class SunControll : MonoBehaviour
{
  private float r = 0;

  void Update()
  {
    TimeSpan span = GameTime.Now.ToUniversalTime() 
      - new DateTime(1970, 1, 1,21,15,0);
    //とりあえず1日=360度とする
    float dayTime = (float)(span.TotalSeconds  % 86400.0);
    float x = dayTime / 86400.0f  * 360.0f;

    this.transform.localRotation =Quaternion.Euler(x, 0, 0);




  }
}
