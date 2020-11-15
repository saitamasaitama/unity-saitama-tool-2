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

  public BlockData Generate(GameObject o, BlockData oldData)
  {

    oldData.Buildings.Clear();

    var result = oldData;

    //雑にビルを生成
    for(int i = 0; i < 10; i++)
    {
      var building = new GameObject("building");
      building.transform.SetParent(o.transform);
      var buildingCreator = building.AddComponent<BuildingCreator>();
      buildingCreator.data = new BuildingData();
      Debug.Log("data set");
      buildingCreator.data.building = new MapGen.City.Building()
      {
        Width = 1f

      };
      /*
      buildingCreator.data.building = new MapGen.City.Building()
      {
        Width = param.minBuildingWidth,
        Height = param.minBuildingHeight,
        Center = Point2F.From(1, 1),
        GroundHeight = 10f,
        GroundDepth =1,
        owner = result.block        
      };
      */

      result.Buildings.Add(buildingCreator.data);
    }


    return result;
  }
}
