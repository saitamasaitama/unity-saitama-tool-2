﻿
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
public class BuildingGeneratorParam:GenericParameters
{
}


[ExecuteInEditMode]
public class BuildingGenerator : IMapGenerator<BuildingData>
{
  private BuildingGeneratorParam param;

  public BuildingGenerator(BuildingGeneratorParam param)
  {

  }

  public BuildingData Generate(GameObject o, BuildingData oldData)
  {
    //フロアを作る

    throw new NotImplementedException();
  }
}

