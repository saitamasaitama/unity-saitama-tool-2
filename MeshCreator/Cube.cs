using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube
{


  public static Vector3 LEFT_BACK_UP => Vector3.up + (Vector3.forward * 0.5f) + (Vector3.left * 0.5f);
  public static Vector3 RIGHT_BACK_UP => Vector3.up + (Vector3.forward * 0.5f) + (Vector3.right * 0.5f);
  public static Vector3 LEFT_FORE_UP => Vector3.up + (Vector3.back * 0.5f) + (Vector3.left * 0.5f);
  public static Vector3 RIGHT_FORE_UP => Vector3.up + (Vector3.back * 0.5f) + (Vector3.right * 0.5f);


  public static Vector3 LEFT_BACK_DOWN => Vector3.zero + (Vector3.forward * 0.5f) + (Vector3.left * 0.5f);
  public static Vector3 RIGHT_BACK_DOWN => Vector3.zero + (Vector3.forward * 0.5f) + (Vector3.right * 0.5f);
  public static Vector3 LEFT_FORE_DOWN => Vector3.zero + (Vector3.back * 0.5f) + (Vector3.left * 0.5f);
  public static Vector3 RIGHT_FORE_DOWN => Vector3.zero + (Vector3.back * 0.5f) + (Vector3.right * 0.5f);

  public static Vector3[] TOP_QUAD ={
      (Vector3.up/2)  + (Vector3.forward * 0.5f)+ (Vector3.left*0.5f),
      (Vector3.up/2) + (Vector3.forward * 0.5f) + (Vector3.right * 0.5f),
      (Vector3.up/2) + (Vector3.back * 0.5f) + (Vector3.left * 0.5f),
      (Vector3.up/2) + (Vector3.back * 0.5f) + (Vector3.right * 0.5f)
  };
}


public class Quad
{
  public Vector3 A, B, C, D;




  //
  public static Vector3[] VERTEX ={
      (Vector3.up/2)  + (Vector3.forward* 0.5f)+ (Vector3.left*0.5f),
      (Vector3.up/2) + (Vector3.forward* 0.5f) + (Vector3.right* 0.5f),
      (Vector3.up/2) + (Vector3.back* 0.5f) + (Vector3.left* 0.5f),
      (Vector3.up/2) + (Vector3.back* 0.5f) + (Vector3.right* 0.5f)
  };

  public static int[] INDEX_FORE ={
    1,3,2,0
  };

  public static int[] INDEX_BACK ={
   3,1,0,2
  };

  public static Vector2[] UV =
  {
    Vector2.zero,
    Vector2.right,
    Vector2.zero+Vector2.up,
    Vector2.right+Vector2.up,
  };

}