using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MapGen.City;


public class BlockData : MonoBehaviour
{
  [SerializeField]
  public Block data;

  //地上部高さ
  public float BuildingGroundHeight;
  //地下部高さ
  public float BuildingUnderGroundHeight;

  public void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.magenta*0.5f;
    Gizmos.DrawCube(
      data.Center.toVector3()
      + Vector3.up * 5f,
      new Vector3(
        data.Width,
        10,
        data.Height)
        );        
  }
}
