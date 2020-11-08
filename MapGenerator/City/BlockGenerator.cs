using UnityEngine;
using System.Collections;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using MapGen.City;
using Random = UnityEngine.Random;

[Serializable]
public class BlockGeneratorParam:GenericParameters
{
  public float dense = 0.5f;//ビルの密集度

  public float buildingFloorHeightMin = 2.4f;
  public float buildingFloorHeightMax = 4f;

  //階層制限
  public int buildingGroundFloorCountMin = 2;
  public int buildingGroundFloorCountMax = 10;


  public float buildingTotalHeightMax = 45f;

  public float StreetLineWidth = 1f;
  public float AvenueLineWidth = 1.5f;

  public float minBuildingWidth = 10f;
  public float minBuildingHeight = 10f;
}


[ExecuteInEditMode]
public class BlockGenerator : IMapGenerator<BlockData>
{
  private BlockGeneratorParam param;

  public BlockGenerator(BlockGeneratorParam param)
  {

  }

  public BlockData Generate(GameObject o)
  {
    
    throw new NotImplementedException();
  }

}
