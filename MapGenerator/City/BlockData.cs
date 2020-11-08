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

}
