using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube 
{
  public static Vector3 LEFT_BACK_UP => Vector3.up + (Vector3.forward * 0.5f)+ (Vector3.left*0.5f);
  public static Vector3 RIGHT_BACK_UP => Vector3.up + (Vector3.forward * 0.5f) + (Vector3.right * 0.5f);
  public static Vector3 LEFT_FORE_UP => Vector3.up + (Vector3.back * 0.5f) + (Vector3.left * 0.5f);
  public static Vector3 RIGHT_FORE_UP => Vector3.up + (Vector3.back * 0.5f) + (Vector3.right * 0.5f);


  public static Vector3 LEFT_BACK_DOWN => Vector3.zero + (Vector3.forward * 0.5f) + (Vector3.left * 0.5f);
  public static Vector3 RIGHT_BACK_DOWN => Vector3.zero + (Vector3.forward * 0.5f) + (Vector3.right * 0.5f);
  public static Vector3 LEFT_FORE_DOWN => Vector3.zero + (Vector3.back * 0.5f) + (Vector3.left * 0.5f);
  public static Vector3 RIGHT_FORE_DOWN => Vector3.zero + (Vector3.back * 0.5f) + (Vector3.right * 0.5f);
}
