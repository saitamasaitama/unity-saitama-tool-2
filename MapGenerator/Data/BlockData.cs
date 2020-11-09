using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MapGen.City;


public class BlockData : GenericData
{
  [SerializeField]
  public Block block;
  [SerializeField]
  public List<BuildingData> Buildings = new List<BuildingData>();


  //地上部高さ
  public float BuildingGroundHeight;
  //地下部深さ
  public float BuildingUnderGroundDepth;

  public override void Reset()
  {
    throw new NotImplementedException();
  }
}
