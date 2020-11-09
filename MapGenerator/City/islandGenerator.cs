
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
public class islandGeneratorParam:GenericParameters
{
}


[ExecuteInEditMode]
public class islandGenerator : IMapGenerator<islandData>
{
  private islandGeneratorParam param;

  public islandGenerator(islandGeneratorParam param)
  {

  }

  public islandData Generate(GameObject o)
  {    
    throw new NotImplementedException();
  }
}

