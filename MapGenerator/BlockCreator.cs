using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using MapGen.City;
using System;
using Random = UnityEngine.Random;


[ExecuteInEditMode]
public class BlockCreator : GenericCreator<BlockGeneratorParam, BlockData>
{
  public override IMapGenerator<BlockData> getGenerator(BlockGeneratorParam param)
  {
    return new BlockGenerator(param);
  }
}
